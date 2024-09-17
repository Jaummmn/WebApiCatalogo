using WebApiCurso.Context;

namespace WebApiCurso.Repositories.Interfaces;

public interface IUnityOfWork
{
    IProdutosRepository ProdutoRepository { get; }
    ICategoriaRepository CategoriaRepository { get; }
   
    void Commit();
}