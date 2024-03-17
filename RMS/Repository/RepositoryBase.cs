using System.Linq.Expressions;
using RMS.Data;
using RMS.Interfaces;

namespace RMS.Repository;

public class RepositoryBase<T> : IRepository<T> where T : class
{
    private readonly DataContext _dataContext;
    public RepositoryBase(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    public bool Add(T entity)
    {
        try
        {
            _dataContext.Set<T>().Add(entity);
            _dataContext.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Repository/Error: There was an error while adding entity. Exeption: {ex.Message}");
            return false;
        }
    }

    public bool Delete(T entity)
    {
        try
        {
            _dataContext.Set<T>().Remove(entity);
            _dataContext.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Repository/Error: There was an error while deleting entity. Exeption: {ex.Message}");
            return false;
        }
    }

    public bool Delete(Expression<Func<T, bool>> where)
    {
        try
        {
            var entitiesToDelete = _dataContext.Set<T>().Where(where);
            _dataContext.Set<T>().RemoveRange(entitiesToDelete);
            _dataContext.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Repository/Error: There was an error while deleting entities. Exeption: {ex.Message}");
            return false;
        }
    }

    public ICollection<T> Get(Expression<Func<T, bool>> where)
    {
        try
        {
            return _dataContext.Set<T>().Where(where).ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Repository/Error: There was an error while getting entities. Exeption: {ex.Message}");
            return null;
        }
    }

    public ICollection<T> GetAll()
    {
        try
        {
            return _dataContext.Set<T>().ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Repository/Error: There was an error while getting all entities. Exeption: {ex.Message}");
            return null;
        }
    }

    public T GetById(int id)
    {
        try
        {
            return _dataContext.Set<T>().Find(id);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Repository/Error: There was an error while getting entity by Id. Exeption: {ex.Message}");
            return null;
        }
    }

    public bool Update(T entity)
    {
        try
        {
            _dataContext.Set<T>().Update(entity);
            _dataContext.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Repository/Error: There was an error while updating entity. Exeption: {ex.Message}");
            return false;
        }
    }
}
