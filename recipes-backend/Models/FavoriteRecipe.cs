using System;
using System.Collections.Generic;

namespace recipes_backend.Models
{
    public partial class FavoriteRecipe
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
