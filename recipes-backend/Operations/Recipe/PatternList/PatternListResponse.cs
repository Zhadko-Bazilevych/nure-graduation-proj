using recipes_backend.Common;
using recipes_backend.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace recipes_backend.Operations.Recipe.PatternList
{
    public class PatternListResponse : BaseResponse
    {
        public List<IdItem> Items { get; set; }
        public List<Pattern> Patterns { get; set; }
    }

    public class Pattern
    {
        public string? Name { get; set; }
        public int? DifficultyMin { get; set; }
        public int? DifficultyMax { get; set; }
        public int? RequiredTimeMin { get; set; }
        public int? RequiredTimeMax { get; set; }
        public List<IdItem>? DishTypeId { get; set; }
        public List<IdItem>? FoodTypeId { get; set; }
        public List<IdItem>? MenuTypeId { get; set; }
        public List<IdItem>? IngredientId { get; set; }
        public bool asIngredientPool { get; set; }
        public string? SortType { get; set; }
        public bool isDescending { get; set; }
    }
}
