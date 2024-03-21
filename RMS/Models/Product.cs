namespace RMS.Models;

public partial class Product : EntityModel
{
    public override bool IsValid()
    {
        if (BrandId == 0)
            return false;

        if (CategoryId == 0)
            return false;

        if (string.IsNullOrEmpty(ProductName))
            return false;

        return true;
    }
}
