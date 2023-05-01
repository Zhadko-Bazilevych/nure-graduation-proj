using System;
using System.Collections.Generic;
using System.Drawing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace recipes_backend.Models
{
    public partial class recipesContext : DbContext
    {
        public recipesContext()
        {
        }

        public recipesContext(DbContextOptions<recipesContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();

            //Seeder.Seed(this);
        }

        public virtual DbSet<AdditionalTip> AdditionalTips { get; set; } = null!;
        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<DishType> DishTypes { get; set; } = null!;
        public virtual DbSet<FavoriteRecipe> FavoriteRecipes { get; set; } = null!;
        public virtual DbSet<FoodType> FoodTypes { get; set; } = null!;
        public virtual DbSet<Ingredient> Ingredients { get; set; } = null!;
        public virtual DbSet<Measurement> Measurements { get; set; } = null!;
        public virtual DbSet<MenuType> MenuTypes { get; set; } = null!;
        public virtual DbSet<MenuTypeList> MenuTypeLists { get; set; } = null!;
        public virtual DbSet<PatternIngridientList> PatternIngridientLists { get; set; } = null!;
        public virtual DbSet<PatternMenuType> PatternMenuTypes { get; set; } = null!;
        public virtual DbSet<PreparationTip> PreparationTips { get; set; } = null!;
        public virtual DbSet<Recipe> Recipes { get; set; } = null!;
        public virtual DbSet<RecipeImage> RecipeImages { get; set; } = null!;
        public virtual DbSet<RecipeIngredient> RecipeIngredients { get; set; } = null!;
        public virtual DbSet<RecipeRating> RecipeRatings { get; set; } = null!;
        public virtual DbSet<RecipeStep> RecipeSteps { get; set; } = null!;
        public virtual DbSet<SearchPattern> SearchPatterns { get; set; } = null!;
        public virtual DbSet<Subscription> Subscriptions { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; } = null!;

        internal class Seeder
        {
            public static void Seed(recipesContext db)
            {
                db.SaveChanges();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Subscription>(entity =>
            {
                entity.ToTable("Subscription");

                entity.HasIndex(e => e.AuthorId, "IX_Subscription_author_id");

                entity.HasIndex(e => e.UserId, "IX_Subscription_user_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AuthorId).HasColumnName("author_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.SubscriptionAuthors)
                    .HasForeignKey(d => d.AuthorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Subscription_User1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SubscriptionUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Subscription_User");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("Comment");

                entity.HasIndex(e => e.ParentCommentId, "IX_Comment_parent_comment_id");

                entity.Property(e => e.ParentCommentId).HasColumnName("parent_comment_id");

                entity.HasOne(d => d.ParentComment)
                    .WithMany(p => p.InverseParentComment)
                    .HasForeignKey(d => d.ParentCommentId)
                    .HasConstraintName("R_33");
            });

            modelBuilder.Entity<Recipe>(entity =>
            {
                entity.ToTable("Recipe");

                entity.HasIndex(e => e.UserId, "IX_Recipe_user_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Recipes)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("R_9");
            });
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<AdditionalTip>(entity =>
        //    {
        //        entity.HasIndex(e => e.RecipeId, "IX_AdditionalTips_recipe_id");

        //        entity.Property(e => e.Id).HasColumnName("id");

        //        entity.Property(e => e.Description)
        //            .HasColumnType("text")
        //            .HasColumnName("description");

        //        entity.Property(e => e.RecipeId).HasColumnName("recipe_id");

        //        entity.HasOne(d => d.Recipe)
        //            .WithMany(p => p.AdditionalTips)
        //            .HasForeignKey(d => d.RecipeId)
        //            .HasConstraintName("R_20");
        //    });

        //    modelBuilder.Entity<Comment>(entity =>
        //    {
        //        entity.ToTable("Comment");

        //        entity.HasIndex(e => e.ParentCommentId, "IX_Comment_parent_comment_id");

        //        entity.HasIndex(e => e.RecipeId, "IX_Comment_recipe_id");

        //        entity.HasIndex(e => e.UserId, "IX_Comment_user_id");

        //        entity.Property(e => e.Id)
        //            .ValueGeneratedNever()
        //            .HasColumnName("id");

        //        entity.Property(e => e.Content)
        //            .HasColumnType("text")
        //            .HasColumnName("content");

        //        entity.Property(e => e.DateCreated)
        //            .HasColumnType("datetime")
        //            .HasColumnName("dateCreated");

        //        entity.Property(e => e.ParentCommentId).HasColumnName("parent_comment_id");

        //        entity.Property(e => e.RecipeId).HasColumnName("recipe_id");

        //        entity.Property(e => e.UserId).HasColumnName("user_id");

        //        entity.HasOne(d => d.ParentComment)
        //            .WithMany(p => p.InverseParentComment)
        //            .HasForeignKey(d => d.ParentCommentId)
        //            .HasConstraintName("R_33");

        //        entity.HasOne(d => d.Recipe)
        //            .WithMany(p => p.Comments)
        //            .HasForeignKey(d => d.RecipeId)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("FK_Comment_Recipe");

        //        entity.HasOne(d => d.User)
        //            .WithMany(p => p.Comments)
        //            .HasForeignKey(d => d.UserId)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("FK_Comment_User");
        //    });

        //    modelBuilder.Entity<DishType>(entity =>
        //    {
        //        entity.ToTable("DishType");

        //        entity.Property(e => e.Id)
        //            .ValueGeneratedNever()
        //            .HasColumnName("id");

        //        entity.Property(e => e.Name)
        //            .HasMaxLength(20)
        //            .IsUnicode(false)
        //            .HasColumnName("name");
        //    });

        //    modelBuilder.Entity<FavoriteRecipe>(entity =>
        //    {
        //        entity.ToTable("FavoriteRecipe");

        //        entity.HasIndex(e => e.RecipeId, "IX_FavoriteRecipe_recipe_id");

        //        entity.HasIndex(e => e.UserId, "IX_FavoriteRecipe_user_id");

        //        entity.Property(e => e.Id)
        //            .ValueGeneratedNever()
        //            .HasColumnName("id");

        //        entity.Property(e => e.RecipeId).HasColumnName("recipe_id");

        //        entity.Property(e => e.UserId).HasColumnName("user_id");

        //        entity.HasOne(d => d.Recipe)
        //            .WithMany(p => p.FavoriteRecipes)
        //            .HasForeignKey(d => d.RecipeId)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("FK_FavoriteRecipe_Recipe");

        //        entity.HasOne(d => d.User)
        //            .WithMany(p => p.FavoriteRecipes)
        //            .HasForeignKey(d => d.UserId)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("FK_FavoriteRecipe_User");
        //    });

        //    modelBuilder.Entity<FoodType>(entity =>
        //    {
        //        entity.ToTable("FoodType");

        //        entity.Property(e => e.Id)
        //            .ValueGeneratedNever()
        //            .HasColumnName("id");

        //        entity.Property(e => e.Name)
        //            .HasMaxLength(20)
        //            .IsUnicode(false)
        //            .HasColumnName("name");
        //    });

        //    modelBuilder.Entity<Ingredient>(entity =>
        //    {
        //        entity.ToTable("Ingredient");

        //        entity.Property(e => e.Id)
        //            .ValueGeneratedNever()
        //            .HasColumnName("id");

        //        entity.Property(e => e.Name)
        //            .HasMaxLength(20)
        //            .IsUnicode(false)
        //            .HasColumnName("name");
        //    });

        //    modelBuilder.Entity<Measurement>(entity =>
        //    {
        //        entity.ToTable("Measurement");

        //        entity.Property(e => e.Id)
        //            .ValueGeneratedNever()
        //            .HasColumnName("id");

        //        entity.Property(e => e.Name)
        //            .HasMaxLength(20)
        //            .IsUnicode(false)
        //            .HasColumnName("name");
        //    });

        //    modelBuilder.Entity<MenuType>(entity =>
        //    {
        //        entity.ToTable("MenuType");

        //        entity.Property(e => e.Id)
        //            .ValueGeneratedNever()
        //            .HasColumnName("id");

        //        entity.Property(e => e.Name)
        //            .HasMaxLength(20)
        //            .IsUnicode(false)
        //            .HasColumnName("name");
        //    });

        //    modelBuilder.Entity<MenuTypeList>(entity =>
        //    {
        //        entity.ToTable("MenuTypeList");

        //        entity.HasIndex(e => e.MenuTypeId, "IX_MenuTypeList_menuType_id");

        //        entity.HasIndex(e => e.RecipeId, "IX_MenuTypeList_recipe_id");

        //        entity.Property(e => e.Id)
        //            .ValueGeneratedNever()
        //            .HasColumnName("id");

        //        entity.Property(e => e.MenuTypeId).HasColumnName("menuType_id");

        //        entity.Property(e => e.RecipeId).HasColumnName("recipe_id");

        //        entity.HasOne(d => d.MenuType)
        //            .WithMany(p => p.MenuTypeLists)
        //            .HasForeignKey(d => d.MenuTypeId)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("R_19");

        //        entity.HasOne(d => d.Recipe)
        //            .WithMany(p => p.MenuTypeLists)
        //            .HasForeignKey(d => d.RecipeId)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("R_18");
        //    });

        //    modelBuilder.Entity<PatternIngridientList>(entity =>
        //    {
        //        entity.ToTable("PatternIngridientList");

        //        entity.HasIndex(e => e.IngridientId, "IX_PatternIngridientList_ingridient_id");

        //        entity.HasIndex(e => e.SearchPatternId, "IX_PatternIngridientList_searchPattern_id");

        //        entity.Property(e => e.Id)
        //            .ValueGeneratedNever()
        //            .HasColumnName("id");

        //        entity.Property(e => e.IngridientId).HasColumnName("ingridient_id");

        //        entity.Property(e => e.SearchPatternId).HasColumnName("searchPattern_id");

        //        entity.HasOne(d => d.Ingridient)
        //            .WithMany(p => p.PatternIngridientLists)
        //            .HasForeignKey(d => d.IngridientId)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("R_13");

        //        entity.HasOne(d => d.SearchPattern)
        //            .WithMany(p => p.PatternIngridientLists)
        //            .HasForeignKey(d => d.SearchPatternId)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("R_12");
        //    });

        //    modelBuilder.Entity<PatternMenuType>(entity =>
        //    {
        //        entity.ToTable("PatternMenuType");

        //        entity.HasIndex(e => e.MenuTypeId, "IX_PatternMenuType_menuType_id");

        //        entity.HasIndex(e => e.SearchPatternId, "IX_PatternMenuType_searchPattern_id");

        //        entity.Property(e => e.Id)
        //            .ValueGeneratedNever()
        //            .HasColumnName("id");

        //        entity.Property(e => e.MenuTypeId).HasColumnName("menuType_id");

        //        entity.Property(e => e.SearchPatternId).HasColumnName("searchPattern_id");

        //        entity.HasOne(d => d.MenuType)
        //            .WithMany(p => p.PatternMenuTypes)
        //            .HasForeignKey(d => d.MenuTypeId)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("R_15");

        //        entity.HasOne(d => d.SearchPattern)
        //            .WithMany(p => p.PatternMenuTypes)
        //            .HasForeignKey(d => d.SearchPatternId)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("R_14");
        //    });

        //    modelBuilder.Entity<PreparationTip>(entity =>
        //    {
        //        entity.HasIndex(e => e.RecipeId, "IX_PreparationTips_recipe_id");

        //        entity.Property(e => e.Id).HasColumnName("id");

        //        entity.Property(e => e.Description)
        //            .HasColumnType("text")
        //            .HasColumnName("description");

        //        entity.Property(e => e.RecipeId).HasColumnName("recipe_id");

        //        entity.HasOne(d => d.Recipe)
        //            .WithMany(p => p.PreparationTips)
        //            .HasForeignKey(d => d.RecipeId)
        //            .HasConstraintName("R_22");
        //    });

        //    modelBuilder.Entity<Recipe>(entity =>
        //    {
        //        entity.ToTable("Recipe");

        //        entity.HasIndex(e => e.DishTypeId, "IX_Recipe_dishType_id");

        //        entity.HasIndex(e => e.FoodTypeId, "IX_Recipe_foodType_id");

        //        entity.HasIndex(e => e.UserId, "IX_Recipe_user_id");

        //        entity.Property(e => e.Id).HasColumnName("id");

        //        entity.Property(e => e.AmountOfFavorites).HasColumnName("amountOfFavorites");

        //        entity.Property(e => e.AmountOfRates).HasColumnName("amountOfRates");

        //        entity.Property(e => e.AmountOfServings).HasColumnName("amountOfServings");

        //        entity.Property(e => e.CaloricValue).HasColumnName("caloricValue");

        //        entity.Property(e => e.CreationDate)
        //            .HasColumnType("datetime")
        //            .HasColumnName("creationDate");

        //        entity.Property(e => e.Description)
        //            .HasColumnType("text")
        //            .HasColumnName("description");

        //        entity.Property(e => e.Difficulty).HasColumnName("difficulty");

        //        entity.Property(e => e.DishTypeId).HasColumnName("dishType_id");

        //        entity.Property(e => e.Fats).HasColumnName("fats");

        //        entity.Property(e => e.FoodTypeId).HasColumnName("foodType_id");

        //        entity.Property(e => e.IsPublished).HasColumnName("isPublished");

        //        entity.Property(e => e.Name)
        //            .HasMaxLength(20)
        //            .IsUnicode(false)
        //            .HasColumnName("name");

        //        entity.Property(e => e.Proteins).HasColumnName("proteins");

        //        entity.Property(e => e.Rating)
        //            .HasColumnType("decimal(2, 1)")
        //            .HasColumnName("rating");

        //        entity.Property(e => e.RequiredTime).HasColumnName("requiredTime");

        //        entity.Property(e => e.UserId).HasColumnName("user_id");

        //        entity.Property(e => e.Video)
        //            .HasMaxLength(20)
        //            .IsUnicode(false)
        //            .HasColumnName("video");

        //        entity.HasOne(d => d.DishType)
        //            .WithMany(p => p.Recipes)
        //            .HasForeignKey(d => d.DishTypeId)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("R_11");

        //        entity.HasOne(d => d.FoodType)
        //            .WithMany(p => p.Recipes)
        //            .HasForeignKey(d => d.FoodTypeId)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("R_10");

        //        entity.HasOne(d => d.User)
        //            .WithMany(p => p.Recipes)
        //            .HasForeignKey(d => d.UserId)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("R_9");
        //    });

        //    modelBuilder.Entity<RecipeImage>(entity =>
        //    {
        //        entity.ToTable("RecipeImage");

        //        entity.HasIndex(e => e.RecipeId, "IX_RecipeImage_recipe_id");

        //        entity.Property(e => e.Id)
        //            .ValueGeneratedNever()
        //            .HasColumnName("id");

        //        entity.Property(e => e.Image)
        //            .HasMaxLength(50)
        //            .IsUnicode(false)
        //            .HasColumnName("image");

        //        entity.Property(e => e.RecipeId).HasColumnName("recipe_id");

        //        entity.HasOne(d => d.Recipe)
        //            .WithMany(p => p.RecipeImages)
        //            .HasForeignKey(d => d.RecipeId)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("R_23");
        //    });

        //    modelBuilder.Entity<RecipeIngredient>(entity =>
        //    {
        //        entity.ToTable("RecipeIngredient");

        //        entity.HasIndex(e => e.IngredientId, "IX_RecipeIngredient_ingredient_id");

        //        entity.HasIndex(e => e.MeasurementId, "IX_RecipeIngredient_measurement_id");

        //        entity.HasIndex(e => e.RecipeId, "IX_RecipeIngredient_recipe_id");

        //        entity.Property(e => e.Id)
        //            .ValueGeneratedNever()
        //            .HasColumnName("id");

        //        entity.Property(e => e.IngredientId).HasColumnName("ingredient_id");

        //        entity.Property(e => e.MeasurementId).HasColumnName("measurement_id");

        //        entity.Property(e => e.RecipeId).HasColumnName("recipe_id");

        //        entity.HasOne(d => d.Ingredient)
        //            .WithMany(p => p.RecipeIngredients)
        //            .HasForeignKey(d => d.IngredientId)
        //            .HasConstraintName("R_25");

        //        entity.HasOne(d => d.Measurement)
        //            .WithMany(p => p.RecipeIngredients)
        //            .HasForeignKey(d => d.MeasurementId)
        //            .HasConstraintName("R_26");

        //        entity.HasOne(d => d.Recipe)
        //            .WithMany(p => p.RecipeIngredients)
        //            .HasForeignKey(d => d.RecipeId)
        //            .HasConstraintName("R_24");
        //    });

        //    modelBuilder.Entity<RecipeRating>(entity =>
        //    {
        //        entity.ToTable("RecipeRating");

        //        entity.HasIndex(e => e.RecipeId, "IX_RecipeRating_recipe_id");

        //        entity.HasIndex(e => e.UserId, "IX_RecipeRating_user_id");

        //        entity.Property(e => e.Id).HasColumnName("id");

        //        entity.Property(e => e.Rate).HasColumnName("rate");

        //        entity.Property(e => e.RecipeId).HasColumnName("recipe_id");

        //        entity.Property(e => e.UserId).HasColumnName("user_id");

        //        entity.HasOne(d => d.Recipe)
        //            .WithMany(p => p.RecipeRatings)
        //            .HasForeignKey(d => d.RecipeId)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("FK_RecipeRating_Recipe");

        //        entity.HasOne(d => d.User)
        //            .WithMany(p => p.RecipeRatings)
        //            .HasForeignKey(d => d.UserId)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("FK_RecipeRating_User");
        //    });

        //    modelBuilder.Entity<RecipeStep>(entity =>
        //    {
        //        entity.ToTable("RecipeStep");

        //        entity.HasIndex(e => e.RecipeId, "IX_RecipeStep_recipe_id");

        //        entity.Property(e => e.Id).HasColumnName("id");

        //        entity.Property(e => e.Description)
        //            .HasColumnType("text")
        //            .HasColumnName("description");

        //        entity.Property(e => e.Image)
        //            .HasMaxLength(50)
        //            .IsUnicode(false)
        //            .HasColumnName("image");

        //        entity.Property(e => e.RecipeId).HasColumnName("recipe_id");

        //        entity.Property(e => e.StepNumber).HasColumnName("stepNumber");

        //        entity.Property(e => e.Title)
        //            .HasMaxLength(20)
        //            .IsUnicode(false)
        //            .HasColumnName("title");

        //        entity.HasOne(d => d.Recipe)
        //            .WithMany(p => p.RecipeSteps)
        //            .HasForeignKey(d => d.RecipeId)
        //            .HasConstraintName("R_16");
        //    });

        //    modelBuilder.Entity<SearchPattern>(entity =>
        //    {
        //        entity.ToTable("SearchPattern");

        //        entity.Property(e => e.Id)
        //            .ValueGeneratedNever()
        //            .HasColumnName("id");

        //        entity.Property(e => e.Difficulty).HasColumnName("difficulty");

        //        entity.Property(e => e.DishTypeId).HasColumnName("dishType_id");

        //        entity.Property(e => e.FilterString)
        //            .HasMaxLength(30)
        //            .IsUnicode(false)
        //            .HasColumnName("filterString");

        //        entity.Property(e => e.FoodTypeId).HasColumnName("foodType_id");

        //        entity.Property(e => e.MaxReqTime).HasColumnName("maxReqTime");

        //        entity.Property(e => e.MinReqTime).HasColumnName("minReqTime");

        //        entity.Property(e => e.Name)
        //            .HasMaxLength(30)
        //            .IsUnicode(false)
        //            .HasColumnName("name");

        //        entity.Property(e => e.SearchType)
        //            .HasMaxLength(20)
        //            .IsUnicode(false)
        //            .HasColumnName("searchType");

        //        entity.Property(e => e.SortType)
        //            .HasMaxLength(20)
        //            .IsUnicode(false)
        //            .HasColumnName("sortType");

        //        entity.Property(e => e.UserId).HasColumnName("user_id");
        //    });

        //    modelBuilder.Entity<Subscription>(entity =>
        //    {
        //        entity.ToTable("Subscription");

        //        entity.HasIndex(e => e.AuthorId, "IX_Subscription_author_id");

        //        entity.HasIndex(e => e.UserId, "IX_Subscription_user_id");

        //        entity.Property(e => e.Id).HasColumnName("id");

        //        entity.Property(e => e.AuthorId).HasColumnName("author_id");

        //        entity.Property(e => e.UserId).HasColumnName("user_id");

        //        entity.HasOne(d => d.Author)
        //            .WithMany(p => p.SubscriptionAuthors)
        //            .HasForeignKey(d => d.AuthorId)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("FK_Subscription_User1");

        //        entity.HasOne(d => d.User)
        //            .WithMany(p => p.SubscriptionUsers)
        //            .HasForeignKey(d => d.UserId)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("FK_Subscription_User");
        //    });

        //    modelBuilder.Entity<User>(entity =>
        //    {
        //        entity.ToTable("User");

        //        entity.Property(e => e.Id).HasColumnName("id");

        //        entity.Property(e => e.Description)
        //            .HasColumnType("text")
        //            .HasColumnName("description");

        //        entity.Property(e => e.UserName)
        //            .HasMaxLength(20)
        //            .IsUnicode(false)
        //            .HasColumnName("userName");

        //        entity.Property(e => e.Image)
        //            .HasMaxLength(50)
        //            .IsUnicode(false)
        //            .HasColumnName("image");

        //        entity.Property(e => e.Email)
        //            .HasMaxLength(40)
        //            .IsUnicode(false)
        //            .HasColumnName("email");

        //        entity.Property(e => e.PasswordHash)
        //            .HasMaxLength(255)
        //            .IsUnicode(false)
        //            .HasColumnName("passwordHash");
        //    });

        //    OnModelCreatingPartial(modelBuilder);
        //}

        //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
