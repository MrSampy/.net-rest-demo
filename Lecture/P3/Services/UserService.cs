using P3.Models;
using P3.Interfaces;
namespace P3.Services
{
    public class UserService : IUserService
    {
        public static List<User> Users;

        public UserService()
        {
            Users = new List<User>
            {
                new() { Id = 1, Name = "John" },
                new() { Id = 2, Name = "Alice" }
            };
        }

        public void Delete(int id)
        {
            Users.RemoveAll(x => x.Id == id);
        }

        public List<User> GetUsers()
        {
            return Users;
        }

        public void Update(User user)
        {
            var userToUpdate = Users.First(x => x.Id == user.Id);
            userToUpdate.Name = user.Name;
        }
    }
}
