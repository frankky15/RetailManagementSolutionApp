using RMS.Interfaces;

namespace RMS.Models;

public partial class Staff : IEntityModel
{
    public bool IsValid()
    {
        if (StaffId == 0)
            return false;
        
        if (StoreId == 0)
            return false;
        
        if (ManagerId == 0)
            return false;

        if (string.IsNullOrEmpty(FirstName))
            return false;
            
        if (string.IsNullOrEmpty(LastName))
            return false;
        
        if (string.IsNullOrEmpty(Email))
            return false;

        return true;
    }
}