using System;
using System.Collections.Generic;

namespace recipes_backend.Models
{
    public partial class PatternIngridientList
    {
        public int Id { get; set; }
        public int SearchPatternId { get; set; }
        public int IngridientId { get; set; }

        public virtual Ingredient Ingridient { get; set; } = null!;
        public virtual SearchPattern SearchPattern { get; set; } = null!;
    }
}
