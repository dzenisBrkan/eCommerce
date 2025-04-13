using Microsoft.AspNetCore.Identity;

namespace AbySalto.Mid.Domain.Entities;

public class User : IdentityUser<int>
{
    //public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Location { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }

    public ICollection<UserRole> UserRoles { get; set; }
}
