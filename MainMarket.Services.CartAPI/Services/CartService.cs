using AutoMapper;
using MainMarket.Services.CartAPI.Data;
using MainMarket.Services.CartAPI.Exceptions;
using MainMarket.Services.CartAPI.Models.DTO;
using MainMarket.Services.CartAPI.Models.DTOs;
using MainMarket.Services.CartAPI.Models.Entities;
using MainMarket.Services.CartAPI.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace MainMarket.Services.CartAPI.Services;

public class CartService : ICartService
{
    private readonly MongoOptions _options;
    private readonly CartContext _context;
    private readonly IMapper _mapper;
    private readonly IProductApiService _productService;
    private readonly ILogger<CartService> _logger;

    public CartService(
        IOptions<MongoOptions> options,
        IMapper mapper,
        IProductApiService productService,
        ILogger<CartService> logger)
    {
        _options = options.Value;
        _mapper = mapper;
        var client = new MongoClient(_options.ConnectionString);
        _context = CartContext.Create(client.GetDatabase(_options.DatabaseName));
        _productService = productService;
        _logger = logger;
    }

    public Task<CartResponse> ApplyCoupon(string couponCode)
    {
        throw new NotImplementedException();
    }

    public async Task<CartResponse> GetCartAsync(string userId)
    {
        var cart = await _context.Carts.FirstOrDefaultAsync(c => c.UserId == userId) ?? throw new NotFoundException($"no cart with userId: {userId} found");
        return _mapper.Map<CartResponse>(cart);
    }

    public async Task<CartResponse> RemoveCartItem(string userId)
    {
        var cart = await _context.Carts.FirstOrDefaultAsync(c => c.UserId == userId) ?? throw new NotFoundException($"no cart with userId: {userId} found");
        var cartEntuty = _context.Carts.Remove(cart);
        return _mapper.Map<CartResponse>(cartEntuty.Entity);
    }

    public async Task<CartResponse> UpSertCart(CartRequest cartRequest)
    {
        if (string.IsNullOrEmpty(cartRequest.Id))
        {
            var newCart = _mapper.Map<Cart>(cartRequest);

            newCart.Id = Guid.NewGuid().ToString();

            _logger.LogInformation("creating new cart");

            await _context.Carts.AddAsync(newCart);

            var productsTask = GetProducts(_productService, cartRequest);
            var saveChangesTask = _context.SaveChangesAsync();

            await Task.WhenAll(productsTask, saveChangesTask);

            var products = await productsTask;

            _logger.LogInformation("created new cart");

            return MapResponse(cartRequest, products, newCart);
        }
        else
        {
            var existingCart = await _context.Carts.FirstOrDefaultAsync(x => x.Id == cartRequest.Id) ?? throw new NotFoundException("Cart Id does not exist");

            _logger.LogInformation("Updating existing cart");
            _mapper.Map(cartRequest, existingCart);

            _context.Carts.Update(existingCart);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Updated cart properties");

            return _mapper.Map<CartResponse>(existingCart);
        }
    }

    private static async Task<List<ProductResponse>> GetProducts(IProductApiService _productService, CartRequest cartRequest)
    {
        var apiResponse = await _productService.GetProducts() ?? throw new Exception("Unable to fetch products");

        var productIdsInCart = cartRequest.CartDetails.Select(detail => detail.ProductId).ToList();

        return apiResponse.Data
           .Where(product => productIdsInCart.Contains(product.Id))
           .ToList();
    }

    private static CartResponse MapResponse(CartRequest cartRequest, List<ProductResponse> products, Cart newCart)
    {
        var cartDetailResponses = cartRequest.CartDetails.Select(detail =>
        {
            var product = products.FirstOrDefault(p => p.Id == detail.ProductId);
            return new CartDetailResponse
            {
                ProductId = detail.ProductId,
                Count = detail.Count,
                Product = product,
            };
        }).ToList();

        return new CartResponse
        {
            Id = newCart.Id,
            UserId = newCart.UserId,
            CouponCode = newCart.CouponCode,
            Discount = newCart.Discount,
            Total = newCart.Total,
            CartDetails = cartDetailResponses
        };
    }
}