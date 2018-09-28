using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace projeto.Models
{
    [JsonObject(IsReference = true)]
    public class Attempted
    {
        public int Id { get; set; }

        public int AnswerAttemptId { get; set; }
        public virtual AnswerAttempt AnswerAttempt { get; set; }

        public int AnswerId { get; set; }
        public virtual Answer Answer { get; set; }
    }
}