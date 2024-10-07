using System.Linq.Expressions;

namespace ReportSystem.Domain.IRepository.IGRepository
{
    public interface IGRepository<T> where T : class
    {

        Task<T> GetByIdAsync(int id);
        IQueryable<T> FindAllNoTracking(Expression<Func<T, bool>> criteria);
        Task<T> AddAsync(T entity);
        T Update(T entity);
    }
}
