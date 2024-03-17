using RMS.Interfaces;

namespace RMS.Models;

public partial class Order : IEntityModel
{
    public bool IsValid()
    {
        if (OrderId == 0)
            return false;

        if (StoreId == 0)
            return false;

        if (StaffId == 0)
            return false;

        return true;
    }
}
