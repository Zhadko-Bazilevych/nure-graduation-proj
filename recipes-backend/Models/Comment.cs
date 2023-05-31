using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace recipes_backend.Models
{
    public partial class Comment
    {
        public Comment()
        {
            InverseParentComment = new HashSet<Comment>();
        }

        public int Id { get; set; }

        [NotMapped]
        public int? ParentCommentId { get; set; }
        public int RecipeId { get; set; }
        public int UserId { get; set; }
        public DateTime DateCreated { get; set; }
        public string Content { get; set; } = null!;
        public string? Image { get; set; }

        [NotMapped]
        public virtual Comment? ParentComment { get; set; }
        public virtual Recipe Recipe { get; set; } = null!;
        public virtual User User { get; set; } = null!;

        [NotMapped]
        public virtual ICollection<Comment> InverseParentComment { get; set; }
    }
}
