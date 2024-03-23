using Microsoft.AspNetCore.Mvc;
using P1.Models;

namespace P1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly List<User> _users = new List<User>() { new User { Id = 1, Name = "Name1", Email = "Email1" } }; // Приклад збереження користувачів у списку. У реальному застосунку використовуйте базу даних.

        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            return _users;
        }

        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }

        [HttpPost]
        public ActionResult<User> Post(User user)
        {
            _users.Add(user);
            return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, User user)
        {
            var existingUser = _users.FirstOrDefault(u => u.Id == id);
            if (existingUser == null)
            {
                return NotFound();
            }
            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
            // Оновлення інших властивостей користувача за потребою
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            _users.Remove(user);
            return NoContent();
        }
    }
}
