using System.Linq.Expressions;
using RMS.Data;
using RMS.Interfaces;

using RMS.Models;

namespace RMS.Repository;

public class ProductRepository : RepositoryBase<Product>, IProductRepository
{
    public ProductRepository(DataContext dataContext) : base(dataContext)
    {

    }
}

public interface IProductRepository : IRepository<Product>
{

}
