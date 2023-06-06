using recipes_backend.Common;
using recipes_backend.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace recipes_backend.Operations.Recipe.GetFilterData
{
    public class RandomResponse : BaseResponse
    {
        public int Id { get; set; }
    }
}
