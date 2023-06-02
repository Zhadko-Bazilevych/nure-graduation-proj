using System;
using System.Collections.Generic;

namespace recipes_backend.Models
{
    public partial class RecipeIngredient
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public int IngredientId { get; set; }
        public int MeasurementId { get; set; }
        public double Amount { get; set; }

        public virtual Ingredient Ingredient { get; set; } = null!;
        public virtual Measurement Measurement { get; set; } = null!;
        public virtual Recipe Recipe { get; set; } = null!;
    }
}
