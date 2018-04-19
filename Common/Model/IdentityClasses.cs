using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace Common.Model
{
    public class ApplicationUser : IdentityUser<long>
    {
        public IQueryable<UserMessage> UserMessages { get; set; }
    }
    
    public class CustomUserRole : IdentityUserRole<long> { }
    public class CustomUserClaim : IdentityUserClaim<long> { }
    public class CustomUserLogin : IdentityUserLogin<long> { }
    
    public class ApplicationRole : IdentityRole<long>
    {
    }
    
    public class CustomRole : IdentityRole<int>
    {
        public CustomRole() { }
        public CustomRole(string name) { Name = name; }
    }
}