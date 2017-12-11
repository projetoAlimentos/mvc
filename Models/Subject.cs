using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projeto.Models
{
    public class Subject
    {
        public int Id { get; set; }

        [Display(Name="Nome")]
        public String Name { get; set; }

        [Display(Name="Descrição")]
        public String Description { get; set; }

        public virtual ICollection<Module> Modules { get; set; }

        public virtual ICollection<ResponsableSubject> Responsables { get; set; }

    }
}
