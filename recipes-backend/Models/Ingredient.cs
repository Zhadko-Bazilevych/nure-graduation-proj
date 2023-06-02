using System;
using System.Collections.Generic;

namespace recipes_backend.Models
{
    public partial class Ingredient
    {
        public Ingredient()
        {
            PatternIngredientLists = new HashSet<PatternIngredientList>();
            RecipeIngredients = new HashSet<RecipeIngredient>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<PatternIngredientList> PatternIngredientLists { get; set; }
        public virtual ICollection<RecipeIngredient> RecipeIngredients { get; set; }
    }
}
