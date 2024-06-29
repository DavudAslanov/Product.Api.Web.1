using Microsoft.AspNetCore.Identity;
namespace Entities.Concrete.TableModels.Membership
{
    public class ApplicationUser:IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }
        public string Email { get; set; }

        public byte Gender { get; set; }

        public string Password { get; set; }

    }
}
