using System.Linq.Expressions;
using WebApiCurso.DTOs;
using WebApiCurso.Models;

namespace WebApiCurso.Repositories.Interfaces;

public interface IRepository<T> 
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetAync(Expression<Func<T, bool>> predicate);
    T Create(T entity);
    T Update(T entity);
    T Delete(T entity);
}

