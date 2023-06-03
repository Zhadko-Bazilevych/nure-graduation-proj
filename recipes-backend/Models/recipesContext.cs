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
            //Database.EnsureCreated();

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
        public virtual DbSet<PatternIngredientList> PatternIngredientLists { get; set; } = null!;
        public virtual DbSet<PatternMenuType> PatternMenuTypes { get; set; } = null!;
        public virtual DbSet<PatternDishType> PatternDishTypes { get; set; } = null!;
        public virtual DbSet<PatternFoodType> PatternFoodTypes { get; set; } = null!;
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
                db.Users.Add(new User
                {
                    Name = "Сергій Жадько-Базілевич",
                    Image = "Images/Users/Default.png",
                    Mail = "lysergidi@gmail.com"
                });
                db.FoodTypes.AddRange(new List<FoodType>
                {
                    new FoodType { Name = "Французька" },
                    new FoodType { Name = "Японська" },
                    new FoodType { Name = "Китайська" },
                    new FoodType { Name = "Українська" },
                });
                db.DishTypes.AddRange( new List<DishType>
                {
                    new DishType { Name = "Випічка" },
                    new DishType { Name = "Салат" },
                    new DishType { Name = "Закуска" },
                    new DishType { Name = "Суп" },
                    new DishType { Name = "Десерт" },
                    new DishType { Name = "Вареники" },
                    new DishType { Name = "Борщ" },
                });
                db.MenuTypes.AddRange(new List<MenuType>
                {
                    new MenuType { Name = "Веганське" },
                    new MenuType { Name = "Вегетаріанське" },
                    new MenuType { Name = "Пісне" },
                    new MenuType { Name = "Безглютенове" },
                    new MenuType { Name = "Малокалорійне" },
                    new MenuType { Name = "Сезонне" },
                    new MenuType { Name = "TestMenuType" },
                });
                db.Ingredients.AddRange(new List<Ingredient>
                {
                    new Ingredient { Name = "Яйце" },
                    new Ingredient { Name = "Мука" },
                    new Ingredient { Name = "Сіль" },
                    new Ingredient { Name = "Картофель" },
                    new Ingredient { Name = "Цибуля ріпчаста" },
                    new Ingredient { Name = "Перець чорний" },
                    new Ingredient { Name = "Олія" },
                    new Ingredient { Name = "Сметана" }
                });
                db.Measurements.AddRange(new List<Measurement>
                {
                    new Measurement{ Name = "гр." },
                    new Measurement{ Name = "мл." },
                    new Measurement{ Name = "шт." },
                    new Measurement{ Name = "ст.л." },
                    new Measurement{ Name = "щіпка(и)" },
                });
                db.SaveChanges();
                db.Recipes.Add(new Recipe
                {
                    UserId = 1,
                    Name = "Вареники з картоплею",
                    Description = "Готуємо дуже смачні домашні вареники з картоплею, ніжне тісто для вареників. Просте та недороге блюдо. Готуємо дуже смачні домашні вареники з картоплею, ніжне тісто для вареників. Просте та недороге блюдо. Готуємо дуже смачні домашні вареники з картоплею, ніжне тісто для вареників. Просте та недороге блюдо. Готуємо дуже смачні домашні вареники з картоплею, ніжне тісто для вареників. Просте та недороге блюдо.",
                    Difficulty = 2,
                    RequiredTime = 30,
                    Servings = 6,
                    CaloricValue = 157.37,
                    Proteins = 4.7,
                    Fats = 4.9,
                    Carbohydrates = 30.1,
                    FoodTypeId = 4,
                    DishTypeId = 6,
                    CreationDate = DateTime.Now,
                    IsPublished = true,
                    AmountOfServings = 6,
                });
                db.Recipes.Add(new Recipe
                {
                    UserId = 1,
                    Name = "Боржч смачний",
                    Description = "Звичайний борщ. Нічого зайвого.",
                    Difficulty = 3,
                    RequiredTime = 120,
                    Servings = 6,
                    CaloricValue = 57.7,
                    Proteins = 3.8,
                    Fats = 2.9,
                    Carbohydrates = 4.3,
                    FoodTypeId = 4,
                    DishTypeId = 7,
                    CreationDate = DateTime.Now,
                    IsPublished = true,
                    AmountOfServings = 6,
                });
                db.SaveChanges();
                db.MenuTypeLists.AddRange(new List<MenuTypeList>
                {
                    new MenuTypeList { RecipeId = 1, MenuTypeId = 2 },
                    new MenuTypeList { RecipeId = 1, MenuTypeId = 7 },
                });

                db.RecipeIngredients.AddRange(new List<RecipeIngredient>
                {
                    new RecipeIngredient{ RecipeId = 1, IngredientId = 1, MeasurementId = 3, Amount = 1 },
                    new RecipeIngredient{ RecipeId = 1, IngredientId = 2, MeasurementId = 1, Amount = 300 },
                    new RecipeIngredient{ RecipeId = 1, IngredientId = 3, MeasurementId = 5, Amount = 1 },
                    new RecipeIngredient{ RecipeId = 1, IngredientId = 4, MeasurementId = 1, Amount = 600 },
                    new RecipeIngredient{ RecipeId = 1, IngredientId = 5, MeasurementId = 3, Amount = 1 },
                    new RecipeIngredient{ RecipeId = 1, IngredientId = 6, MeasurementId = 5, Amount = 1 },
                    new RecipeIngredient{ RecipeId = 1, IngredientId = 7, MeasurementId = 2, Amount = 30 },
                    new RecipeIngredient{ RecipeId = 1, IngredientId = 8, MeasurementId = 1, Amount = 100 }
                });
                db.PreparationTips.AddRange(new List<PreparationTip>
                {
                    new PreparationTip {RecipeId = 1, Description = "Вареники - це традиційна українська страва, яка складається з тонкого тіста, що обгортає різноманітну начинку. Вони мають круглу форму і готуються шляхом скручування тіста. Тісто для вареників робиться з муки, води та солі. За начинку можна використовувати різноманітні інгредієнти - від картоплі та сиру до фруктів і ягід. Наприклад, вареники з картоплею - це класичний варіант, де картопля тушкується, а потім заповнюється у вареник."},
                    new PreparationTip {RecipeId = 1, Description = "Вареники можна подавати з вершковим маслом або песто. Вареники з сиром - ще один популярний варіант, де сир змішується з цукром та ваніллю, а потім кладеться в тісто. Їх можна полити сметаною або солодким соусом. На десерт можна приготувати вареники з полуницею або вишнею, де свіжі ягоди кладуться в середину тіста і запікаються."}
                });
                db.AdditionalTips.AddRange(new List<AdditionalTip>
                {
                    new AdditionalTip {RecipeId = 1, Description = "Вареники можна варити у киплячій воді або смажити на сковороді з олією. Вони чудово поєднуються з різними соусами, такими як грибний соус, томатний соус або сметана. Вареники - це улюблена страва на святковому столі та популярний варіант для сімейного обіду."},
                });
                db.RecipeSteps.AddRange(new List<RecipeStep>
                {
                    new RecipeStep { RecipeId = 1, StepNumber = 1, Title = "Підготовлюємо картоплю", Description = "Беремо 600 грам картоплі (4-5 штук), чистимо від кожури та ріжемо на чотири частини кожну.", Image = "Images/RecipeSteps/PotatoDumplings1.png"},
                    new RecipeStep { RecipeId = 1, StepNumber = 2, Title = "Варимо картоплю", Description = "Варимо картоплю у кастрюлі з підсоленою водою 25 хвилин."},
                    new RecipeStep { RecipeId = 1, StepNumber = 3, Title = "Готуємо цибулю", Description = "Дрібно нарізаємо цибулю та обсмажуємо на сковорідці на малому вогні до золотистої скоринки.", Image = "Images/RecipeSteps/PotatoDumplings3.png"},
                    new RecipeStep { RecipeId = 1, StepNumber = 4, Title = "Готуємо тісто", Description = "Додаємо у посудину 110мл води, 1 яйце, пару грамів солі та 300гр муки. Перемішуємо тісто до однорідної консистенції.", Image = "Images/RecipeSteps/PotatoDumplings4.png"},
                    new RecipeStep { RecipeId = 1, StepNumber = 5, Title = "Замішуємо начинку пельменів", Description = "Після того, як картопля зварилася, перетовчемо картоплю для перетворення вареної картоплі в пюреподібну субстанцію. Додаємо жарену цибулю та чорний перець на свій смак.", Image = "Images/RecipeSteps/PotatoDumplings5.png"},
                    new RecipeStep { RecipeId = 1, StepNumber = 6, Title = "Оброблюємо тісто", Description = "Розкатуємо тісто та формуємо круглі зліпки 6 см у діаметрі. Повинно вийти 40-45 заготівок під вареники.", Image = "Images/RecipeSteps/PotatoDumplings6.png"},
                    new RecipeStep { RecipeId = 1, StepNumber = 7, Title = "Начиняємо вареник", Description = "Кладемо близько 10 грамів начинки на кожен вареник та згортаємо зліпок у полукруг, зліплюючи кінці вареника.", Image = "Images/RecipeSteps/PotatoDumplings7.png"},
                    new RecipeStep { RecipeId = 1, StepNumber = 8, Title = "Результат", Description = "Готові вареники заморожуємо. За надібністю варимо готові вареники не більше 8 хвилин.", Image = "Images/RecipeSteps/PotatoDumplings8.png"},
                });
                db.RecipeImages.AddRange(new List<RecipeImage>
                {
                    new RecipeImage { RecipeId = 1, Image = "Images/Recipes/PotatoDumplingsI1.png"},
                    new RecipeImage { RecipeId = 1, Image = "Images/Recipes/PotatoDumplingsI2.png"},
                    new RecipeImage { RecipeId = 1, Image = "Images/Recipes/PotatoDumplingsI3.png"},
                });
                db.SaveChanges();

                db.SearchPatterns.AddRange(new List<SearchPattern>
                {
                    new SearchPattern { isDescending = true, FilterString="Вар", DifficultyMin=1, DifficultyMax=4, MinReqTime=15, MaxReqTime=90, Name="Фільтр для вареників", asIngredientPool=false, SortType="Назва", UserId=1},
                    new SearchPattern { isDescending = true, FilterString="еники", DifficultyMin=1, DifficultyMax=4, MinReqTime=15, MaxReqTime=90, Name="Інший фільтр", asIngredientPool=false, SortType="Назва", UserId=1},
                    new SearchPattern { isDescending = true, asIngredientPool=false, DifficultyMin=1, DifficultyMax=5, Name="Порожній", SortType="Нещодавні", UserId=1},
                });
                db.SaveChanges();

                db.PatternDishTypes.AddRange(new List<PatternDishType>
                {
                    new PatternDishType { DishTypeId = 6, SearchPatternId = 1},
                    new PatternDishType { DishTypeId = 7, SearchPatternId = 1},
                });

                db.PatternMenuTypes.AddRange(new List<PatternMenuType>
                {
                    new PatternMenuType { MenuTypeId = 2, SearchPatternId = 1},
                    new PatternMenuType { MenuTypeId = 7, SearchPatternId = 1},
                });

                db.PatternFoodTypes.AddRange(new List<PatternFoodType>
                {
                    new PatternFoodType { FoodTypeId = 2, SearchPatternId = 1},
                    new PatternFoodType { FoodTypeId = 4, SearchPatternId = 1},
                });

                db.PatternIngredientLists.AddRange(new List<PatternIngredientList>
                {
                    new PatternIngredientList { IngredientId = 2, SearchPatternId = 1},
                    new PatternIngredientList { IngredientId = 4, SearchPatternId = 1},
                    new PatternIngredientList { IngredientId = 5, SearchPatternId = 1},
                });

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

        //    modelBuilder.Entity<PatternIngredientList>(entity =>
        //    {
        //        entity.ToTable("PatternIngredientList");

        //        entity.HasIndex(e => e.IngredientId, "IX_PatternIngredientList_ingredient_id");

        //        entity.HasIndex(e => e.SearchPatternId, "IX_PatternIngredientList_searchPattern_id");

        //        entity.Property(e => e.Id)
        //            .ValueGeneratedNever()
        //            .HasColumnName("id");

        //        entity.Property(e => e.IngredientId).HasColumnName("ingredient_id");

        //        entity.Property(e => e.SearchPatternId).HasColumnName("searchPattern_id");

        //        entity.HasOne(d => d.Ingredient)
        //            .WithMany(p => p.PatternIngredientLists)
        //            .HasForeignKey(d => d.IngredientId)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("R_13");

        //        entity.HasOne(d => d.SearchPattern)
        //            .WithMany(p => p.PatternIngredientLists)
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
