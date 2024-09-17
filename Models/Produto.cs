

namespace WebApiCurso.Models;

public class Produto
{
  

  
    public int ProdutoId { get; set; }
    public string? ProdutoNome { get; set; }
    public string? ImageUrl { get; set; } 
    public decimal PrecoProduto { get; set; }
    public bool EmEstoque { get; set; }
    public DateTime DataCadastro { get; set; }

    public int CategoriaId { get; set; }
    [System.Text.Json.Serialization.JsonIgnore]
    public Categoria? Categoria { get; set; }
}