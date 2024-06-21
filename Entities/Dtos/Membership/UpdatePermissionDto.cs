using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Membership
{
    public class UpdatePermissionDto
    {
        [Required(ErrorMessage = "UserName is Required")]
        public string UserName { get; set; }
    }
}
