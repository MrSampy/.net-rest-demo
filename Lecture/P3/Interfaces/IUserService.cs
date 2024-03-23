using P3.Models;

namespace P3.Interfaces
{
    public interface IUserService
    {
        public List<User> GetUsers();
        public void Update(User user);
        public void  Delete(int id);
    }
}
