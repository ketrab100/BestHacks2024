using BestHacks2024.Database.Entities;

namespace BestHacks2024.Database.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity?> GetById(Guid id);
        Task<TEntity> Insert(TEntity obj);
        void Update(TEntity obj);
        Task<TEntity> Delete(Guid id);
        Task Save();
    }
}
