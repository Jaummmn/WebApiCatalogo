using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApiCurso.DTOs;
using WebApiCurso.DTOs.Mappings;
using WebApiCurso.Models;
using WebApiCurso.Pagination;
using WebApiCurso.Repositories.Interfaces;
using X.PagedList;

namespace WebApiCurso.Controllers;

public class CategoriasController : ControllerBase
{
    private readonly IUnityOfWork _uof;

    public CategoriasController(IUnityOfWork uof)
    {
        _uof = uof;
    }


    private ActionResult<IEnumerable<CategoriaDTO>> CategoriaNomeDTO(IPagedList<Categoria> categorias)
    {
        var metadata = new
        {
            categorias.Count,
            categorias.PageSize,
            categorias.PageCount,
            categorias.TotalItemCount,
            categorias.HasNextPage,
            categorias.HasPreviousPage
        };

        Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metadata));
        var categoriasDto = categorias.ToCategoriaDtosList();
        return Ok(categoriasDto);
    }


    [HttpGet("FiltroNomeCategoria")]
    public async Task<ActionResult<IEnumerable<CategoriaDTO>>> GetCategoriaNome(
        [FromQuery] CategoriaFiltroNome categoriaFiltroNome)
    {
        var categorias = await _uof.CategoriaRepository.GetCategoriasFiltroNomeAsync(categoriaFiltroNome);
        return CategoriaNomeDTO(categorias);
    }
    
    [HttpGet("pagination")]
    public async Task<ActionResult<IEnumerable<CategoriaDTO>>> GetCategoriasPorPagina(
        [FromQuery] CategoriaParameters getCategoriasPorPagina)
    {
        var categorias = await _uof.CategoriaRepository.GetCategoriasAsync(getCategoriasPorPagina);
        return CategoriaNomeDTO(categorias);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoriaDTO>>> Get()
    {
        var categorias = await _uof.CategoriaRepository.GetAllAsync();
        var categoriasDto = categorias.ToCategoriaDtosList();

        return Ok(categoriasDto);
    }

    [HttpGet("ObterCategoriaProdutos/{id:int}", Name = "ObterCategoriaProdutos")]
    public ActionResult<CategoriaDTO> GetCategoriasProdutos(int id)
    {
        var categoria = _uof.CategoriaRepository.GetCategoriasProdutosAsync(id).FirstOrDefault();

        if (categoria == null)
        {
            return NotFound();
        }

        var categoriaDTO = categoria.ToCategoriaDto();
        return Ok(categoriaDTO);
    }

    [HttpGet("{id:int:min(1)}", Name = "ObterCategoria")]
    public async Task<ActionResult<CategoriaDTO>> GetCategoriaId(int id)
    {
        var categoria = await _uof.CategoriaRepository.GetAync(c => c.Categoriaid == id);

        if (categoria is null)
        {
            return NotFound();
        }

        var categoriaDTO = categoria.ToCategoriaDto();
        return Ok(categoriaDTO);
    }

    [HttpPost("PostCategoria")]
    public ActionResult<CategoriaDTO> PostCategorias(Categoria categoria)
    {
        if (categoria is null)
        {
            return BadRequest();
        }

        _uof.CategoriaRepository.Create(categoria);
        _uof.Commit();
        var categoriaDto = categoria.ToCategoriaDto();
        return new CreatedAtRouteResult("ObterCategoria",
            new { id = categoriaDto.Categoriaid }, categoriaDto);
    }

    [HttpPut("{id:int:min(1)}")]
    public ActionResult<CategoriaDTO> PutCategoria(int id, Categoria categoria)
    {
        if (categoria.Categoriaid != id)
        {
            return BadRequest();
        }

        var categoriaAtualizada = _uof.CategoriaRepository.Update(categoria);
        _uof.Commit();
        var categoriaDtoAtualizada = categoriaAtualizada.ToCategoriaDto();
        return Ok(categoriaDtoAtualizada);
    }

    [HttpDelete("{id:int:min(1)}")]
    public async Task<ActionResult<CategoriaDTO>> DeleteCategoria(int id)
    {
        var categoria = await _uof.CategoriaRepository.GetAync(c => c.Categoriaid == id);
        if (categoria is null)
        {
            return NotFound();
        }

        var categoriaExcluida = _uof.CategoriaRepository.Delete(categoria);
        _uof.Commit();
        var categoriaDTo = categoriaExcluida.ToCategoriaDto();
        return Ok(categoriaDTo);
    }
}