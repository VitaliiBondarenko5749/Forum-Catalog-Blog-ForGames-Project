using Catalog_of_Games_BAL.Contracts;
using Catalog_of_Games_BAL.Services;
using Catalog_of_Games_DAL.Data;
using Catalog_of_Games_DAL.Repositories;
using Catalog_of_Games_DAL.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

OpenApiContact contact = new()
{
    Name = "Vitalii Bondarenko",
    Email = "bondarenko.vitalii@chnu.edu.ua"
};

OpenApiInfo info = new()
{
    Version = "v1",
    Title = "Web API for Forum",
    Contact = contact
};

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", info);
});

builder.Services.AddDbContext<CatalogOfGamesContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MSSQLConnection"));
});

// UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Services(DAL)
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IGameRepository, GameRepository>();
builder.Services.AddScoped<IDeveloperRepository, DeveloperRepository>();
builder.Services.AddScoped<ILanguageRepository, LanguageRepository>();
builder.Services.AddScoped<IPlatformRepository, PlatformRepository>();
builder.Services.AddScoped<IPublisherRepository, PublisherRepository>();
builder.Services.AddScoped<IGamePlatformRepository, GamePlatformRepository>();
builder.Services.AddScoped<IGameLanguageRepository, GameLanguageRepository>();
builder.Services.AddScoped<IGameDeveloperRepository, GameDeveloperRepository>();
builder.Services.AddScoped<IGameCommentRepository, GameCommentRepository>();

// Services(BAL)
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IDeveloperService, DeveloperService>();
builder.Services.AddScoped<ILanguageService, LanguageService>();
builder.Services.AddScoped<IPlatformService, PlatformService>();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();