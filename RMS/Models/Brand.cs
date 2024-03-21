namespace RMS.Models;

public partial class Brand : EntityModel
{
    public override bool IsValid()
    {        
        if (string.IsNullOrEmpty(BrandName))
            return false;

        return true;
    }
}
