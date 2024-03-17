using System.Linq.Expressions;
using RMS.Data;
using RMS.Interfaces;

using RMS.Models;

namespace RMS.Repository;

public class OrderRepository : RepositoryBase<Order>, IOrderRepository
{
    public OrderRepository(DataContext dataContext) : base(dataContext)
    {

    }
}

public interface IOrderRepository : IRepository<Order>
{

}
