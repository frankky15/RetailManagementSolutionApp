namespace RMS.Models;

public partial class Stock : EntityModel
{
    public override bool IsValid()
    {
        if (StoreId == 0)
            return false;

        if (ProductId == 0)
            return false;

        return true;
    }
}
