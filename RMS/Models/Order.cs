namespace RMS.Models;

public partial class Order : EntityModel
{
    public override bool IsValid()
    {
        if (StoreId == 0)
            return false;

        if (StaffId == 0)
            return false;

        return true;
    }
}
