using ApiIntro.Data;
using ApiIntro.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiIntro.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _context.Categories.ToListAsync());
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Category category)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Create),category);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBydId([FromRoute] int id)
        {
            var existData = await _context.Categories.FindAsync(id);
            if (existData is null) return NotFound();
            return Ok(existData);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int? id)
        {
            if (id is null) return BadRequest();
            var existData = await _context.Categories.FindAsync(id);
            if (existData is null) return NotFound();
            _context.Categories.Remove(existData);
            await _context.SaveChangesAsync();
            return Ok();
        }

    }
}
