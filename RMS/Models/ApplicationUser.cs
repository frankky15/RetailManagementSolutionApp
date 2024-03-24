using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace RMS.Models;

public class ApplicationUser : IdentityUser
{
    [Column("staff_id")]
    public int? StaffId { get; set; }

    [ForeignKey("StaffId")]
    public Staff? Staff { get; set; } = null!;
}
