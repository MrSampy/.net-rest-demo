using Microsoft.AspNetCore.Mvc;
using P3.Interfaces;
using P3.Models;

namespace P3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService UserService)
        {
            _userService = UserService;
        }

        // GET: api/users
        [HttpGet]
        public IActionResult GetUsers()
        {
            return Ok(_userService.GetUsers());
        }

        // GET: api/users/{id}
        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var user = _userService.GetUsers().FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // POST: api/users
        [HttpPost]
        public IActionResult CreateUser([FromBody]User user)
        {
            _userService.GetUsers().Add(user);
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        // PUT: api/users/
        [HttpPut]
        public IActionResult UpdateUser([FromBody] User user)
        {
            var existingUser = _userService.GetUsers().FirstOrDefault(u => u.Id == user.Id);
            if (existingUser == null)
            {
                return NotFound();
            }

            _userService.Update(user); // Update the user's name
            return Ok();
        }

        // Delete: api/users/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = _userService.GetUsers().FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            _userService.Delete(id);
            return Ok();
        }
    }
}
