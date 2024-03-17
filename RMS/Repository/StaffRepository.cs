using System.Linq.Expressions;
using RMS.Data;
using RMS.Interfaces;

using RMS.Models;

namespace RMS.Repository;

public class StaffRepository : RepositoryBase<Staff>, IStaffRepository
{
    public StaffRepository(DataContext dataContext) : base(dataContext)
    {

    }
}

public interface IStaffRepository : IRepository<Staff>
{

}
