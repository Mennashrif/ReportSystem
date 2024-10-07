using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ReportSystem.Domain.IRepository.IGRepository;
using ReportSystem.Infrastructure.Context;
using System.Linq.Expressions;

namespace ReportSystem.Infrastructure.Repository.GRepository
{
    public class GRepository<T> : IGRepository<T> where T : class
    {
        protected DatabaseContext _context;
        public ILogger _logger { get; set; }
        public GRepository(DatabaseContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<T> GetByIdAsync(int id) => await _context.Set<T>().FindAsync(id);
        public IQueryable<T> FindAllNoTracking(Expression<Func<T, bool>> criteria) => _context.Set<T>().Where(criteria).AsNoTracking();
        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }

        public T Update(T entity)
        {
            _context.Update(entity);
            return entity;
        }
    }
}
