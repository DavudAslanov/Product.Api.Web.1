using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
namespace Entities.Concrete.TableModels.Membership
{
    public class ApplicationUser:IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public byte Gender { get; set; }

        public int Number { get; set; }

        [NotMapped]
        public IFormFile? Photo { get; set; }

    }
}
