using RMS.Interfaces;

namespace RMS.Models;

public partial class Store : IEntityModel
{
    public bool IsValid()
    {
        if (StoreId == 0)
            return false;
            
        if (string.IsNullOrEmpty(StoreName))
            return false;

        return true;
    }
}