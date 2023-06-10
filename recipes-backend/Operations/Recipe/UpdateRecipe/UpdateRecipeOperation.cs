using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using recipes_backend.Common;
using recipes_backend.Models;
using recipes_backend.Operations.Recipe.RecipeInfo;
using recipes_backend.Services;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;

namespace recipes_backend.Operations.Recipe.UpdateRecipe
{
    public class UpdateRecipeOperation
    {
        recipesContext db;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ImageService imageService;

        public UpdateRecipeOperation(recipesContext db, IMapper mapper, IHttpContextAccessor httpContextAccessor, ImageService imgService)
        {
            this.db = db;
            this._mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            imageService = imgService;

        }

        public async Task<UpdateRecipeResponse> Execute(UpdateRecipeRequest request)
        {
            var validate = await Validate(request);
            if (validate.Code != 200)
            {
                return new UpdateRecipeResponse { Code = validate.Code, Message = validate.Message };
            }

            string Email = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
            if (Email != null)
            {
                var user = await db.Users.Where(x => x.Mail == Email).FirstOrDefaultAsync();
                var recipe = await db.Recipes.Where(x => x.Id == request.Id).FirstOrDefaultAsync();
                if (user.Id == recipe.UserId)
                {
                    recipe.Name = request.Name;
                    recipe.Description = request.Description;
                    recipe.Difficulty = request.Difficulty;
                    recipe.RequiredTime = request.RequiredTime;
                    recipe.Servings = request.Servings;
                    recipe.CaloricValue = request.CaloricValue;
                    recipe.Proteins = request.Proteins;
                    recipe.Fats = request.Fats;
                    recipe.Carbohydrates = request.Carbohydrates;
                    recipe.Video = request.Video;
                    recipe.FoodTypeId = request.FoodType;
                    recipe.DishTypeId = request.DishType;
                    recipe.CreationDate = (request.IsPublished && !recipe.IsPublished ? DateTime.Now : null);
                    recipe.IsPublished = request.IsPublished;
                    await db.SaveChangesAsync();

                    var remPrepTips = db.PreparationTips.Where(x => x.RecipeId == request.Id);
                    db.PreparationTips.RemoveRange(remPrepTips);
                    var remAdditTips = db.AdditionalTips.Where(x => x.RecipeId == request.Id);
                    db.AdditionalTips.RemoveRange(remAdditTips);
                    var remMenuTypes = db.MenuTypeLists.Where(x => x.RecipeId == request.Id).ToList();
                    db.MenuTypeLists.RemoveRange(remMenuTypes);
                    var remIngredients = db.RecipeIngredients.Where(x => x.RecipeId == request.Id);
                    db.RecipeIngredients.RemoveRange(remIngredients);
                    await db.SaveChangesAsync();

                    if (request.PreparationTips != null) 
                    {
                        List<PreparationTip> newPreps = new List<PreparationTip>();
                        for (int i = 0; i < request.PreparationTips.Count; i++)
                        {
                            if (request.PreparationTips[i] != null)
                            {
                                newPreps.Add(new PreparationTip
                                {
                                    RecipeId = recipe.Id,
                                    Description = request.PreparationTips[i]
                                });
                            }
                        }
                        await db.PreparationTips.AddRangeAsync(newPreps);
                        await db.SaveChangesAsync();
                    }

                    if (request.AdditionalTips != null)
                    {
                        List<AdditionalTip> newAddit = new List<AdditionalTip>();
                        for (int i = 0; i < request.AdditionalTips.Count; i++)
                        {
                            if (request.AdditionalTips[i] != null)
                            {
                                newAddit.Add(new AdditionalTip
                                {
                                    RecipeId = recipe.Id,
                                    Description = request.AdditionalTips[i]
                                });
                            }
                        }
                        await db.AdditionalTips.AddRangeAsync(newAddit);
                        await db.SaveChangesAsync();
                    }

                    if (request.MenuTypes != null)
                    {
                        List<MenuTypeList> newMenus = new List<MenuTypeList>();
                        for (int i = 0; i < request.MenuTypes.Count; i++)
                        {
                            newMenus.Add(new MenuTypeList
                            {
                                RecipeId = recipe.Id,
                                MenuTypeId = request.MenuTypes[i],
                            });
                        }
                        await db.MenuTypeLists.AddRangeAsync(newMenus);
                        await db.SaveChangesAsync();
                    }

                    if (request.IngredientsId != null)
                    {
                        List<RecipeIngredient> newIngr = new List<RecipeIngredient>();
                        for (int i = 0; i < request.IngredientsId.Count; i++)
                        {
                            newIngr.Add(new RecipeIngredient
                            {
                                RecipeId = recipe.Id,
                                IngredientId = request.IngredientsId[i],
                                MeasurementId = request.IngredientsMeasurementId[i],
                                Amount = request.IngredientsAmount[i],
                            });
                        }
                        await db.RecipeIngredients.AddRangeAsync(newIngr);
                        await db.SaveChangesAsync();
                    }

                    var remSteps = db.RecipeSteps.Where(x => x.RecipeId == request.Id && (request.StepsIds==null || !request.StepsIds.Any(a => a == x.Id)));
                    db.RecipeSteps.RemoveRange(remSteps);
                    if (request.BackendStepImageToDelete != null)
                    {
                        var remStepImgs = db.RecipeSteps.Where(x => x.RecipeId == request.Id && request.BackendStepImageToDelete.Any(a => a == x.Id));
                        var e = remStepImgs.ToList();
                        if(remStepImgs != null)
                        {
                            foreach(var step in remStepImgs)
                            {
                                if (step.Image != null)
                                {
                                    imageService.DeleteImageByPath(step.Image);
                                }
                                step.Image = null;
                            }
                        }
                    }
                    await db.SaveChangesAsync();
                    if (request.StepsIds != null)
                    {
                        for (int i = 0; i < request.StepsIds.Count; i++)
                        {
                            var step = request.StepsIds[i];
                            if (step != 0)
                            {
                                var stepUpdate = await db.RecipeSteps.Where(x => x.Id == step).FirstAsync();
                                stepUpdate.Title = request.StepsTitles[i];
                                stepUpdate.Description = request.StepsDescriptions[i];
                                if (request.StepImagesData[i].Length != 0)
                                {
                                    stepUpdate.Image = await imageService.SaveImage(request.StepImagesData[i], "Images", "RecipeSteps");
                                }
                            }
                            else
                            {
                                var newStep = new RecipeStep
                                {
                                    RecipeId = recipe.Id,
                                    Title = request.StepsTitles[i],
                                    Description = request.StepsDescriptions[i],
                                    Image = (
                                        request.StepImagesData[i].Length != 0 ?
                                        await imageService.SaveImage(request.StepImagesData[i], "Images", "RecipeSteps") :
                                        null
                                    ),
                                };
                                await db.RecipeSteps.AddAsync(newStep);
                            }
                        }
                    }
                    await db.SaveChangesAsync();
                    var remImages = db.RecipeImages.Where(x => x.RecipeId == request.Id && (request.ImagesIndexes == null || !request.ImagesIndexes.Any(a => a == x.Id)));
                    db.RecipeImages.RemoveRange(remImages);
                    if (request.ImagesIndexes != null)
                    {
                        for (int i = 0; i < request.ImagesIndexes.Count; i++)
                        {
                            if (request.ImagesIndexes[i] == 0)
                            {
                                await db.RecipeImages.AddAsync(new RecipeImage
                                {
                                    Image = await imageService.SaveImage(request.ImagesData[i], "Images", "Recipes"),
                                    RecipeId = recipe.Id,
                                });
                            }
                        }
                    }
                    await db.SaveChangesAsync();

                    return new UpdateRecipeResponse();
                }
                else
                {
                    return new UpdateRecipeResponse { Code = 401, Message = "You have no acces to this recipe" };
                }
            }
            else
            {
                return new UpdateRecipeResponse() { Code = 401, Message = "You have no acces to this recipe" };
            }
        }

        public async Task<ValidateResult> Validate(UpdateRecipeRequest request)
        {
            return new ValidateResult();
        }
    }
}
