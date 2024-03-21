namespace RMS.Models;

public partial class Customer : EntityModel
{
    public override bool IsValid()
    {
        if (string.IsNullOrEmpty(FirstName))
            return false;

        if (string.IsNullOrEmpty(LastName))
            return false;

        if (string.IsNullOrEmpty(Email))
            return false;

        return true;
    }
}
