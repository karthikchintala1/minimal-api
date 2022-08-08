using Bogus;

namespace MinimalAPIs.Repositories
{
    public class User
    {
        public int Id { get; set; }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    }

    public interface IUserRepository
    {
        User GetUser(int id);
        Task<User> GetUserAsync(int id, CancellationToken cancellationToken);
        bool UpdateUser(int id, User user);
        void DeleteUser(int id);
    }

    public class UserRepository : IUserRepository
    {
        private List<User> _users = new List<User>();

        public UserRepository()
        {
            GenerateSeedData();
        }

        public User GetUser(int id)
        {
            return _users.FirstOrDefault(u => u.Id == id);
        }

        public Task<User> GetUserAsync(int id, CancellationToken cancellationToken)
        {
            return Task.FromResult(GetUser(id));
            //Task.Run(() => GetUser(id)), cancellationToken);
            //return Task.FromResult(GetUser(id));
        }

        public bool UpdateUser(int id, User user)
        {
            var dbUser = _users.FirstOrDefault(u => u.Id == id);
            if (dbUser is null) return false;

            dbUser.UserName = user.UserName;
            dbUser.FirstName = user.FirstName;
            dbUser.LastName = user.LastName;

            return true;
        }

        public void DeleteUser(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user is null) return;

            _users.Remove(user);
        }

        private void GenerateSeedData()
        {
            _users = new Faker<User>()
                .StrictMode(true)
                .RuleFor(u => u.Id, f => f.IndexFaker)
                .RuleFor(u => u.UserName, f => f.Person.UserName)
                .RuleFor(u => u.FirstName, f => f.Person.FirstName)
                .RuleFor(u => u.LastName, f => f.Person.LastName)
                .Generate(50);
        }
    }
}
