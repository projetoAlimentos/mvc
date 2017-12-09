using System;
using System.Collections.Generic;

namespace projeto.Models
{
    public class Question
    {
        public int Id { get; set; }

        public int Difficulty { get; set; }

        public String Description { get; set; }

        public String Hint { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }

        public bool Active { get; set; }

        public int? TopicId { get; set; }
        public virtual Topic Topic { get; set; }

    }

}