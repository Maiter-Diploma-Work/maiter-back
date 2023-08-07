using MaterApp.Models;
using MaterApp.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace MaterApp.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserService _userService;

        public UserController(ApplicationDbContext context, UserService userService)
        {
            _context = context;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            //Not need
            //var userService = HttpContext.RequestServices.GetRequiredService<UserService>();
            // await userService.SyncUsersWithElasticsearch();



            var users = await _context.Users.ToListAsync();
            return Ok(users);

            //var users = await _context.Users.Include(u => u.BlockedUsers).ToListAsync();
           // return Ok(users);
        }


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

            if (user == null)
            {
                return NotFound("We have no user with such id!");
            }
            return Ok(user);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, EditUserDTO updatedUser)
        {
            var user = _context.Users.Include(u => u.UserInterests).FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound("We have no user with such id!");
            }

            // Обновление полей пользователя
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

            // Обновление списка интересов пользователя
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

        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> SearchUsers(string partialName)
        {
            var users = await _userService.PartialSearchByName(partialName);

            return Ok(users); // Возвращаем найденных пользователей в виде HTTP-ответа
        }


        //NEED TEST
        [HttpPost("{userId}/block/{blockedUserId}")]
        public IActionResult BlockUser(int userId, int blockedUserId)
        {
            var user = _context.Users.Include(u => u.BlockedUsers).FirstOrDefault(u => u.Id == userId);
            var blockedUser = _context.Users.FirstOrDefault(u => u.Id == blockedUserId);

            if (user == null || blockedUser == null)
            {
                return NotFound();
            }

            // Проверяем, что пользователь еще не заблокирован
            if (!user.BlockedUsers.Any(bu => bu.BlockedUserId == blockedUserId))
            {
                // Не указываем значение для столбца Id
                user.BlockedUsers.Add(new BlockedUsers { BlockedUserId = blockedUserId });
                _context.SaveChanges();
            }

            return NoContent();
        }






        //NEED TEST
        // Добавление лайка от одного пользователя к другому
        [HttpPost("{likerId}/like/{likeeId}")]
        public IActionResult LikeUser(int likerId, int likeeId)
        {
            var liker = _context.Users.Find(likerId);
            var likee = _context.Users.Find(likeeId);

            if (liker == null || likee == null)
            {
                return NotFound("One or both of the users not found.");
            }

            // Проверяем, что пользователь еще не лайкнул данного пользователя
            if (!_context.Likes.Any(l => l.LikerId == likerId && l.LikeeId == likeeId))
            {
                // Создаем новый объект Like и добавляем его в контекст базы данных
                var like = new Like
                {
                    LikerId = likerId,
                    LikeeId = likeeId
                };

                _context.Likes.Add(like);
                _context.SaveChanges();

                return Ok("Successfully liked the user.");
            }

            return BadRequest("The user has already been liked.");
        }


    }
}
