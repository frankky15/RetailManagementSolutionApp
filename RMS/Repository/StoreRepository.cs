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
}

public interface IStoreRepository : IRepository<Store>
{

}
