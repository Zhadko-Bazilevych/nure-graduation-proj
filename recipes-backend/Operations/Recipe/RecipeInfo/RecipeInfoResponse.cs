using recipes_backend.Common;
using recipes_backend.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace recipes_backend.Operations.Recipe.RecipeInfo
{
    public class RecipeInfoResponse : BaseResponse
    {
        public RecipeView Recipe { get; set; }
    }

    public class RecipeView
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? Difficulty { get; set; }
        public int? RequiredTime { get; set; }
        public int Servings { get; set; }
        public double? CaloricValue { get; set; }
        public double? Proteins { get; set; }
        public double? Fats { get; set; }
        public double? Carbohydrates { get; set; }
        public string? Video { get; set; }
        public int? AmountOfRates { get; set; }
        public int? AmountOfServings { get; set; }
        public int? AmountOfFavorites { get; set; }
        public int FoodTypeId { get; set; }
        public string FoodType { get; set; }
        public int DishTypeId { get; set; }
        public string DishType { get; set; }
        public double? Rating { get; set; }
        public DateTime? CreationDate { get; set; }
        public bool IsPublished { get; set; }

        public List<string> PreparationTips { get; set; }
        public List<string> AdditionalTips { get; set; }
        public List<string> MenuTypes { get; set; }
        public List<CollectedComment> Comments { get; set; }
        public List<CollectedIngredient> Ingredients { get; set; }
        public List<CollectedRecipeStep> Steps { get; set; }

        public List<string> Images { get; set; }
        public bool isFavorite { get; set; } = false;
        public int UserRate { get; set; }
    }

    public class CollectedIngredient
    {
        public string Name { get; set; }
        public string Amount { get; set; }
        public string Measurement { get; set; }
    }

    public class CollectedRecipeStep
    {
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
        public DateTime DateCreated { get; set; }
        public bool isAuthor { get; set; } = false;
    }

}
