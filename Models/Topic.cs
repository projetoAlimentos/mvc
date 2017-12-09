using System;
using System.Collections.Generic;

namespace projeto.Models
{
    public class Topic
    {
        public int Id { get; set; }

        public String Name { get; set; }

        public String Description { get; set; }

        public int? ModuleId { get; set; }

        public virtual Module Module { get; set; }

        public virtual ICollection<Question> Questions { get; set; }

        public bool Active { get; set; }

        public int Difficulty { get; set; }
    }
}

