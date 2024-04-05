using MainMarket.Services.ProductAPI.Models.Entities;
using Microsoft.EntityFrameworkCore.Storage;

namespace MainMarket.Services.ProductAPI.Repository.Interfaces;

public interface IUnitOfWork
{
    public IGenericRepository<Product> ProductRepository { get; }

    public IGenericRepository<Image> ImageRepository { get; }

    public IGenericRepository<Brand> BrandRepository { get; }

    public IGenericRepository<Category> CategoryRepository { get; }

    Task<int> SaveChangesAsync();

    Task<IDbContextTransaction> BeginTransactionAsync();

    Task CommitAsync(IDbContextTransaction transaction);
}