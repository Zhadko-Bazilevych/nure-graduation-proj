using System;
using System.Collections.Generic;

namespace recipes_backend.Models
{
    public partial class RecipeStep
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public int StepNumber { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string? Image { get; set; }

        public virtual Recipe Recipe { get; set; } = null!;
    }
}
