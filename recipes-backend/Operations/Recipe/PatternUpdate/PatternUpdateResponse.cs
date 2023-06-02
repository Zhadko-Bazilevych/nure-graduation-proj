using recipes_backend.Common;
using recipes_backend.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace recipes_backend.Operations.Recipe.PatternUpdate
{
    public class PatternUpdateResponse : BaseResponse
    {
        public int? Id { get; set; }
    }
}
