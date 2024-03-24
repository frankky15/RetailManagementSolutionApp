using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models;

public partial class Staff : EntityModel
{
    [Column("user_id")]
    public string? UserId { get; set; }
    public ApplicationUser? User { get; set; }

    public override bool IsValid()
    {
        if (StoreId == 0)
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
