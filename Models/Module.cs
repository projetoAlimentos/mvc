using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace projeto.Models
{
    [JsonObject(IsReference = true)]
    public class Module
    {
        public int Id { get; set; }

        [Display(Name="Nome")]
        public String Name { get; set; }

        [Display(Name="Descrição")]
        public String Description { get; set; }

        public virtual ICollection<Topic> Topics { get; set; }

        public int? SubjectId { get; set; }

        public virtual Subject Subject { get; set; }

        [Display(Name="Ativo")]
        public bool Active { get; set; }

    }
}