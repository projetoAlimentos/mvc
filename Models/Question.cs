using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace projeto.Models
{
    [JsonObject(IsReference = true)]
    public class Question
    {
        public int Id { get; set; }

        [Display(Name="Dificuldade")]
        public int Difficulty { get; set; }

        [Display(Name="Enunciado")]
        public String Description { get; set; }

        [Display(Name="Dica")]
        public String Hint { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }

        public bool Active { get; set; }

        [DefaultValue(false)]
        public bool VerdadeiraFalsa { get; set; }

        public int? TopicId { get; set; }
        public virtual Topic Topic { get; set; }

    }

}