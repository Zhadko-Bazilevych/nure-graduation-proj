using System;
using System.Collections.Generic;

namespace recipes_backend.Models
{
    public partial class PatternDishType
    {
        public int Id { get; set; }
        public int SearchPatternId { get; set; }
        public int DishTypeId { get; set; }

        public virtual DishType DishType { get; set; } = null!;
        public virtual SearchPattern SearchPattern { get; set; } = null!;
    }
}
