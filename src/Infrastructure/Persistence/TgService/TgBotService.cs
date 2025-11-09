using Application.Dtos;
using Application.Interfaces;
using Application.Interfaces.Services;
using Application.Services;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Infrastructure.Persistence.TgService;

public class TgBotService : BackgroundService, ITelegramBotService
{
    private readonly ILogger<TgBotService> _logger;
    private readonly TelegramBotClient _botClient;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly AddressHandler _addressHandler;
    private readonly ProductBotService _productBotService;
    private readonly string _channelId = "@StylePointMarket";
    private static Dictionary<long, (long OrderId, bool WaitingForPromo, PaymentMethod Method)> PendingPromoEntries = new();

    public TgBotService(ILogger<TgBotService> logger, IConfiguration config, IServiceScopeFactory scopeFactory, ProductBotService productBotService)
    {
        _logger = logger;
        _scopeFactory = scopeFactory;

        string token = config["TelegramBot:Token"]
            ?? throw new InvalidOperationException("Telegram bot token not found in configuration.");
        _botClient = new TelegramBotClient(token);

        _addressHandler = new AddressHandler(_botClient, _scopeFactory);
        _productBotService = new ProductBotService(_botClient, _scopeFactory);
    }

    public async Task NotifyNewProductAsync(Product product)
    {
        var botUsername = "StylePointUzb_Bot";
        var deepLinkStart = $"https://t.me/{botUsername}?start=start";
        var deepLinkKey = $"https://t.me/{botUsername}?start={product.SecretCode}";

        var captionBuilder = new StringBuilder();
        captionBuilder.AppendLine("<b>🆕 Yangi mahsulot keldi!</b>\n");
        captionBuilder.AppendLine($"<b>📦 Nomi:</b> {product.Name}");
        captionBuilder.AppendLine($"<b>💰 Narxi:</b> {product.Price:N0} so‘m");
        if (product.DiscountPrice > 0)
            captionBuilder.AppendLine($"<b>💸 Aksiya narxi:</b> {product.DiscountPrice:N0} so‘m");
        //captionBuilder.AppendLine($"<b>📂 Kategoriya:</b> {(product.Category?.Name ?? "Aniqlanmagan")}");
        //captionBuilder.AppendLine($"<b>🧩 Maxfiy kod:</b> <code>{product.SecretCode}</code>\n");
        captionBuilder.AppendLine("📝 <b>Tavsif:</b>");
        captionBuilder.AppendLine(product.Description);
        captionBuilder.AppendLine("\n🛍️ <i>Agar siz botdan foydalanmagan bo‘lsangiz, avval “START” tugmasini bosing.</i>");
        captionBuilder.AppendLine("Keyin esa <b>🔍 Mahsulotlarni qidirish</b> tugmasini bosing 👇");

        var replyMarkup = new InlineKeyboardMarkup(new[]
        {
        InlineKeyboardButton.WithUrl("🤖 Botni ochish (START)", $"https://t.me/{botUsername}"),
        InlineKeyboardButton.WithUrl("🔍 Mahsulotlarni qidirish", deepLinkKey)
    });

        try
        {
            if (!string.IsNullOrEmpty(product.ImageUrl))
            {
                if (product.ImageUrl.StartsWith("data:image"))
                {
                    // Base64 formatdagi rasmni yuborish
                    var base64Data = product.ImageUrl.Substring(product.ImageUrl.IndexOf(",") + 1);
                    var bytes = Convert.FromBase64String(base64Data);
                    await using var stream = new MemoryStream(bytes);

                    await _botClient.SendPhotoAsync(
                        chatId: _channelId,
                        photo: InputFile.FromStream(stream, "product.jpg"),
                        caption: captionBuilder.ToString(),
                        parseMode: ParseMode.Html,
                        replyMarkup: replyMarkup
                    );
                }
                else
                {
                    // URL orqali rasm yuborish
                    await _botClient.SendPhotoAsync(
                        chatId: _channelId,
                        photo: InputFile.FromUri(product.ImageUrl),
                        caption: captionBuilder.ToString(),
                        parseMode: ParseMode.Html,
                        replyMarkup: replyMarkup
                    );
                }
            }
            else
            {
                // Agar rasm yo‘q bo‘lsa, faqat matn yuborish
                await _botClient.SendTextMessageAsync(
                    chatId: _channelId,
                    text: captionBuilder.ToString(),
                    parseMode: ParseMode.Html,
                    replyMarkup: replyMarkup
                );
            }
        }
        catch (Exception ex)
        {
            await _botClient.SendTextMessageAsync(
                chatId: _channelId,
                text: $"⚠️ Xato: {ex.Message}"
            );
        }
    }




    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("🤖 Telegram bot started...");

