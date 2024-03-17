using RMS.Interfaces;

namespace RMS.Models;

public partial class Product : IEntityModel
{
    public bool IsValid()
    {
        // if (ProductId == 0)
        //     return false;

        if (BrandId == 0)
            return false;

        if (CategoryId == 0)
            return false;

        if (string.IsNullOrEmpty(ProductName))
            return false;

        return true;
    }
}