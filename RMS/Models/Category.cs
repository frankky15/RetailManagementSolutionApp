namespace RMS.Models;

public partial class Category : EntityModel
{
    public override bool IsValid()
    {
        if (string.IsNullOrEmpty(CategoryName))
            return false;

        return true;
    }
}
