using System.Collections.Generic;
using Newtonsoft.Json;

namespace projeto.Models
{
    [JsonObject(IsReference = true)]
    public class Attempt
    {
        public int Id { get; set; }

        public string ApplicationUserId { get; set; }

        public ApplicationUser User { get; set; }

        public int? TopicId { get; set; }

        public Topic topico { get; set; }

        public virtual ICollection<AnswerAttempt> AnswerAttempt { get; set; }
    }
}

