using recipes_backend.Common;
using recipes_backend.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace recipes_backend.Operations.Recipe.UpdateRecipeInfo
{
    public class UpdateRecipeInfoResponse : BaseResponse
    {
        public RecipeData Recipe { get; set; }
    }

    public class RecipeData
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
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
        public int? AmountOfRates { get; set; }
        public int? AmountOfServings { get; set; }
        public int? AmountOfFavorites { get; set; }
        public IdItem? FoodType { get; set; }
        public IdItem? DishType { get; set; }
        public double? Rating { get; set; }
        public DateTime? CreationDate { get; set; }
        public bool IsPublished { get; set; }

        public List<IdItem>? PreparationTips { get; set; }
        public List<IdItem>? AdditionalTips { get; set; }
        public List<IdItem>? MenuTypes { get; set; }
        public List<CollectedUpdateIngredient>? Ingredients { get; set; }
        public List<CollectedRecipeStep>? Steps { get; set; }

        public List<IdItem>? Images { get; set; }
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
