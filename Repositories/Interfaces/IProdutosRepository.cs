using WebApiCurso.Models;
using WebApiCurso.Pagination;
using X.PagedList;

namespace WebApiCurso.Repositories.Interfaces;

public interface IProdutosRepository : IRepository<Produto>
{
    Task<IEnumerable<Produto>> GetProdutosPorCategoriaAsync(int id);
    Task<IEnumerable<Produto>> GetProdutosPaginationAsync(ProdutosParameters produtosParameters);
    Task<Pagination.PagedList<Produto>> GetprodutosPorPaginaAsync(ProdutosParameters produtosParameters);
    Task<Pagination.PagedList<Produto>> GetProdutosFiltroPrecoAsync(ProdutosFiltroPreco produtosFiltroPreco);
    Task<Pagination.PagedList<Produto>> ObterProdutoFiltroNome(ProdutosParameters produtosParameters);
}