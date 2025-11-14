using Application.Common.Interfaces.Repositories;
using Application.Dtos;
using Application.Helpers;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Services;
using Application.Services.Implementations;
using Application.Validators;
using Domain.Repositories;
using FluentValidation;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Repositories;

namespace E_Commerce.Configurations;

public static class DependecyInjectionsConfiguration
{
    public static void ConfigureDependecies(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IAddressService, AddressService>();
        services.AddScoped<ICartItemService, CartItemService>();
        services.AddScoped<ICartService, CartItemService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IUserService, UserService>();


        services.AddScoped<ICategoryService, CategoryService>();




        services.AddScoped<IAddressRepository, AddressRepository>();
        services.AddScoped<ICartItemRepository, CartItemRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ICartItemRepository, CartRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

        services.AddScoped<IValidator<AddressCreateDto>, AddressCreateDtoValidator>();
        services.AddScoped<IValidator<CartItemCreateDto>, CartItemCreateDtoValidator>();
        services.AddScoped<IValidator<ConfirmCodeRequestDto>, ConfirmCodeRequestDtoValidator>();
        services.AddScoped<IValidator<OrderCreateDto>, OrderCreateDtoValidator>();
        services.AddScoped<IValidator<PaymentCreateDto>, PaymentCreateDtoValidator>();
        services.AddScoped<IValidator<UserCreateDto>, UserCreateDtoValidator>();
        services.AddScoped<IValidator<UserLoginDto>, UserLoginDtoValidator>();


        services.AddScoped<ITokenService, TokenService>();



    }
}
