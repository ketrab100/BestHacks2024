using BestHacks2024.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace BestHacks2024.Database.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        public BestHacksDbContext _context;
        public DbSet<T> table;
        public GenericRepository(BestHacksDbContext _context)
        {
            this._context = _context;
            table = _context.Set<T>();
        }
        public async Task<IEnumerable<T>> GetAll()
        {
            return await table.ToListAsync();
        }
        public async Task<T?> GetById(Guid id)
        {
            return await table.FindAsync(id);
        }
        public async Task<T> Insert(T obj)
        {
            await table.AddAsync(obj);
            return obj;
        }
        public void Update(T obj)
        {
            table.Update(obj);
        }
        public async Task<T> Delete(Guid id)
        {
            var existing = await table.FindAsync(id);
            if (existing == null) throw new ApplicationException();
            table.Remove(existing);
            return existing;
        }
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
