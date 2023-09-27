using MaterApp.Models;
using MaterApp.Models.DTO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MaterApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegistrationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;

        private readonly IHttpContextAccessor _httpContextAccessor;
        public RegistrationController(IConfiguration configuration, ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
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
            };

            // Установка пароля пользователя

            user.SetPassword(model.Password);

            // Сохранение пользователя в базе данных

            _context.Users.Add(user);
            _context.SaveChanges();

            var claims = new[]
           {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),              
            };

            var issuer = _configuration["AuthOptions:Issuer"];
            var audience = _configuration["AuthOptions:Audience"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthOptions:Key"]));

            // Создание JWT-токена

            var jwt = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)), // Измените срок действия токена по вашему усмотрению
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = token,
                username = user.Username
            };

            return Ok(response);
        }


    }


}
