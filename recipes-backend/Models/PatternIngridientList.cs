﻿using System;
using System.Collections.Generic;

namespace recipes_backend.Models
{
    public partial class PatternIngredientList
    {
        public int Id { get; set; }
        public int SearchPatternId { get; set; }
        public int IngredientId { get; set; }

        public virtual Ingredient Ingredient { get; set; } = null!;
        public virtual SearchPattern SearchPattern { get; set; } = null!;
    }
}
