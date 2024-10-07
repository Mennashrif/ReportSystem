
using Microsoft.Extensions.Logging;
using ReportSystem.Domain.IRepository.IGRepository;
using ReportSystem.Domain.IRepository.IUnitOfWork;
using ReportSystem.Infrastructure.Context;
using ReportSystem.Infrastructure.Repository.GRepository;

namespace ReportSystem.Infrastructure.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly DatabaseContext _context;
        private readonly ILogger<UnitOfWork> _logger;
        public UnitOfWork(DatabaseContext databaseContext, ILogger<UnitOfWork> logger)
        {
            _context = databaseContext;
            _logger = logger;
        }
        public IGRepository<TEntity> Repository<TEntity>() where TEntity : class => new GRepository<TEntity>(_context, _logger);

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public void Dispose() => _context.Dispose();
    }
}
