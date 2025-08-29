using MinhaLojaExpress.Dominio.Entidades;

namespace MinhaLojaExpress.Dominio.Interfaces
{
    public interface IItemRepositorio : IRepositorio<Item>
    {
        Task<IEnumerable<string>> ListarExistentes(IEnumerable<string> itemsId);
    }
}
