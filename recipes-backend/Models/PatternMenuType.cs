using System;
using System.Collections.Generic;

namespace recipes_backend.Models
{
    public partial class PatternMenuType
    {
        public int Id { get; set; }
        public int SearchPatternId { get; set; }
        public int MenuTypeId { get; set; }

        public virtual MenuType MenuType { get; set; } = null!;
        public virtual SearchPattern SearchPattern { get; set; } = null!;
    }
}
