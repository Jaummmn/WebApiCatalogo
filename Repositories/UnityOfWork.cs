using WebApiCurso.Context;
using WebApiCurso.Repositories.Interfaces;

namespace WebApiCurso.Repositories;

public class UnityOfWork : IUnityOfWork
{
    private IProdutosRepository? _produtoRepo;
    private ICategoriaRepository? _categoriaRepo;
    public AppDbContext _context;
     
    public UnityOfWork(AppDbContext context)
    {
        _context = context;
    }
//nao foi criada no construtor para nao criar varias instancias 

    public IProdutosRepository ProdutoRepository  
    {
        get
        {
            return _produtoRepo = _produtoRepo ?? new ProdutoRepository(_context);
        }
    }

    public ICategoriaRepository CategoriaRepository
    {
        get
        {
            return _categoriaRepo = _categoriaRepo ?? new CategoriaRepository(_context);
        }
    }
    public  void Commit()
    {
     _context.SaveChanges();
    }

    public void Dispose()
    {
        _context.Dispose(); 
    }
}