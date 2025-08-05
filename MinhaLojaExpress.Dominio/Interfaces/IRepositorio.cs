namespace MinhaLojaExpress.Dominio.Interfaces
{
    public interface IRepositorio<T> where T : class
    {
        Task<T?> GetByIdAsync(Guid id);
        Task<T?> GetSingleAsync(Func<T, bool> predicate);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAsync(Func<T, bool> predicate);
        Task AddAsync(T request);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> UpdateAsync(T request);
        Task<bool> UpdateRangeAsync(ICollection<T> request);
        Task<bool> DeleteRangeAsync(ICollection<T> request);
        Task<bool> AddRangeAsync(ICollection<T> request);
    }
}
