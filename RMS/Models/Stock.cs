using RMS.Interfaces;

namespace RMS.Models;

public partial class Stock : IEntityModel
{
    public bool IsValid()
    {
        if (StoreId == 0)
            return false;

        if (ProductId == 0)
            return false;

        return true;
    }
}
