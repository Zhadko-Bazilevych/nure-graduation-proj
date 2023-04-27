using System;
using System.Collections.Generic;

namespace recipes_backend.Models
{
    public partial class Comment
    {
        public Comment()
        {
            InverseParentComment = new HashSet<Comment>();
        }

        public int Id { get; set; }
        public int? ParentCommentId { get; set; }
        public int RecipeId { get; set; }
        public string UserId { get; set; }
        public DateTime DateCreated { get; set; }
        public string Content { get; set; } = null!;

        public virtual Comment? ParentComment { get; set; }
        public virtual Recipe Recipe { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual ICollection<Comment> InverseParentComment { get; set; }
    }
}
