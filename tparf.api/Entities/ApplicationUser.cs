using Microsoft.AspNetCore.Identity;

namespace tparf.api.Entities
{
    public class ApplicationUser : IdentityUser<long>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
    }
}
