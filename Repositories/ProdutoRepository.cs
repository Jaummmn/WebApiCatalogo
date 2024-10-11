using WebApiCurso.Context;
using WebApiCurso.Models;
using WebApiCurso.Pagination;
using WebApiCurso.Repositories.Interfaces;
using X.PagedList.Extensions;

namespace WebApiCurso.Repositories;

public class ProdutoRepository : Repository<Produto>, IProdutosRepository
{
    public ProdutoRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<PagedList<Produto>> ObterProdutoFiltroNome(ProdutosParameters produtosParameters)
    {
        var produtos = await GetAllAsync();
        if (!string.IsNullOrWhiteSpace(produtosParameters.nome))
        {
            produtos = produtos.Where(c => c.ProdutoNome.ToLower().Contains(produtosParameters.nome.ToLower()));
        }

        var produtosPaginados = PagedList<Produto>
            .ToPagedList(produtos.AsQueryable(),
                produtosParameters.PageNumber,
                produtosParameters.PageSize);
        
        return produtosPaginados;
    }

    public async Task<IEnumerable<Produto>> GetProdutosPorCategoriaAsync(int id)
    {
        var produtos = await GetAllAsync();
        var produtosCategorias = produtos.Where(c => c.CategoriaId == id);
        return produtosCategorias;
    }

    public async Task<IEnumerable<Produto>> GetProdutosPaginationAsync(ProdutosParameters produtosParameters)
    {
        var produtos = await GetAllAsync();
        var produtosOrdenados = produtos.OrderBy(n => n.ProdutoNome)
            .Skip((produtosParameters.PageNumber - 1) * produtosParameters.PageSize)
            .Take(produtosParameters.PageSize);
        return produtosOrdenados;
    }

    public async Task<PagedList<Produto>> GetprodutosPorPaginaAsync(ProdutosParameters produtosParameters)
    {
        var produtos = await GetAllAsync();
        var produtosOrdenados = produtos.OrderBy(p => p.ProdutoId);
        var produtosPaginados = PagedList<Produto>
            .ToPagedList(produtos.AsQueryable(),
                produtosParameters.PageNumber,
                produtosParameters.PageSize);
        return produtosPaginados;
    }

    public async Task<PagedList<Produto>> GetProdutosFiltroPrecoAsync(ProdutosFiltroPreco produtosFiltroPreco)
    {
        var produtos = await GetAllAsync();
        if (produtosFiltroPreco.Preco.HasValue && !string.IsNullOrEmpty(produtosFiltroPreco.PrecoCriterio))
        {
            if (produtosFiltroPreco.PrecoCriterio.Equals("maior", StringComparison.OrdinalIgnoreCase))
            {
                produtos = produtos.Where(p => p.PrecoProduto > produtosFiltroPreco.Preco.Value)
                    .OrderBy(p => p.PrecoProduto);
            }
            else if (produtosFiltroPreco.PrecoCriterio.Equals("menor", StringComparison.OrdinalIgnoreCase))
            {
                produtos = produtos.Where(p => p.PrecoProduto < produtosFiltroPreco.Preco.Value)
                    .OrderBy(p => p.PrecoProduto);
            }
            else
            {
                produtos = produtos.Where(p => p.PrecoProduto == produtosFiltroPreco.Preco.Value)
                    .OrderBy(p => p.PrecoProduto);
            }
        }

        var produtosFiltrados =
            PagedList<Produto>.ToPagedList(produtos.AsQueryable(), produtosFiltroPreco.PageNumber,
                produtosFiltroPreco.PageSize);
        return produtosFiltrados;
    }
}