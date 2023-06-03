using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace recipes_backend.Models
{
    public partial class User
    {
        public User()
        {
            Comments = new HashSet<Comment>();
            FavoriteRecipes = new HashSet<FavoriteRecipe>();
            RecipeRatings = new HashSet<RecipeRating>();
            Recipes = new HashSet<Recipe>();
            SubscriptionAuthors = new HashSet<Subscription>();
            SubscriptionUsers = new HashSet<Subscription>();
            RefreshTokens = new HashSet<RefreshToken>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Image { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }
        public string Mail { get; set; } = null!;
        public string? Description { get; set; }
        public int? AmountOfSubscribers { get; set; }
        public int? AmountOfRecipes { get; set; }
        public string? Role { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<FavoriteRecipe> FavoriteRecipes { get; set; }
        public virtual ICollection<RecipeRating> RecipeRatings { get; set; }
        public virtual ICollection<Recipe> Recipes { get; set; }
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }

        [NotMapped]
        public virtual ICollection<Subscription> SubscriptionAuthors { get; set; }

        [NotMapped]
        public virtual ICollection<Subscription> SubscriptionUsers { get; set; }
    }
}
