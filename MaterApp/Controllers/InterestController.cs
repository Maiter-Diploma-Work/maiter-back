using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MaterApp.Controllers
{
    [ApiController]
    [Route("interests")]
    public class InterestController : Controller
    {
        private readonly ApplicationDbContext _context;
        public InterestController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("getAllInterests")]
        public async Task<IActionResult> GetAllInterests()
        {
            var interests = await _context.Interests.ToListAsync();
            return Ok(interests);
        }
    }
}
