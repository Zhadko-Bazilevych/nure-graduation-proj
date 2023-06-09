using recipes_backend.Common;

namespace recipes_backend.Operations.Recipe.UpdateRecipe
{
    public class UpdateRecipeRequest
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? Difficulty { get; set; }
        public int? RequiredTime { get; set; }
        public int? Servings { get; set; }
        public double? CaloricValue { get; set; }
        public double? Proteins { get; set; }
        public double? Fats { get; set; }
        public double? Carbohydrates { get; set; }
        public string? Video { get; set; }
        public int? FoodType { get; set; }
        public int? DishType { get; set; }
        public bool IsPublished { get; set; }

        public List<string>? PreparationTips { get; set; }
        public List<string>? AdditionalTips { get; set; }
        public List<int>? MenuTypes { get; set; }
        public List<int>? IngredientsId { get; set; }
        public List<int>? IngredientsMeasurementId { get; set; }
        public List<int>? IngredientsAmount { get; set; }
        public List<int>? StepsIds { get; set; }
        public List<string>? StepsTitles { get; set; }
        public List<string>? StepsDescriptions { get; set; }
        public List<IFormFile>? StepImagesData { get; set; }

        public List<int>? ImagesIndexes { get; set; }
        public List<IFormFile>? ImagesData { get; set; }
    }

    public class CollectedUpdateIngredient
    {
        public int? Id { get; set; }
        public int? IngredientId { get; set; }
        public string Name { get; set; }
        public double Amount { get; set; }
        public int MeasurementId { get; set; }
        public string Measurement { get; set; }
    }
}
