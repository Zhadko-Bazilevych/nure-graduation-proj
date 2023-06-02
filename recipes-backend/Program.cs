using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using recipes_backend.Models;
using recipes_backend.Services;
using recipes_backend.Operations.OAuth.AuthByCode;
using recipes_backend.Operations.OAuth.Refresh;
using Microsoft.Extensions.FileProviders;
using System.Text;
using recipes_backend.Operations.Recipe.RecipeInfo;
using recipes_backend.Operations.Recipe.Rate;
using recipes_backend.Operations.Recipe.changeFavorite;
using recipes_backend.Operations.Recipe.Filter;
using recipes_backend.Operations.Recipe.GetFilterData;
using recipes_backend.Operations.Recipe.FilterIngredient;
using recipes_backend.Operations.Recipe.PatternList;
using recipes_backend.Operations.Recipe.PatternUpdate;
using recipes_backend.Operations.Recipe.PatternDelete;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<recipesContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<GoogleOAuthService>();
builder.Services.AddScoped<ImageService>();


builder.Services.AddScoped<FilterOperation>();
builder.Services.AddScoped<GetFilterDataOperation>();
builder.Services.AddScoped<FilterIngredientOperation>();

builder.Services.AddScoped<RecipeInfoOperation>();
builder.Services.AddScoped<RateOperation>();
builder.Services.AddScoped<changeFavoriteOperation>();
builder.Services.AddScoped<AuthByCodeOperation>();
builder.Services.AddScoped<RefreshOperation>();

builder.Services.AddScoped<PatternListOperation>();
builder.Services.AddScoped<PatternUpdateOperation>();
builder.Services.AddScoped<PatternDeleteOperation>();

var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenKey"]));
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(y =>
    {
        y.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = key,
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
//builder.Services.AddScoped<TokenService>();


var app = builder.Build();

app.UseCors(policy =>
{
    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200");
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
           Path.Combine(builder.Environment.ContentRootPath, "Images")),
    RequestPath = "/Images"
});

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
