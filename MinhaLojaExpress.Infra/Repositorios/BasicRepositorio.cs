using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MinhaLojaExpress.Dominio.Interfaces;
using MinhaLojaExpress.Infra.Contexto;

namespace MinhaLojaExpress.Infra.Repositorios
{
    public abstract class BasicRepositorio<T> : IRepositorio<T> where T : class
    {
        private readonly DbSet<T> _set;
        private readonly MinhaLojaExpressContext _context;
        
        protected BasicRepositorio(MinhaLojaExpressContext context)
        {
            _context = context;
            _set = context.Set<T>();
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            var obj = await _set.FindAsync(id);
            return obj;
        }

        public async Task<T?> GetSingleAsync(Expression<Func<T, bool>> predicate)
        {
            var obj = await _set.FirstAsync(predicate);
            return obj;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _set.ToListAsync();
        }

        public async Task<List<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            var objs = await _set.Where(predicate).ToListAsync();
            return objs;
        }

        public async Task AddAsync(T request)
        {
            await _set.AddAsync(request);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var obj = await _set.FindAsync(id);
            _set.Remove(obj!);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(T request)
        {
            _set.Update(request);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateRangeAsync(ICollection<T> request)
        {
            _set.UpdateRange(request);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteRangeAsync(ICollection<T> request)
        {
            _set.RemoveRange(request);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddRangeAsync(ICollection<T> request)
        {
            await _set.AddRangeAsync(request);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
