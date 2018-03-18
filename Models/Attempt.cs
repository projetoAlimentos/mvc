using System.Collections.Generic;

namespace projeto.Models
{
    public class Attempt
    {
        public int Id { get; set; }

        public int? ApplicationUserId { get; set; }

        public ApplicationUser User { get; set; }

        public int? TopicId { get; set; }

        public Topic topico { get; set; }

        public virtual ICollection<AnswerTry> AnswerTry { get; set; }
    }
}

