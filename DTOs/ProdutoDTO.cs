namespace WebApiCurso.DTOs;

public class ProdutoDTO
{
    public int ProdutoId { get; set; }
    public string? ProdutoNome { get; set; }
    public string? ImageUrl { get; set; } 
    public decimal PrecoProduto { get; set; }
    public int CategoriaId { get; set; }
}