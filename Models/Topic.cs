using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace projeto.Models
{
    [JsonObject(IsReference = true)]
    public class Topic
    {
        public int Id { get; set; }

        [Display(Name="Nome")]
        public String Name { get; set; }

        [Display(Name="Descrição")]
        public String Description { get; set; }

        public int? ModuleId { get; set; }

        public virtual Module Module { get; set; }

        public virtual ICollection<Question> Questions { get; set; }

        [Display(Name="Ativo")]
        public bool Active { get; set; }

        [Display(Name = "Dificuldade")]
        public int Difficulty { get; set; }

        [Display(Name = "Ordem")]
        public int? Order { get; set; }
    }
}

