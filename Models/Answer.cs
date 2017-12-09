using System;
using System.ComponentModel.DataAnnotations;

namespace projeto.Models
{
    public class Answer
    {
        public int Id { get; set; }

        public String Description { get; set; }

        public bool Correct { get; set; }

        public int? QuestionId { get; set; }
        public virtual Question Question { get; set; }
    }
}
