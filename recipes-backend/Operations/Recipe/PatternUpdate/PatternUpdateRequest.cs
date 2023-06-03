namespace recipes_backend.Operations.Recipe.PatternUpdate
{
    public class PatternUpdateRequest
    {
        public int? Id {  get; set; }
        public string? PatternName { get; set; }
        public string? Name { get; set; }
        public int? DifficultyMin { get; set; }
        public int? DifficultyMax { get; set; }
        public int? RequiredTimeMin { get; set; }
        public int? RequiredTimeMax { get; set; }
        public List<int>? DishTypeId { get; set; }
        public List<int>? FoodTypeId { get; set; }
        public List<int>? MenuTypeId { get; set; }
        public List<int>? IngredientId { get; set; }
        public bool asIngredientPool { get; set; }
        public string? SortType { get; set; }
        public bool isDescending { get; set; }
    }
}
