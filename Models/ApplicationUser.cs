using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace projeto.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {

        [Display(Name="Nome")]
        public String Name { get; set; }

        [Column(TypeName = "blob")]
        [Display(Name="Imagem de perfil")]
        public byte[] ProfileImage { get; set; }

        public int? SubjectUserId { get; set; }

        public SubjectUser SubjectUser { get; set; }

        public string IdentityRoleId { get; set; }

        public virtual IdentityRole IdentityRole { get; set; }
    }
}
