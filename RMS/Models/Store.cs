namespace RMS.Models;

public partial class Store : EntityModel
{
    public override bool IsValid()
    {            
        if (string.IsNullOrEmpty(StoreName))
            return false;

        return true;
    }
}
