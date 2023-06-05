using recipes_backend.Common;
using recipes_backend.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace recipes_backend.Operations.UserRecipes.AuthorSubscriptionList
{
    public class AuthorSubscriptionListResponse : BaseResponse
    {
        public List<AuthorShort> AuthorList { get; set; }
    }

    public class AuthorShort
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int AmountOfSubscribers { get; set; }
        public int AmountOfRecipes { get; set; }
    }
}
