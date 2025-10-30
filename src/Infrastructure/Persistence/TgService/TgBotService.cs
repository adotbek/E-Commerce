using Application.Services;
using Domain.Entities;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.Persistence.TgService;

public class TgBotService : BackgroundService, ITelegramBotService
{
    public Task NotifyNewProductAsync(Product product)
    {
        throw new NotImplementedException();
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        throw new NotImplementedException();
    }
}
