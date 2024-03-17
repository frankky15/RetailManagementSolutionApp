using System.Linq.Expressions;
using RMS.Data;
using RMS.Interfaces;

using RMS.Models;

namespace RMS.Repository;

public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
{
    public CategoryRepository(DataContext dataContext) : base(dataContext)
    {

    }
}

public interface ICategoryRepository : IRepository<Category>
{

}
