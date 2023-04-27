using System;
using System.Collections.Generic;

namespace recipes_backend.Models
{
    public partial class MenuType
    {
        public MenuType()
        {
            MenuTypeLists = new HashSet<MenuTypeList>();
            PatternMenuTypes = new HashSet<PatternMenuType>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<MenuTypeList> MenuTypeLists { get; set; }
        public virtual ICollection<PatternMenuType> PatternMenuTypes { get; set; }
    }
}
