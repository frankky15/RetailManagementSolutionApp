using RMS.Interfaces;

namespace RMS.Models;

public partial class Brand : IEntityModel
{
    public bool IsValid()
    {
        if (BrandId == 0)
            return false;
        
        if (string.IsNullOrEmpty(BrandName))
            return false;

        return true;
    }
}
