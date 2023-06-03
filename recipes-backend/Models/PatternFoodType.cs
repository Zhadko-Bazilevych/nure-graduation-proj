using System;
using System.Collections.Generic;

namespace recipes_backend.Models
{
    public partial class PatternFoodType
    {
        public int Id { get; set; }
        public int SearchPatternId { get; set; }
        public int FoodTypeId { get; set; }

        public virtual FoodType FoodType { get; set; } = null!;
        public virtual SearchPattern SearchPattern { get; set; } = null!;
    }
}
