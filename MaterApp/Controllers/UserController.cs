using MaterApp.Models;
using MaterApp.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;

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


        //[HttpGet("withInterests")]
        //public ActionResult<List<int>> GetAllUsersWithInterests()
        //{
        //    var interestIds = _context.Users
        //         .Include(u => u.UserInterests)
        //         .ThenInclude(ui => ui.Interest)
        //         .SelectMany(u => u.UserInterests.Select(ui => ui.Interest.Id))
        //         .ToList();

        //    return interestIds;
        //}

        [HttpGet("withInterests")]
        public ActionResult<List<EditUserDTO>> GetAllUsersWithInterests()
        {
            var usersWithInterests = _context.Users
                .Include(u => u.UserInterests)
                .ThenInclude(ui => ui.Interest)
                .Select(u => new EditUserDTO
                {
                    Username = u.Username,
                    Password = u.PasswordHash,
                    Email = u.Email,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    DateOfBirth = u.DateOfBirth,
                    Gender = u.Gender,
                    Address = u.Address,
                    Phone = u.Phone,
                    Photo = u.ProfilePhoto,
                    Interests = u.UserInterests.Select(ui => ui.InterestId).ToList()
                })
                .ToList();

            return usersWithInterests;
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
