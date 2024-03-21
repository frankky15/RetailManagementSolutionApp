namespace RMS.Models;

public partial class OrderItem : EntityModel
{
    public override bool IsValid()
    {
        if (OrderId == 0)
            return false;

        if (ItemId == 0)
            return false;

        if (ProductId == 0)
            return false;

        return true;
    }
}
