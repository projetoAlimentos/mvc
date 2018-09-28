using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace projeto.Models
{
    [JsonObject(IsReference = true)]
    public class SubjectUser
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public int? SubjectId { get; set; }

        public virtual Subject Subject { get; set; }

    }
}
