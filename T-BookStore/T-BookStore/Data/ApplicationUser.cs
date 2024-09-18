using Microsoft.AspNetCore.Identity;

namespace T_BookStore.Data
{
    public class ApplicationUser : IdentityUser
    {
        public String FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
    }
}
