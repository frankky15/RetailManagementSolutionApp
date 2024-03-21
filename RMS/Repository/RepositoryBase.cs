using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Metadata;
using RMS.Data;
using RMS.Interfaces;
using RMS.Models;

namespace RMS.Repository;

public class RepositoryBase<T> : IRepository<T> where T : EntityModel
{
    protected readonly DataContext _dataContext;
    public RepositoryBase(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    public virtual bool Add(T entity)
    {
        if (!entity.IsValid())
        {
            Console.WriteLine("Repository/Error: There was an error while adding entity, The entity is not valid.");
            return false;
        }

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

    public virtual bool Delete(T entity)
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

    public virtual bool Delete(Expression<Func<T, bool>> where)
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

    public virtual IQueryable<T> Get(Expression<Func<T, bool>> where)
    {
        try
        {
            return _dataContext.Set<T>().Where(where);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Repository/Error: There was an error while getting entities. Exeption: {ex.Message}");
            return null;
        }
    }

    public virtual IQueryable<T> GetAll()
    {
        try
        {
            return _dataContext.Set<T>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Repository/Error: There was an error while getting all entities. Exeption: {ex.Message}");
            return null;
        }
    }

    public virtual T GetById(int id)
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

    public virtual bool Update(T entity)
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

    public virtual bool IdExists(int id)
    {
        if (GetById(id) == null)
            return false;

        return true;
    }
}
