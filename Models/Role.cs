using System;
using System.Collections.Generic;

namespace projeto.Models
{
    public class Role
    {
        public int Id { get; set; }

        public String Name { get; set; }

        public virtual ICollection<Permission> Permissions { get; set; }
    }
}