﻿namespace recipes_backend.Common
{
    public class IdItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class RecipeShort
    {
        public int RecipeId { get; set; }
        public string Name { get; set; }
        public string? Image { get; set; }
        public int? RequiredTime { get; set; }
        public int? Difficulty { get; set; }
        public string Author { get; set; }
        public int AuthorId { get; set; }
        public string? Description { get; set; }
        public bool? IsPublished { get; set; }
    }

    public class CollectedIngredient
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public double Amount { get; set; }
        public string Measurement { get; set; }
    }

    public class CollectedRecipeStep
    {
        public int? Id { get; set; }
        public int StepNumber { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string? Image { get; set; }
    }

    public class CollectedComment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string? Image { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string? UserImage { get; set; }
        public DateTime DateCreated { get; set; }
        public bool isAuthor { get; set; } = false;
        public int CountReplies { get; set; }
    }
}
