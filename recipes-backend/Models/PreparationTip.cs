using System;
using System.Collections.Generic;

namespace recipes_backend.Models
{
    public partial class PreparationTip
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public string Description { get; set; } = null!;
        public virtual Recipe Recipe { get; set; } = null!;
    }
}
