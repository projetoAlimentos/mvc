using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace projeto.Models
{
    public class ResponsableSubject
    {
        public int Id { get; set; }

        public int? UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int? SubjectId { get; set; }

        public virtual Subject Subject { get; set; }

    }
}
