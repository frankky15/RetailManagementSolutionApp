using System.Linq.Expressions;
using RMS.Data;
using RMS.Interfaces;

using RMS.Models;

namespace RMS.Repository;

public class StoreRepository : RepositoryBase<Store>, IStoreRepository
{
    public StoreRepository(DataContext dataContext) : base(dataContext)
    {

    }

     public override bool Add(Store entity)
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
                _dataContext.Set<Store>().Add(entity);
                _dataContext.SaveChanges();

                foreach (var product in _dataContext.Products)
                {
                    var newStock = new Stock
                    {
                        ProductId = product.ProductId,
                        StoreId = entity.StoreId,
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

public interface IStoreRepository : IRepository<Store>
{

}
