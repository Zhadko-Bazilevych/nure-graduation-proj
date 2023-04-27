using System;
using System.Collections.Generic;

namespace recipes_backend.Models
{
    public partial class RecipeRating
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public int Rate { get; set; }
        public string UserId { get; set; }

        public virtual Recipe Recipe { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
