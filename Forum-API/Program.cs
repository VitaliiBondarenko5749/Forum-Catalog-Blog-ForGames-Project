using Forum_BAL.Contracts;
using Forum_BAL.Services;
using Forum_DAL.Contracts;
using Forum_DAL.Repositories;
using Microsoft.OpenApi.Models;
using System.Data;
using System.Data.SqlClient;

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

builder.Services.AddScoped(sqlConnection => new SqlConnection(builder.Configuration.GetConnectionString("MSSQLConnection")));
builder.Services.AddScoped<IDbTransaction>(sqlConnection =>
{
    SqlConnection connection = sqlConnection.GetRequiredService<SqlConnection>();

    connection.Open();

    return connection.BeginTransaction();
});

// UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Services(DAL)
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<IGameRepository, GameRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<ILikedCommentRepository, LikedCommentRepository>();
builder.Services.AddScoped<ILikedReplyRepository, LikedReplyRepository>();
builder.Services.AddScoped<IReplyRepository, ReplyRepository>();
builder.Services.AddScoped<IPostGameRepository, PostGameRepository>();
builder.Services.AddScoped<IPostCommentRepository, PostCommentRepository>();
builder.Services.AddScoped<ICommentReplyRepository, CommentReplyRepository>();

// Services(BAL)
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IReplyService, ReplyService>();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();