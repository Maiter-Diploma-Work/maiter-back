using MaterApp.Models;
using MaterApp.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace MaterApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegistrationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;

        public RegistrationController(IConfiguration configuration, ApplicationDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterUserDTO model)
        {
            // Проверка входных данных модели

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Проверка, существует ли пользователь с заданным email
            var existingUser = _context.Users.FirstOrDefault(u => u.Email == model.Email);
            if (existingUser != null)
            {
                //return Conflict("User with this email already exists");
                return BadRequest(ModelState);
            }

            // Проверка совпадения пароля и подтверждения пароля
            if (model.Password != model.ConfirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "Password and confirm password do not match");
                return BadRequest(ModelState);
            }


            // Создание нового пользователя

            var user = new User
            {
                Username = model.Username,
                Email = model.Email,
                //FirstName = model.FirstName,
                //LastName = model.LastName,
                //DateOfBirth = model.DateOfBirth,
                //Gender = model.Gender,
                //Address = model.Address
            };

            // Установка пароля пользователя

            user.SetPassword(model.Password);


            // Сохранение пользователя в базе данных

            _context.Users.Add(user);
            _context.SaveChanges();

            // Возвращаем успешный результат

            return Ok("User registered successfully");
        }
    }

}
