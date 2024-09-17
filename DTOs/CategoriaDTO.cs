using System.ComponentModel.DataAnnotations;

namespace WebApiCurso.DTOs;

public class CategoriaDTO
{
    public int Categoriaid { get; set; }

    [StringLength(80, ErrorMessage = "Maximo de caracteres permitido atingido")]
    public string? CategoriaNome { get; set; }

    [StringLength(80, ErrorMessage = "Maximo de caracteres permitido atingido")]
    public string? CategoriaDescricao { get; set; }
}