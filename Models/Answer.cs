using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace projeto.Models
{
    [JsonObject(IsReference = true)]
    public class Answer
    {
        public int Id { get; set; }

        [Display(Name="Descrição")]
        public String Description { get; set; }

        [Column(TypeName = "blob")]
        [Display(Name="Imagem")]
        public byte[] Image { get; set; }

        [Display(Name="Esta resposta está correta")]
        public bool Correct { get; set; }

        public int? QuestionId { get; set; }

        public virtual Question Question { get; set; }
    }
}
