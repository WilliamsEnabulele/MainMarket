using MainMarket.Services.ProductAPI.Data;
using MainMarket.Services.ProductAPI.Models.Entities;
using MainMarket.Services.ProductAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace MainMarket.Services.ProductAPI.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly ProductContext _context;

    public IGenericRepository<Product> ProductRepository { get; private set; }

    public IGenericRepository<Image> ImageRepository { get; private set; }

    public IGenericRepository<Brand> BrandRepository { get; private set; }

    public IGenericRepository<Category> CategoryRepository { get; private set; }

    public UnitOfWork(ProductContext context)
    {
        _context = context;
        ProductRepository = new GenericRepository<Product>(_context);
        ImageRepository = new GenericRepository<Image>(_context);
        BrandRepository = new GenericRepository<Brand>(_context);
        CategoryRepository = new GenericRepository<Category>(_context);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitAsync(IDbContextTransaction transaction)
    {
        await transaction.CommitAsync();
    }
}