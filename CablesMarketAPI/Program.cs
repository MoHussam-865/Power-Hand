using MyDatabase;
using MyDatabase.Repository.Clients;
using MyDatabase.Repository.Emploee;
using MyDatabase.Repository.Invoices;
using MyDatabase.Repository.Items;
using MyDatabase.Repository.Posts;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    //options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// injection
builder.Services.AddDbContext<DatabaseContext>();
builder.Services.AddScoped<IApiItemsRepo, ApiItemsRepoImpl>();
builder.Services.AddScoped<IPostsRepo, PostsRepoImpl>();




var app = builder.Build();

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
