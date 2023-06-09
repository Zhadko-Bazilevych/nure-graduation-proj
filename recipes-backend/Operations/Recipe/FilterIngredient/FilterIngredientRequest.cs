namespace recipes_backend.Operations.Recipe.FilterIngredient
{
    public class FilterIngredientRequest
    {
        public string Name { get; set; }
        public List<int>? notIncluded { get; set; }
    }
}
