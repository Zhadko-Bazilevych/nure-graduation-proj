using recipes_backend.Common;
using recipes_backend.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace recipes_backend.Operations.Recipe.GetFilterData
{
    public class GetFilterDataResponse : BaseResponse
    {
        public List<IdItem>? MenuTypes { get; set; }
        public List<IdItem>? FoodTypes { get; set; }
        public List<IdItem>? DishTypes { get; set; }
    }

    public class IdItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
