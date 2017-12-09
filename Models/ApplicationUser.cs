using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace projeto.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {

        public String Name { get; set; }

        [Column(TypeName = "blob")]
        public byte[] ProfileImage { get; set; }

        public int? RoleId { get; set; }
        public virtual Role Role { get; set; }

        public int? SubjectUserId { get; set; }
        public SubjectUser SubjectUser { get; set; }
    }
}
