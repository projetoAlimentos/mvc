using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projeto.Models
{
  public class Answer
  {
    public int Id { get; set; }

    public String Description { get; set; }

    [Column(TypeName = "blob")]
    public byte[] Image { get; set; }

    public bool Correct { get; set; }

    public int? QuestionId { get; set; }

    public virtual Question Question { get; set; }
  }
}
