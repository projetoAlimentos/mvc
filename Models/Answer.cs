using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projeto.Models
{
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
