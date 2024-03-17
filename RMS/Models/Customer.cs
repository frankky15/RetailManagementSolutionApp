using RMS.Interfaces;

namespace RMS.Models;

public partial class Customer : IEntityModel
{
    public bool IsValid()
    {
        if (CustomerId == 0)
            return false;

        if (string.IsNullOrEmpty(FirstName))
            return false;

        if (string.IsNullOrEmpty(LastName))
            return false;

        if (string.IsNullOrEmpty(Email))
            return false;

        return true;
    }
}
