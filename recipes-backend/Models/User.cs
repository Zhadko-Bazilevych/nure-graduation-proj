using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace recipes_backend.Models
{
    public partial class User : IdentityUser
    {
        public User()
        {
            Comments = new HashSet<Comment>();
            FavoriteRecipes = new HashSet<FavoriteRecipe>();
            RecipeRatings = new HashSet<RecipeRating>();
            Recipes = new HashSet<Recipe>();
            SubscriptionAuthors = new HashSet<Subscription>();
            SubscriptionUsers = new HashSet<Subscription>();
        }

        public string? Image { get; set; }
        public string Description { get; set; } = null!;

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<FavoriteRecipe> FavoriteRecipes { get; set; }
        public virtual ICollection<RecipeRating> RecipeRatings { get; set; }
        public virtual ICollection<Recipe> Recipes { get; set; }
        public virtual ICollection<Subscription> SubscriptionAuthors { get; set; }
        public virtual ICollection<Subscription> SubscriptionUsers { get; set; }
    }
}
