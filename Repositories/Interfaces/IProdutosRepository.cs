using WebApiCurso.Models;
using WebApiCurso.Pagination;

namespace WebApiCurso.Repositories.Interfaces;

public interface IProdutosRepository : IRepository<Produto>
{
    Task<IEnumerable<Produto>> GetProdutosPorCategoriaAsync(int id);
    Task<IEnumerable<Produto>> GetProdutosPaginationAsync(ProdutosParameters produtosParameters);
    Task<PagedList<Produto>> GetprodutosPorPaginaAsync(ProdutosParameters produtosParameters);
    Task<PagedList<Produto>> GetProdutosFiltroPrecoAsync(ProdutosFiltroPreco produtosFiltroPreco);
}