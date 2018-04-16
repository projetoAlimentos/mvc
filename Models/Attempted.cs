using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace projeto.Models
{
    [JsonObject(IsReference = true)]
    public class Attempted
    {
        public int Id { get; set; }

        public int answerAttemptId { get; set; }
        public virtual AnswerAttempt answerAttempt { get; set; }

        public int answerId { get; set; }
        public virtual Answer answer { get; set; }
    }
}