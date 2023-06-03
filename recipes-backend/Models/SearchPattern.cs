using System;
using System.Collections.Generic;

namespace recipes_backend.Models
{
    public partial class SearchPattern
    {
        public SearchPattern()
        {
            PatternIngredientLists = new HashSet<PatternIngredientList>();
            PatternMenuTypes = new HashSet<PatternMenuType>();
            PatternDishTypes = new HashSet<PatternDishType>();
            PatternFoodTypes = new HashSet<PatternFoodType>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public string? Name { get; set; }
        public string? FilterString { get; set; }
        public int? DifficultyMin { get; set; }
        public int? DifficultyMax { get; set; }
        public int? MinReqTime { get; set; }
        public int? MaxReqTime { get; set; }
        public string? SortType { get; set; }
        public bool isDescending { get; set; }
        public bool asIngredientPool { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ICollection<PatternIngredientList> PatternIngredientLists { get; set; }
        public virtual ICollection<PatternMenuType> PatternMenuTypes { get; set; }
        public virtual ICollection<PatternDishType> PatternDishTypes { get; set; }
        public virtual ICollection<PatternFoodType> PatternFoodTypes { get; set; }
    }
}
