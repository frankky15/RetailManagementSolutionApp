using System.Linq.Expressions;
using RMS.Models;

namespace RMS.Interfaces;

public interface IRepository<T> where T : EntityModel
{
    bool Add(T entity);
    bool Update(T entity);
    bool Delete(T entity);
    bool Delete(Expression<Func<T, bool>> where);
    T GetById(int id);
    IQueryable<T> Get(Expression<Func<T, bool>> where);
    IQueryable<T> GetAll();
    bool IdExists(int id);
}
