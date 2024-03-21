namespace RMS.Models;

public partial class Staff : EntityModel
{
    public override bool IsValid()
    {        
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
