using System.Collections.Generic;

namespace projeto.Models
{
    public class AnswerAttempt
    {
        public int Id { get; set; }

        public int? QuestionId { get; set; }
        public virtual Question Question { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }

        public Attempt Attempt { get; set; }

    }
}