using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace WebApiCurso.Models;

public class Categoria
{
    public Categoria()
    {
        produtos = new Collection<Produto>();
    }

    public int Categoriaid { get; set; }

    [StringLength(80, ErrorMessage = "Maximo de caracteres permitido atingido")]
    public string? CategoriaNome { get; set; }

    [StringLength(80, ErrorMessage = "Maximo de caracteres permitido atingido")]
    public string? CategoriaDescricao { get; set; }

    public ICollection<Produto> produtos { get; set; }
}