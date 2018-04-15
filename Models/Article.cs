using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace projeto.Models
{
    [JsonObject(IsReference = true)]
    public class Article
    {
        public int Id { get; set; }

        [Display(Name="Conteúdo")]
        public String Body { get; set; }

        [Display(Name="Vídeo")]
        public String Video { get; set; }

    }

}