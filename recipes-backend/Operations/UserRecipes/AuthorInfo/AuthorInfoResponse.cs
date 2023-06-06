using recipes_backend.Common;
using recipes_backend.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace recipes_backend.Operations.UserRecipes.AuthorInfo
{
    public class AuthorInfoResponse : BaseResponse
    {
        public AuthorInfo Author { get; set; }
    }

    public class AuthorInfo
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Image { get; set; }
        public string Mail { get; set; } = null!;
        public string? Description { get; set; }
        public int AmountOfSubscribers { get; set; }
        public int AmountOfRecipes { get; set; }
        public bool IsMe { get; set; }
        public bool IsPublicMail { get; set; }
        public bool IsSubscribed { get; set; } = false;
    }
}
