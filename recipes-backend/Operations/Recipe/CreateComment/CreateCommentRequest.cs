namespace recipes_backend.Operations.Recipe.CreateComment
{
    public class CreateCommentRequest
    {
        public int? ParentCommentId { get; set; }
        public int RecipeId { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; } = null!;
        public IFormFile? Image { get; set; }
    }
}
