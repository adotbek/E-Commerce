using Api.Endpoints;
using E_Commerce.Configurations;
using E_Commerce.Extensions;
using E_Commerce.Middlewares;

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
app.MapAddressEndpoints();
app.MapCartEndpoints();
app.MapCategoryEndpoints();
app.MapCouponEndpoints();
app.MapOrderEndpoints();
app.MapPaymentEndpoints();
app.MapPaymentOptionEndpoints();
app.MapProductEndpoints();
app.MapProductImageEndpoints();
app.MapReviewEndpoints();
app.MapWishlistEndpoints();

app.MapControllers();

app.Run();
