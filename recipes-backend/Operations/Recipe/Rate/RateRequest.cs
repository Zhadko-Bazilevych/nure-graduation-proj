namespace recipes_backend.Operations.Recipe.Rate
{
    public class RateRequest
    {
        public int recipeId { get; set; }
        public int newRate {  get; set; }
    }
}
