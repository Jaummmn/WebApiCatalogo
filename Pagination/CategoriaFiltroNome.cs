namespace WebApiCurso.Pagination;

public class CategoriaFiltroNome : QueryStringPagination
{
    public string? Nome { get; set; }
}