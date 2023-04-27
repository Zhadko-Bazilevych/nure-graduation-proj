using System;
using System.Collections.Generic;

namespace recipes_backend.Models
{
    public partial class Ingredient
    {
        public Ingredient()
        {
            PatternIngridientLists = new HashSet<PatternIngridientList>();
            RecipeIngredients = new HashSet<RecipeIngredient>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<PatternIngridientList> PatternIngridientLists { get; set; }
        public virtual ICollection<RecipeIngredient> RecipeIngredients { get; set; }
    }
}
