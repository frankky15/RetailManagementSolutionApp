using RMS.Interfaces;

namespace RMS.Models;

public partial class Category : IEntityModel
{
    public bool IsValid()
    {
        if (CategoryId == 0)
            return false;

        if (string.IsNullOrEmpty(CategoryName))
            return false;

        return true;
    }
}
