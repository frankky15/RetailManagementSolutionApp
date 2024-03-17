using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using RMS.Interfaces;

namespace RMS.Models;

public partial class OrderItem : IEntityModel
{
    public bool IsValid()
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