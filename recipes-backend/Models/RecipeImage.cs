using System;
using System.Collections.Generic;

namespace recipes_backend.Models
{
    public partial class RecipeImage
    {
        public int Id { get; set; }
        public string Image { get; set; } = null!;
        public int RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; } = null!;
    }
}
