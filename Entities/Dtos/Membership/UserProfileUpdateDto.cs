using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Membership
{
    public class UserProfileUpdateDto
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
