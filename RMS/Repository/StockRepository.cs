using System.Linq.Expressions;
using RMS.Data;
using RMS.Interfaces;

using RMS.Models;

namespace RMS.Repository;

public class StockRepository : RepositoryBase<Stock>, IStockRepository
{
    public StockRepository(DataContext dataContext) : base(dataContext)
    {

    }
}

public interface IStockRepository : IRepository<Stock>
{

}
