namespace recipes_backend.Operations.UserRecipes.EditUser
{
    public class EditUserRequest
    {
        public int AuthorId { get; set; }
        public bool IsPublicMail { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool isImageChanging { get; set; } = false;
        public IFormFile? Image { get; set; }
    }
}
