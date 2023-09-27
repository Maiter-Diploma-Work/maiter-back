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

        [HttpGet("search")]
        public async Task<IActionResult> GetInterestsName(string searchQuery)
        {
            //выполняем запрос к БД, чтобы получить интересы, начинающиеся с searchQuery
            //var matchingInterests = _context.Interests
            //    .Where(i => i.Name.StartsWith(searchQuery))
            //    .Select(i => i.Name)
            //    .ToList();

            var matchingInterests = _context.Interests
                 .Where(i => i.Name.Contains(searchQuery))
                 .Select(i => i.Name)
                 .ToList();

            return Ok(matchingInterests);
        }
    }
}
