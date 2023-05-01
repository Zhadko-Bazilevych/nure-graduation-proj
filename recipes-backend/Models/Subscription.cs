using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace recipes_backend.Models
{
    [NotMapped]
    public partial class Subscription
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int AuthorId { get; set; }

        public virtual User Author { get; set; } = null!;

        public virtual User User { get; set; } = null!;
    }
}


