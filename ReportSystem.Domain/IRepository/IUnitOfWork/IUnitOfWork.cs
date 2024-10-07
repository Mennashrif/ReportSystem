using ReportSystem.Domain.IRepository.IGRepository;

namespace ReportSystem.Domain.IRepository.IUnitOfWork
{
    public interface IUnitOfWork
    {
        IGRepository<TEntity> Repository<TEntity>() where TEntity : class;
        Task<int> SaveChangesAsync();
        int SaveChanges();
    }
}
