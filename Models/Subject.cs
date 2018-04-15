using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace projeto.Models
{
    [JsonObject(IsReference = true)]
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
