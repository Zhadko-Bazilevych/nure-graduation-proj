using recipes_backend.Common;
using recipes_backend.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace recipes_backend.Operations.UserRecipe.GetMeasurementsData
{
    public class GetMeasurementsDataResponse : BaseResponse
    {
        public List<IdItem>? Measurements { get; set; }
    }
}
