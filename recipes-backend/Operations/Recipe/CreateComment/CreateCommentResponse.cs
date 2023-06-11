using recipes_backend.Common;
using recipes_backend.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace recipes_backend.Operations.Recipe.CreateComment
{
    public class CreateCommentResponse : BaseResponse
    {
        public int Id { get; set; }
        public string? Image { get; set; }
    }
}
