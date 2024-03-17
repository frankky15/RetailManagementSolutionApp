using System.Linq.Expressions;
using RMS.Data;
using RMS.Interfaces;

using RMS.Models;

namespace RMS.Repository;

public class BrandRepository : RepositoryBase<Brand>, IBrandRepository
{
    public BrandRepository(DataContext dataContext) : base(dataContext)
    {

    }
}

public interface IBrandRepository : IRepository<Brand>
{

}
