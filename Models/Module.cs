using System;
using System.Collections.Generic;

namespace projeto.Models
{
    public class Module
    {
        public int Id { get; set; }

        public String Name { get; set; }

        public String Description { get; set; }

        public virtual ICollection<Topic> Topics { get; set; }

        public int? SubjectId { get; set; }

        public virtual Subject Subject { get; set; }

        public bool Active { get; set; }

    }
}