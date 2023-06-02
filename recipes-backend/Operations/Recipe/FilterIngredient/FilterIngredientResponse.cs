using recipes_backend.Common;
using recipes_backend.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace recipes_backend.Operations.Recipe.FilterIngredient
{
    public class FilterIngredientResponse : BaseResponse
    {
        public List<IdIngredient>? Ingredients { get; set; }
    }

    public class IdIngredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
