using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using recipes_backend.Common;
using recipes_backend.Models;
using recipes_backend.Operations.Recipe.PatternUpdate;
using recipes_backend.Operations.Recipe.Rate;
using recipes_backend.Services;
using System.Security.Claims;
using System.Security.Cryptography;

namespace recipes_backend.Operations.Recipe.PatternUpdate
{
    public class PatternUpdateOperation
    {
        recipesContext db;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PatternUpdateOperation(recipesContext db, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.db = db;
            this._mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<PatternUpdateResponse> Execute(PatternUpdateRequest request)
        {
            var validate = await Validate(request);
            if (validate.Code != 200)
            {
                return new PatternUpdateResponse { Code = validate.Code, Message = validate.Message };
            }
            string Email = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
            if (Email == null)
            {
                return new PatternUpdateResponse { Code = 401 };
            }
            var user = await db.Users.Where(x => x.Mail == Email).FirstOrDefaultAsync();

            var isExists = await db.SearchPatterns.AnyAsync(x => x.Id == request.Id);
            if (isExists)
            {
                var sp = await db.SearchPatterns.Where(x => x.Id == request.Id).FirstOrDefaultAsync();
                sp.DifficultyMin = request.DifficultyMin;
                sp.DifficultyMax = request.DifficultyMax;
                sp.asIngredientPool = request.asIngredientPool;
                sp.FilterString = request.Name;
                sp.isDescending = request.isDescending;
                sp.MaxReqTime = request.RequiredTimeMax;
                sp.MinReqTime = request.RequiredTimeMin;
                sp.SortType = request.SortType;
                sp.Name = request.PatternName;
                db.SaveChanges();

                List<PatternFoodType> pf = await db.PatternFoodTypes.Where(x=>x.SearchPatternId == sp.Id).ToListAsync();
                if(pf == null) { pf = new List<PatternFoodType>(); }
                if(request.FoodTypeId == null) { request.FoodTypeId = new List<int>(); }
                foreach (var foodType in pf)
                {
                    if(request.FoodTypeId.Any(x=> x == foodType.FoodTypeId)) continue;
                    db.PatternFoodTypes.Remove(foodType);
                }
                foreach (var foodType in request.FoodTypeId)
                {
                    if (pf.Any(x => x.FoodTypeId == foodType)) continue;
                    db.PatternFoodTypes.Add(new PatternFoodType { FoodTypeId = foodType, SearchPatternId = sp.Id } );
                }
                await db.SaveChangesAsync();

                List<PatternDishType> pd = await db.PatternDishTypes.Where(x => x.SearchPatternId == sp.Id).ToListAsync();
                if (pd == null) { pd = new List<PatternDishType>(); }
                if (request.DishTypeId == null) { request.DishTypeId = new List<int>(); }
                foreach (var dishType in pd)
                {
                    if (request.DishTypeId.Any(x => x == dishType.DishTypeId)) continue;
                    db.PatternDishTypes.Remove(dishType);
                }
                foreach (var dishType in request.DishTypeId)
                {
                    if (pd.Any(x => x.DishTypeId == dishType)) continue;
                    db.PatternDishTypes.Add(new PatternDishType { DishTypeId = dishType, SearchPatternId = sp.Id });
                }
                await db.SaveChangesAsync();

                List<PatternMenuType> pm = await db.PatternMenuTypes.Where(x => x.SearchPatternId == sp.Id).ToListAsync();
                if (pm == null) { pm = new List<PatternMenuType>(); }
                if (request.MenuTypeId == null) { request.MenuTypeId = new List<int>(); }
                foreach (var menuType in pm)
                {
                    if (request.MenuTypeId.Any(x => x == menuType.MenuTypeId)) continue;
                    db.PatternMenuTypes.Remove(menuType);
                }
                foreach (var menuType in request.MenuTypeId)
                {
                    if (pm.Any(x => x.MenuTypeId == menuType)) continue;
                    db.PatternMenuTypes.Add(new PatternMenuType { MenuTypeId = menuType, SearchPatternId = sp.Id });
                }
                await db.SaveChangesAsync();

                List<PatternIngredientList> pi = await db.PatternIngredientLists.Where(x => x.SearchPatternId == sp.Id).ToListAsync();
                if (pi == null) { pi = new List<PatternIngredientList>(); }
                if (request.IngredientId == null) { request.IngredientId = new List<int>(); }
                foreach (var ingredientType in pi)
                {
                    if (request.IngredientId.Any(x => x == ingredientType.IngredientId)) continue;
                    db.PatternIngredientLists.Remove(ingredientType);
                }
                foreach (var ingredientType in request.IngredientId)
                {
                    if (pi.Any(x => x.IngredientId == ingredientType)) continue;
                    db.PatternIngredientLists.Add(new PatternIngredientList { IngredientId = ingredientType, SearchPatternId = sp.Id });
                }
                await db.SaveChangesAsync();
                return new PatternUpdateResponse { Id = sp.Id };
            }
            else
            {
                var newPattern = new SearchPattern
                {
                    DifficultyMax = request.DifficultyMax,
                    DifficultyMin = request.DifficultyMin,
                    asIngredientPool = request.asIngredientPool,
                    FilterString = request.Name,
                    isDescending = request.isDescending,
                    MaxReqTime = request.RequiredTimeMax,
                    MinReqTime = request.RequiredTimeMin,
                    SortType = request.SortType,
                    Name = request.PatternName,
                    UserId = user!.Id
                };
                db.SearchPatterns.Add(newPattern);

                await db.SaveChangesAsync();


                if(request.FoodTypeId != null)
                {
                    foreach(var item in  request.FoodTypeId)
                    {
                        db.PatternFoodTypes.Add(new PatternFoodType { FoodTypeId = item, SearchPatternId = newPattern.Id });
                    }
                }

                if (request.DishTypeId != null)
                {
                    foreach (var item in request.DishTypeId)
                    {
                        db.PatternDishTypes.Add(new PatternDishType { DishTypeId = item, SearchPatternId = newPattern.Id });
                    }
                }

                if (request.MenuTypeId != null)
                {
                    foreach (var item in request.MenuTypeId)
                    {
                        db.PatternMenuTypes.Add(new PatternMenuType { MenuTypeId = item, SearchPatternId = newPattern.Id });
                    }
                }
                await db.SaveChangesAsync();

                return new PatternUpdateResponse { Id = newPattern.Id };
            }
        }

        public async Task<ValidateResult> Validate(PatternUpdateRequest request)
        {
            return new ValidateResult();
        }
    }
}
