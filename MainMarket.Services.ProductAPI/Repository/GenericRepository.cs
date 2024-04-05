using MainMarket.Services.ProductAPI.Data;
using MainMarket.Services.ProductAPI.Exceptions;
using MainMarket.Services.ProductAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MainMarket.Services.ProductAPI.Repository;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly ProductContext _context;
    private readonly DbSet<T> _dbSet;

    public GenericRepository(ProductContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = _context.Set<T>();
    }

    public async Task<T> Create(T entity)
    {
        var entityEntry = await _dbSet.AddAsync(entity);
        return entityEntry.Entity;
    }

    public async Task<bool> Delete(string Id)
    {
        var entity = await GetById(Id);
        if (entity == null)
            return false;

        _dbSet.Remove(entity);
        return true;
    }

    public async Task<IQueryable<T>> GetAll(Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IQueryable<T>>? includeFunc = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null)
    {
        var query = _dbSet
        .AsNoTracking()
        .AsQueryable();

        query = filter != null ? query.Where(filter) : query;
        query = orderBy != null ? orderBy(query) : query;

        if (includeFunc != null)
        {
            query = includeFunc(query);
        }
        return query;
    }

    public async Task<T> GetById(string Id)
    {
        return await _dbSet.FindAsync(Id) ?? throw new BadRequestException($"entity with id {Id} not found ");
    }

    public async Task<T> Update(T entity, string Id)
    {
        var existingEntity = await _dbSet.FindAsync(Id) ?? throw new BadRequestException($"entity with id {Id} not found ");

        _context.Entry(existingEntity).CurrentValues.SetValues(entity);
        return existingEntity;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}