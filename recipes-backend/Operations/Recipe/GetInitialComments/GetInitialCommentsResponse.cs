using recipes_backend.Common;
using recipes_backend.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace recipes_backend.Operations.Recipe.GetInitialComments
{
    public class GetInitialCommentsResponse : BaseResponse
    {
        public List<CollectedComment> Comments { get; set; }
    }
}
