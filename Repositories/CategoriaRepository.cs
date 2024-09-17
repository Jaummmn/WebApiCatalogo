using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiCurso.Context;
using WebApiCurso.Models;
using WebApiCurso.Pagination;
using WebApiCurso.Repositories.Interfaces;
using X.PagedList;
using X.PagedList.Extensions;

namespace WebApiCurso.Repositories;

public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
{
    public CategoriaRepository(AppDbContext context) : base(context)
    {
    }


    public IEnumerable<Categoria> GetCategoriasProdutosAsync(int id)
    {
        return _context.Categorias
            .Include(c => c.produtos)
            .Where(c => c.Categoriaid == id).ToList();
    }

    public async Task<IPagedList<Categoria>> GetCategoriasAsync(CategoriaParameters categoriasParams)
    {
        var categorias = await GetAllAsync();

        // Agora, de forma assíncrona
        var categoriasOrdenadas = categorias.OrderBy(p => p.Categoriaid).ToList();

        var resultado = categoriasOrdenadas.ToPagedList(categoriasParams.PageNumber,
            categoriasParams.PageSize);

        return resultado;
    }

    public async Task<IPagedList<Categoria>> GetCategoriasFiltroNomeAsync(CategoriaFiltroNome categoriasParams)
    {
        var categorias = await GetAllAsync();

        if (!string.IsNullOrEmpty(categoriasParams.Nome))
        {
            categorias = categorias.Where(c => c.CategoriaNome.Contains(categoriasParams.Nome));
        }

        var categoriasFiltradas =  categorias.ToPagedList(
            categoriasParams.PageNumber,
            categoriasParams.PageSize);

        return categoriasFiltradas;
    }
}