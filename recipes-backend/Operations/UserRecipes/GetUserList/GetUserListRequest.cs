namespace recipes_backend.Operations.UserRecipes.GetUserList
{
    public class GetUserListRequest
    {
        public int listType { get; set; }
        public int? authorId { get; set; }
    }
}
