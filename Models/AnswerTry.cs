using System.Collections.Generic;

namespace projeto.Models
{
    public class AnswerTry
    {
        public int Id { get; set; }

        public int? QuestionId { get; set; }
        public virtual Question Question { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }

        public Try Try { get; set; }

    }
}