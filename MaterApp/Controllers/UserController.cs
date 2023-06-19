using MaterApp.Models;
using MaterApp.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MaterApp.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _context.Users.ToList();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var user = _context.Users.Find(id);

            if (user== null)
            {
                return NotFound("We have no user with such id!");
            }
            return Ok(user);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, EditUserDTO updatedUser)
        {
            var user = _context.Users.Include(u => u.UserInterests).FirstOrDefault(u => u.Id == id);
            if (user==null)
            {
                return NotFound("We have no user with such id!");
            }

            //Обновление полей пользователя
            user.Username = updatedUser.Username;
            user.Email = updatedUser.Email;
            user.FirstName = updatedUser.FirstName;
            user.LastName = updatedUser.LastName;
            user.DateOfBirth = updatedUser.DateOfBirth;
            user.Gender = updatedUser.Gender;
            user.Address = updatedUser.Address;
            user.ProfilePhoto = updatedUser.Photo;
            user.Phone = updatedUser.Phone;

            user.SetPassword(updatedUser.Password);

            //Обновление списка интересов пользователя
            if (updatedUser.Interests != null)
            {
                user.UserInterests.Clear(); // Очистка текущего списка интересов пользователя

                foreach (var interestId in updatedUser.Interests)
                {
                    var interest = _context.Interests.Find(interestId);

                    if (interest != null)
                    {
                        user.UserInterests.Add(new UserInterest { User = user, Interest = interest });
                    }
                }
            }

            _context.SaveChanges();
            return Ok(user);
        }

        
    }
}
