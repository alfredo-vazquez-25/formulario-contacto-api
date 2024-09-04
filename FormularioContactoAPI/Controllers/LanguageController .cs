using FormularioContactoAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FormularioContactoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguageController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LanguageController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{languageCode}")]
        public async Task<IActionResult> GetTexts(string languageCode)
        {
            var texts = await _context.LanguageTexts
                .Where(t => t.LanguageCode == languageCode)
                .ToListAsync();

            var result = texts.ToDictionary(t => t.Key, t => t.Text);

            return Ok(result);
        }
    }
}
