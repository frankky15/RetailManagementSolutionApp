using System.Linq.Expressions;

namespace RMS.Interfaces;

public interface IRepository<T> where T : class
{
    bool Add(T entity);
    bool Update(T entity);
    bool Delete(T entity);
    bool Delete(Expression<Func<T, bool>> where);
    T GetById(int id);
    ICollection<T> Get(Expression<Func<T, bool>> where);
    ICollection<T> GetAll();
}
