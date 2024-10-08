using WebApiCurso.Models;

namespace WebApiCurso.DTOs.Mappings;

public static class CategoriaDtoMappingExtensions
{
    public static CategoriaDTO? ToCategoriaDto(this Categoria? categoria)
    {
        if (categoria is null)
        {
            return null;
        }

        return new CategoriaDTO
        {
            Categoriaid = categoria.Categoriaid,
            CategoriaDescricao = categoria.CategoriaDescricao,
            CategoriaNome = categoria.CategoriaNome
        };
    }

    public static Categoria? ToCategoria(this CategoriaDTO? categoriaDto)
    {
        if (categoriaDto is null)
        {
            return null;
        }

        return new Categoria
        {
            Categoriaid = categoriaDto.Categoriaid,
            CategoriaDescricao = categoriaDto.CategoriaDescricao,
            CategoriaNome = categoriaDto.CategoriaNome
        };
    }


    public static IEnumerable<CategoriaDTO> ToCategoriaDtosList(this IEnumerable<Categoria>? categorias)
    {
        if (categorias is null || !categorias.Any())
        {
            return new List<CategoriaDTO>();
        }

        return categorias.Select(c => new CategoriaDTO
        {
            Categoriaid = c.Categoriaid,
            CategoriaDescricao = c.CategoriaDescricao,
            CategoriaNome = c.CategoriaNome
        }).ToList();
    }
}