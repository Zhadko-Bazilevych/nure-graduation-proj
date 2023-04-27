using System;
using System.Collections.Generic;

namespace recipes_backend.Models
{
    public partial class DishType
    {
        public DishType()
        {
            Recipes = new HashSet<Recipe>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Recipe> Recipes { get; set; }
    }
}
