using System;
using System.Collections.Generic;

namespace recipes_backend.Models
{
    public partial class MenuTypeList
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public int MenuTypeId { get; set; }

        public virtual MenuType MenuType { get; set; } = null!;
        public virtual Recipe Recipe { get; set; } = null!;
    }
}
