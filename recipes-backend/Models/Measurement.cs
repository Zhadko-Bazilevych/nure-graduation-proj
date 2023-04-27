using System;
using System.Collections.Generic;

namespace recipes_backend.Models
{
    public partial class Measurement
    {
        public Measurement()
        {
            RecipeIngredients = new HashSet<RecipeIngredient>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<RecipeIngredient> RecipeIngredients { get; set; }
    }
}
