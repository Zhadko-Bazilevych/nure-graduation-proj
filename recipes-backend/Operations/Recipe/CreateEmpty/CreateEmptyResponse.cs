using recipes_backend.Common;
using recipes_backend.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace recipes_backend.Operations.Recipe.CreateEmpty
{
    public class CreateEmptyResponse : BaseResponse
    {
        public int Id { get; set; }
    }
}
