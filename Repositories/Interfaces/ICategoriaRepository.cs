using WebApiCurso.Models;
using WebApiCurso.Pagination;
using X.PagedList;

namespace WebApiCurso.Repositories.Interfaces
{
    public interface ICategoriaRepository : IRepository<Categoria>
    {
        IEnumerable<Categoria> GetCategoriasProdutosAsync(int id);
        Task<IPagedList<Categoria>> GetCategoriasAsync(CategoriaParameters categoriasParams);
        Task<IPagedList<Categoria>> GetCategoriasFiltroNomeAsync(CategoriaFiltroNome categoriasParams);
    }
}