        int offset = 0;
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var updates = await _botClient.GetUpdatesAsync(offset, cancellationToken: stoppingToken);

                foreach (var update in updates)
                {
                    offset = update.Id + 1;
                    await HandleUpdateAsync(update);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Telegram bot error occurred.");
                await Task.Delay(2000, stoppingToken);
            }
        }

        _logger.LogInformation("🛑 Telegram bot stopped.");
    }

    private async Task HandleUpdateAsync(ITelegramBotClient _botClient, Update update, IServiceScope scope)
    {
        if (update.Type == UpdateType.Message && update.Message!.Type == MessageType.Text)
        {
            var chatId = update.Message.Chat.Id;
            var text = update.Message.Text!.Trim();

            var orderService = scope.ServiceProvider.GetRequiredService<IOrderService>();
            var productService = scope.ServiceProvider.GetRequiredService<IProductService>();
            var paymentService = scope.ServiceProvider.GetRequiredService<IPaymentService>();

            if (text.Equals("/start", StringComparison.OrdinalIgnoreCase))
            {
                await _botClient.SendTextMessageAsync(
                    chatId,
                    "👋 Salom! Buyurtma berish uchun mahsulot tanlang yoki /orders buyrug‘idan foydalaning."
                );
                return;
            }

            if (text.Equals("/orders", StringComparison.OrdinalIgnoreCase))
            {
                var orders = await orderService.GetAllByTelegramChatIdAsync(chatId);
                if (!orders.Any())
                {
                    await _botClient.SendTextMessageAsync(chatId, "Sizda hali hech qanday buyurtma yo‘q.");
                    return;
                }

                foreach (var order in orders)
                {
                    string msg =
                        $"🧾 Buyurtma ID: {order.Id}\n" +
                        $"🕒 Sana: {order.CreatedAt:dd.MM.yyyy HH:mm}\n" +
                        $"💰 Umumiy summa: {order.TotalAmount}\n" +
                        $"📦 Status: {order.Status}";

                    var buttons = new List<InlineKeyboardButton[]>
                {
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("💳 To‘lash (Kartadan)", $"payOrderCard_{order.Id}"),
                        InlineKeyboardButton.WithCallbackData("💵 Naqd to‘lash", $"payOrderCash_{order.Id}")
                    }
                };

                    await _botClient.SendTextMessageAsync(chatId, msg, replyMarkup: new InlineKeyboardMarkup(buttons));
                }

                return;
            }

            await _botClient.SendTextMessageAsync(chatId, "Noma’lum buyruq. /start yoki /orders ni yozing.");
        }

        if (update.Type == UpdateType.CallbackQuery)
        {
            var query = update.CallbackQuery!;
            var chatId = query.Message!.Chat.Id;

            var paymentService = scope.ServiceProvider.GetRequiredService<IPaymentService>();

            if (query.Data!.StartsWith("payOrderCard_") || query.Data.StartsWith("payOrderCash_"))
            {
                var idStr = query.Data.Split("_")[1];
                if (!long.TryParse(idStr, out var orderId))
                    return;

                var method = query.Data.StartsWith("payOrderCard_")
                    ? PaymentMethod.Card
                    : PaymentMethod.Cash;

                var dto = new PaymentCreateDto
                {
                    OrderId = orderId,
                    Method = method
                };

                try
                {
                    var paymentDto = await paymentService.ProcessTelegramPaymentAsync(chatId, dto);
                    await _botClient.AnswerCallbackQueryAsync(query.Id, "✅ To‘lov amalga oshirildi!");
                    await _botClient.EditMessageTextAsync(
                        chatId,
                        query.Message.MessageId,
                        $"✅ Buyurtma {paymentDto.OrderId} uchun to‘lov muvaffaqiyatli amalga oshirildi."
                    );
                }
                catch (Exception ex)
                {
                    await _botClient.AnswerCallbackQueryAsync(query.Id, $"❌ Xato: {ex.Message}");
                }

                return;
            }
        }
    }

}