using System;
using System.Collections.Generic;

namespace recipes_backend.Models
{
    public partial class SearchPattern
    {
        public SearchPattern()
        {
            PatternIngridientLists = new HashSet<PatternIngridientList>();
            PatternMenuTypes = new HashSet<PatternMenuType>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public string? Name { get; set; }
        public string? FilterString { get; set; }
        public int? DifficultyMin { get; set; }
        public int? DifficultyMax { get; set; }
        public int? MinReqTime { get; set; }
        public int? MaxReqTime { get; set; }
        public int? DishTypeId { get; set; }
        public int? FoodTypeId { get; set; }
        public string? SortType { get; set; }
        public bool isDescending { get; set; }
        public bool asIngredientPool { get; set; }

        public virtual ICollection<PatternIngridientList> PatternIngridientLists { get; set; }
        public virtual ICollection<PatternMenuType> PatternMenuTypes { get; set; }
    }
}
