﻿using MaterApp.Models.DTO;
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
    //[ApiController]
    //[Route("[controller]")]
    //public class AuthController : Controller
    //{
    //    private readonly IConfiguration _configuration;
    //    private readonly ApplicationDbContext _context;
    //    private readonly IHttpContextAccessor _httpContextAccessor;
    //    public AuthController(IConfiguration configuration, ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
    //    {
    //        _configuration = configuration;
    //        _context = context;
    //        _httpContextAccessor = httpContextAccessor;
    //    }


    //    [HttpPost("login")]
    //    public IActionResult Login(LoginUserDTO userToLogin)
    //    {
    //        if (!ModelState.IsValid)
    //        {
    //            return BadRequest(ModelState);
    //        }

    //        //Проверка учетных данных
    //        var user = _context.Users.FirstOrDefault(u => u.Email == userToLogin.Login);

    //        if (user == null || !user.VerifyPassword(userToLogin.Password))
    //        {
    //            return Unauthorized("Invalid email or password");
    //        }

    //        // Проверка заполненности профиля
    //        if (string.IsNullOrEmpty(user.FirstName) || string.IsNullOrEmpty(user.LastName))
    //        {
    //            return Unauthorized("Profile is not filled. Please complete your profile before logging in.");
    //        }

    //        // Создание списка утверждений (claims)
    //        //Утверждения в JWT-токене могут быть использованы для идентификации пользователя и предоставления доступа к определенным ресурсам или функциональности в приложении.
    //        var claims = new[]
    //       {
    //            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
    //            new Claim(ClaimTypes.Email, user.Email),
                
    //            // Другие утверждения по желанию
    //        };

    //        var issuer = _configuration["AuthOptions:Issuer"];
    //        var audience = _configuration["AuthOptions:Audience"];
    //        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthOptions:Key"]));

    //        // Создание JWT-токена

    //        var jwt = new JwtSecurityToken(
    //            issuer: issuer,
    //            audience: audience,
    //            claims: claims,
    //            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)), // Измените срок действия токена по вашему усмотрению
    //            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

    //        var token = new JwtSecurityTokenHandler().WriteToken(jwt);

    //        var response = new
    //        {
    //            access_token = token,
    //            username = userToLogin.Login
    //        };

    //        return Json(response);
    //    }


    //    [Authorize]
    //    [Route("data")]
    //    public IActionResult Data()
    //    {
    //        return Json("Получили данные)))"); 
    //    }



    //}
}
