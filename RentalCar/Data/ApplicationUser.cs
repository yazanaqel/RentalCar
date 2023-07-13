using Microsoft.AspNetCore.Identity;
using RentalCar.Models;
using System.ComponentModel.DataAnnotations;

namespace RentalCar.Data;

public class ApplicationUser : IdentityUser
{
    [Required, MaxLength(100)]
    public string FirstName { get; set; } = string.Empty;

    [Required, MaxLength(100)]
    public string LastName { get; set; } = string.Empty;

    public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }

}
