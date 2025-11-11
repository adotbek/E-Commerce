using Api.Endpoints;
using E_Commerce.Configurations;
using E_Commerce.Endpoints;
using E_Commerce.Extensions;
using E_Commerce.Middlewares;
using Telegram.Bot;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.ConfigureDataBase();
builder.ConfigurationJwtAuth();
builder.Services.ConfigureDependecies();
builder.ConfigureJwtSettings();


//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowLocalhost5173", policy =>
//    {
//        policy.WithOrigins(
//            "http://localhost:4200",
//            "http://localhost:5173"
//        )
//        .AllowAnyHeader()
//        .AllowAnyMethod();
//    });
//});

var botToken = builder.Configuration["TelegramBot:Token"];
if (string.IsNullOrEmpty(botToken))
    throw new InvalidOperationException("? Telegram bot token topilmadi!");
builder.Services.AddSingleton<ITelegramBotClient>(new TelegramBotClient(botToken));
//builder.Services.AddSingleton<ProductBotService>();

//builder.Services.AddSingleton<ITelegramBotService, TgBotService>();
//builder.Services.AddSingleton<IHostedService>(sp => (TgBotService)sp.GetRequiredService<ITelegramBotService>());


ServiceCollectionExtensions.AddSwaggerWithJwt(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.MapAuthEndpoints();
app.MapAdminEndpoints();
app.MapAddressEndpoints();
app.MapCartEndpoints();
app.MapCategoryEndpoints();
app.MapOrderEndpoints();
app.MapPaymentEndpoints();
app.MapProductEndpoints();

app.MapControllers();

app.Run();
