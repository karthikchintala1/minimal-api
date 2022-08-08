using MinimalAPIs.Repositories;
using Carter;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IProductsRepository, ProductsRepository>();

builder.Services.AddCarter();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapCarter();

#region API's

//// USER operations
//app.MapGet("/user/{id}", async (int id, IUserRepository userRepository, CancellationToken cancellationToken) =>
//{
//    var user = await userRepository.GetUserAsync(id, cancellationToken);
//    if (user is null) return Results.BadRequest();

//    return Results.Ok(user);
//}).WithName("GetUsers");

//app.MapPut("/user/update/{id}", (int id, User user, IUserRepository userRepository) =>
//{
//    return userRepository.UpdateUser(id, user);
//}).WithName("UpdateUsers");

//app.MapDelete("/user/{id}", (int id, IUserRepository userRepository) =>
//{
//    userRepository.DeleteUser(id);
//    return Results.Ok();
//}).WithName("DeleteUser");

//// PRODUCTS operations
//app.MapPost("/product/create", (Product product, IProductsRepository productsRepo) =>
//{
//    if (!product.IsValid()) return Results.BadRequest();
//    productsRepo.CreateProduct(product);
//    return Results.StatusCode(201);
//});

//app.MapGet("/product/{id}", (int id, IProductsRepository repo) =>
//{
//    if (id <= 0) return Results.BadRequest();

//    return Results.Ok(repo.GetProductById(id));
//});

//app.MapPut("/product/update/{id}", (int id, Product product, IProductsRepository productRepo) =>
//{
//    if (!product.IsValid()) return Results.BadRequest();

//    productRepo.UpdateProduct(id, product);
//    return Results.Ok();
//});

//app.MapDelete("/product/delete/{id}", (int id, IProductsRepository productsRepo) =>
//{
//    return productsRepo.DeleteProduct(id);
//});
#endregion

app.Run();