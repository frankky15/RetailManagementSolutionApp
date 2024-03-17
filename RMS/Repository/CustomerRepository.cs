using System.Linq.Expressions;
using RMS.Data;
using RMS.Interfaces;

using RMS.Models;

namespace RMS.Repository;

public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
{
    public CustomerRepository(DataContext dataContext) : base(dataContext)
    {

    }
}

public interface ICustomerRepository : IRepository<Customer>
{

}
