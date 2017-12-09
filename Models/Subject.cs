using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace projeto.Models
{
    public class Subject
    {
        public int Id { get; set; }

        public String Name { get; set; }

        public String Description { get; set; }

        public virtual ICollection<Module> Modules { get; set; }

        public virtual ICollection<ResponsableSubject> Responsables { get; set; }

    }
}
