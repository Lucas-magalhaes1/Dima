using System.Transactions;
using Dima.Api.Data;
using Dima.Api.Handlers;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment()) 
{
    builder.Configuration.AddUserSecrets<Program>();
}

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString)
);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.CustomSchemaIds(n=>n.FullName);
});
builder.Services.AddTransient<ICategoryHandler, CategoryHandler>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Dima API V1");
    c.RoutePrefix = string.Empty; 
});



app.MapPost(
    "/v1/categories", async (CreateCategoryRequest request, ICategoryHandler handler) =>await handler.CreateAsync(request))
    .WithName("Create categories")
    .WithSummary("Cria uma nova categoria")
    .Produces<Response<Category?>>(); 

app.MapPut(
        "/v1/categories{Id}",
        async (long id,
            UpdateCategoryRequest request, ICategoryHandler handler) => 
        {
            request.Id = id;
            return await handler.UpdateAsync(request);
        })
    .WithName("Update categories")
    .WithSummary("Atuliza uma categoria")
    .Produces<Response<Category?>>(); 

app.MapDelete(
        "/v1/categories{Id}",
        async (long id,
             ICategoryHandler handler) =>
        {
            var request = new DeleteCategoryRequest
            {
                Id = id,
                UserId = "Test@Lucas"
            };
            return await handler.DeleteAsync(request);
        })
    .WithName("Delete categories")
    .WithSummary("Deleta uma categoria")
    .Produces<Response<Category?>>(); 

app.MapGet(
        "/v1/categories",
        async (
            ICategoryHandler handler) =>
        {
            var request = new GetAllCategoriesRequest()
            {
                UserId = "Test@Lucas"
            };
            return await handler.GetAllAsync(request);
        })
    .WithName("Get by all categories")
    .WithSummary("Retorna todas as  categoria")
    .Produces<PagedResponse<List<Category>?>>(); 

app.MapGet(
        "/v1/categories{Id}",
        async (long id,
            ICategoryHandler handler) =>
        {
            var request = new GetCategoryByIdRequest
            {
                Id = id,
                UserId = "Test@Lucas"
            };
            return await handler.GetByIdAsync(request);
        })
    .WithName("Get by id categories")
    .WithSummary("Retorna uma categoria")
    .Produces<Response<Category?>>(); 

app.Run();



