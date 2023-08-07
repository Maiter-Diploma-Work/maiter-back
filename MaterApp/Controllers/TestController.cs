using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MaterApp.Controllers
{
    [ApiController]
    [Route("testcontroller")]
    public class TestController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserService _userService;

        public TestController(ApplicationDbContext context)
        {
            _context = context;

        }

        [HttpGet("testMethod")]
        public async Task<IActionResult> TestMethod5()
        {
            return Ok("OK");
        }

        //[HttpGet("getUsers")]
        //public async Task<IActionResult> GetUsers()
        //{        
        //    var users = await _context.Users.ToListAsync();
        //    return Ok(users);
        //}
    }
}
