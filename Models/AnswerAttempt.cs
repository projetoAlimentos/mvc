using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace projeto.Models
{
    [JsonObject(IsReference = true)]
    public class AnswerAttempt
    {
        public int Id { get; set; }

        public int? QuestionId { get; set; }
        public virtual Question Question { get; set; }

        public String Description { get; set; }

        public virtual ICollection<Attempted> Attempts { get; set; }

        public Attempt Attempt { get; set; }

    }
}