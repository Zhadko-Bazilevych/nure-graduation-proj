using recipes_backend.Common;
using recipes_backend.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace recipes_backend.Operations.Recipe.Filter
{
    public class FilterResponse : BaseResponse
    {
        public List<RecipeShort>? Recipes { get; set; }
    }
}
