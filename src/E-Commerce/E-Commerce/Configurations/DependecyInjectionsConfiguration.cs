using Application.Common.Interfaces.Repositories;
using Application.Dtos;
using Application.DTOs.FlashSaleItems;
using Application.Helpers;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Services;
using Application.Services.Implementations;
using Application.Settings;
using Application.Validators;
using Domain.Repositories;
using FluentValidation;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Repositories;
using Microsoft.Extensions.Options;

namespace E_Commerce.Configurations;

public static class DependecyInjectionsConfiguration
{
    public static void ConfigureDependecies (this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IAddressService, AddressService>();
        services.AddScoped<IBannerService, BannerService>();
        services.AddScoped<ICartItemService, CartItemService>();
        services.AddScoped<ICartService, CartService>();
        services.AddScoped<ICouponService, CouponService>();
        services.AddScoped<IFlashSaleItemService, FlashSaleItemService>();
        services.AddScoped<IFlashSaleService, FlashSaleService>();
        services.AddScoped<IOrderItemService, OrderItemService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IPaymentOptionService, PaymentOptionService>();
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<IProductImageService, ProductImageService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IReviewService, ReviewService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IWishlistItemService, WishlistItemService>();
        services.AddScoped<IWishlistService, WishlistService>();
        
        services.AddScoped<IAddressRepository, AddressRepository>();
        services.AddScoped<IBannerRepository, BannerRepository>();
        services.AddScoped<ICartItemRepository, CartItemRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ICartRepository, CartRepository>();
        services.AddScoped<ICouponRepository, CouponRepository>();
        services.AddScoped<IFlashSaleItemRepository, FlashSaleItemRepository>();
        services.AddScoped<IFlashSaleRepository, FlashSaleRepository>();
        services.AddScoped<IOrderItemRepository, OrderItemRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IPaymentOptionRepository, PaymentOptionRepository>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped<IProductImageRepository, ProductImageRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IReviewRepository, ReviewRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IWishlistItemRepository, WishlistItemRepository>();
        services.AddScoped<IWishlistRepository, WishlistRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

        services.AddScoped<IValidator<AddressCreateDto>, AddressCreateDtoValidator>();
        services.AddScoped<IValidator<CartItemCreateDto>, CartItemCreateDtoValidator>();
        services.AddScoped<IValidator<ConfirmCodeRequestDto>, ConfirmCodeRequestDtoValidator>();
        services.AddScoped<IValidator<CouponCreateDto>, CouponCreateDtoValidator>();
        services.AddScoped<IValidator<FlashSaleCreateDto>, FlashSaleCreateDtoValidator>();
        services.AddScoped<IValidator<FlashSaleItemCreateDto>, FlashSaleItemCreateDtoValidator>();
        services.AddScoped<IValidator<OrderCreateDto>, OrderCreateDtoValidator>();
        services.AddScoped<IValidator<OrderItemCreateDto>, OrderItemCreateDtoValidator>();
        services.AddScoped<IValidator<PaymentCreateDto>, PaymentCreateDtoValidator>();
        services.AddScoped<IValidator<PaymentOptionCreateDto>, PaymentOptionCreateDtoValidator>();
        services.AddScoped<IValidator<UserCreateDto>, UserCreateDtoValidator>();
        services.AddScoped<IValidator<UserLoginDto>, UserLoginDtoValidator>();


        services.AddScoped<ITokenService, TokenService>();



    }
}
