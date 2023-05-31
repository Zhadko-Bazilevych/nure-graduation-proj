using recipes_backend.Common;
using recipes_backend.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace recipes_backend.Operations.Recipe.Filter
{
    public class FilterResponse : BaseResponse
    {
        public List<RecipeShort>? Recipes { get; set; }
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
