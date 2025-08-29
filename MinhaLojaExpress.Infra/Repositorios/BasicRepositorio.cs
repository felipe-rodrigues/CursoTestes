using Microsoft.EntityFrameworkCore;
using MinhaLojaExpress.Dominio.Interfaces;
using MinhaLojaExpress.Infra.Contexto;

namespace MinhaLojaExpress.Infra.Repositorios
{
    public abstract class BasicRepositorio<T>(MinhaLojaExpressContext context) : IRepositorio<T>
        where T : class
    {
        private readonly DbSet<T> _set = context.Set<T>();

        public virtual async Task<T?> GetByIdAsync(Guid id)
        {
            var obj = await _set.FindAsync(id);
            return obj;
        }

        public async Task<T?> GetSingleAsync(Func<T, bool> predicate)
        {
            var obj = await _set.FindAsync(predicate);
            return obj;
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _set.ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> GetAsync(Func<T, bool> predicate)
        {
            var objs = _set.Where(predicate).ToList();
            return await Task.FromResult(objs);
        }

        public virtual async Task AddAsync(T request)
        {
            await _set.AddAsync(request);
            await context.SaveChangesAsync();
        }

        public virtual async Task<bool> DeleteAsync(Guid id)
        {
            var obj = await _set.FindAsync(id);
            _set.Remove(obj!);
            await context.SaveChangesAsync();
            return true;
        }

        public virtual async Task<bool> UpdateAsync(T request)
        {
            _set.Update(request);
            await context.SaveChangesAsync();
            return true;
        }

        public virtual async Task<bool> UpdateRangeAsync(ICollection<T> request)
        {
            _set.UpdateRange(request);
            await context.SaveChangesAsync();
            return true;
        }

        public virtual async Task<bool> DeleteRangeAsync(ICollection<T> request)
        {
            _set.RemoveRange(request);
            await context.SaveChangesAsync();
            return true;
        }

        public virtual async Task<bool> AddRangeAsync(ICollection<T> request)
        {
            await _set.AddRangeAsync(request);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
