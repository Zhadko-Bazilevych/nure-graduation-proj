using System;
using System.Collections.Generic;

namespace recipes_backend.Models
{
    public partial class Subscription
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string AuthorId { get; set; }

        public virtual User Author { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
