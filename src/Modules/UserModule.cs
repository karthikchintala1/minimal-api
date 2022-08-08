using Carter;
using MinimalAPIs.Repositories;

namespace MinimalAPIs.Modules
{
    public class UserModule : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/user/{id}", GetUserById).WithName("GetUsers");

            app.MapPut("/user/update/{id}", UpdateUser).WithName("UpdateUsers");

            app.MapDelete("/user/{id}", DeleteUser).WithName("DeleteUser");
        }

        private async Task<IResult> GetUserById(int id, IUserRepository userRepository, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetUserAsync(id, cancellationToken);
            if (user is null) return Results.BadRequest();

            return Results.Ok(user);
        }

        private bool UpdateUser(int id, User user, IUserRepository userRepository)
        {
            return userRepository.UpdateUser(id, user);
        }

        private IResult DeleteUser(int id, IUserRepository userRepository)
        {
            userRepository.DeleteUser(id);
            return Results.Ok();
        }

    }
}
