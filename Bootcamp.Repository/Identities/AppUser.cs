using Microsoft.AspNetCore.Identity;

namespace Bootcamp.Repository.Identities
{
    public class AppUser : IdentityUser<Guid>
    {
        public string Name { get; set; } = default!;
        public string Surname { get; set; } = default!;
    }
}