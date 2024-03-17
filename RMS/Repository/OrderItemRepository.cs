using System.Linq.Expressions;
using RMS.Data;
using RMS.Interfaces;

using RMS.Models;

namespace RMS.Repository;

public class OrderItemRepository : RepositoryBase<OrderItem>, IOrderItemRepository
{
    public OrderItemRepository(DataContext dataContext) : base(dataContext)
    {

    }
}

public interface IOrderItemRepository : IRepository<OrderItem>
{

}
