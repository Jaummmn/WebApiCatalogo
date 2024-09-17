namespace WebApiCurso.Pagination;

public class ProdutosFiltroPreco : QueryStringPagination
{
    public decimal? Preco { get; set; }
    public string? PrecoCriterio { get; set; }
}