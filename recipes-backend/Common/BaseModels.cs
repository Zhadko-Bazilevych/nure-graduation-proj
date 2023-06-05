namespace recipes_backend.Common
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
        public string Image { get; set; }
        public int RequiredTime { get; set; }
        public int Difficulty { get; set; }
        public string Author { get; set; }
        public int AuthorId { get; set; }
        public string Description { get; set; }
    }
}
