using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace recipes_backend.Models
{
    public partial class Recipe
    {
        public Recipe()
        {
            AdditionalTips = new HashSet<AdditionalTip>();
            Comments = new HashSet<Comment>();
            FavoriteRecipes = new HashSet<FavoriteRecipe>();
            MenuTypeLists = new HashSet<MenuTypeList>();
            PreparationTips = new HashSet<PreparationTip>();
            RecipeImages = new HashSet<RecipeImage>();
            RecipeIngredients = new HashSet<RecipeIngredient>();
            RecipeRatings = new HashSet<RecipeRating>();
            RecipeSteps = new HashSet<RecipeStep>();
        }

        public int Id { get; set; }
        
        [NotMapped]
        public int UserId { get; set; }
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
        public int AmountOfRates { get; set; } = 0;
        public int TotalRates { get; set; } = 0;
        public int AmountOfFavorites { get; set; } = 0;
        public int? FoodTypeId { get; set; }
        public int? DishTypeId { get; set; }
        public double Rating { get; set; } = 0;
        public DateTime? CreationDate { get; set; }
        public bool IsPublished { get; set; } = false;

        public virtual DishType DishType { get; set; }
        public virtual FoodType FoodType { get; set; }

        [NotMapped]
        public virtual User User { get; set; } = null!;
        public virtual ICollection<AdditionalTip> AdditionalTips { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<FavoriteRecipe> FavoriteRecipes { get; set; }
        public virtual ICollection<MenuTypeList> MenuTypeLists { get; set; }
        public virtual ICollection<PreparationTip> PreparationTips { get; set; }
        public virtual ICollection<RecipeImage> RecipeImages { get; set; }
        public virtual ICollection<RecipeIngredient> RecipeIngredients { get; set; }
        public virtual ICollection<RecipeRating> RecipeRatings { get; set; }
        public virtual ICollection<RecipeStep> RecipeSteps { get; set; }
    }
}
