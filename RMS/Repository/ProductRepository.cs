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

    public override bool Add(Product entity)
    {
        if (!entity.IsValid())
        {
            Console.WriteLine("Repository/Error: There was an error while adding entity, The entity is not valid.");
            return false;
        }

        using (var transaction = _dataContext.Database.BeginTransaction())
        {
            try
            {
                _dataContext.Set<Product>().Add(entity);
                _dataContext.SaveChanges();

                foreach (var store in _dataContext.Stores)
                {
                    var newStock = new Stock
                    {
                        ProductId = entity.ProductId,
                        StoreId = store.StoreId,
                        Quantity = 0
                    };

                    _dataContext.Set<Stock>().Add(newStock);
                }

                _dataContext.SaveChanges();

                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Console.WriteLine($"Repository/Error: There was an error while adding entity. Exeption: {ex.Message}");
                return false;
            }
        }
    }
}

public interface IProductRepository : IRepository<Product>
{

}
