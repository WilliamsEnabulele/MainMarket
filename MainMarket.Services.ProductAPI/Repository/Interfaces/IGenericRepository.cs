using System.Linq.Expressions;

namespace MainMarket.Services.ProductAPI.Repository.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> Create(T entity);

        Task<T> Update(T entity, string Id);

        Task<bool> Delete(string Id);

        Task<IQueryable<T>> GetAll(Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IQueryable<T>>? includeFunc = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null);

        Task<T> GetById(string Id);

        Task SaveChangesAsync();
    }
}