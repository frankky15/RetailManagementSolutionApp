using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RMS.Data;
using RMS.Interfaces;

using RMS.Models;

namespace RMS.Repository;

public class OrderRepository : RepositoryBase<Order>, IOrderRepository
{
    public OrderRepository(DataContext dataContext) : base(dataContext)
    {

    }

    public override bool Add(Order entity)
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
                _dataContext.Set<Order>().Add(entity);
                _dataContext.SaveChanges();

                foreach (var item in entity.OrderItems)
                {
                    var product = _dataContext.Products.Where(p => p.ProductId == item.ProductId)
                        .Include(p => p.Stocks).First();

                    var stock = product.Stocks.Where(s => s.StoreId == entity.StoreId).First();
                    stock.Quantity -= item.Quantity;

                    _dataContext.SaveChanges();
                }

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

    public override bool Delete(Order entity)
    {
        using (var transaction = _dataContext.Database.BeginTransaction())
        {
            try
            {
                foreach (var item in entity.OrderItems)
                {
                    var product = _dataContext.Products.Where(p => p.ProductId == item.ProductId)
                        .Include(p => p.Stocks).First();

                    var stock = product.Stocks.Where(s => s.StoreId == entity.StoreId).First();
                    stock.Quantity += item.Quantity;

                    _dataContext.SaveChanges();
                }

                _dataContext.Set<Order>().Remove(entity);
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

public interface IOrderRepository : IRepository<Order>
{

}
