using MinimalAPIs.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IUserRepository, UserRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

#region API's
app.MapGet("/user/{id}", async (int id, IUserRepository userRepository, CancellationToken cancellationToken) =>
{
    await Task.Delay(5000, cancellationToken);
    return await userRepository.GetUserAsync(id, cancellationToken);
    //if (user is null) return Results.BadRequest();

    //return Results.Ok(user);
}).WithName("GetUsers");

app.MapPut("/user/update/{id}", (int id, User user, IUserRepository userRepository) =>
{
    return userRepository.UpdateUser(id, user);
}).WithName("UpdateUsers");

app.MapDelete("/user/{id}", (int id, IUserRepository userRepository) =>
{
    userRepository.DeleteUser(id);
    return Results.Ok();
}).WithName("DeleteUser");
#endregion

app.Run();