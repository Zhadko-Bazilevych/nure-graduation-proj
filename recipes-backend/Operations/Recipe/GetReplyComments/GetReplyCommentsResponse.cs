using recipes_backend.Common;
using recipes_backend.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace recipes_backend.Operations.Recipe.GetReplyComments
{
    public class GetReplyCommentsResponse : BaseResponse
    {
        public List<CollectedComment> Comments { get; set; }
    }
}
