using System;
using System.ComponentModel.DataAnnotations;

namespace projeto.Models
{
    public class Article
    {
        public int Id { get; set; }

        [Display(Name="Conteúdo")]
        public String Body { get; set; }

        [Display(Name="Vídeo")]
        public String Video { get; set; }

    }

}