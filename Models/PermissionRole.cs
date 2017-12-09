using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace projeto.Models
{
    public class PermissionRole
    {
        public int Id { get; set; }

        public int? RoleId { get; set; }

        public virtual Role Role { get; set; }

        public int? PermissionId { get; set; }

        public virtual Permission Permission { get; set; }


    }
}
