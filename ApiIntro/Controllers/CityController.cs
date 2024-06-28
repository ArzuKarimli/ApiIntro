using ApiIntro.Data;
using ApiIntro.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiIntro.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CityController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _context.Cities.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] City city)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _context.Cities.AddAsync(city);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Create), city);

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var existData=await _context.Cities.FindAsync(id);
            if (existData is null) return NotFound();
            return Ok(existData);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int? id)
        {
            if (id == null) return BadRequest();
            var existData = await _context.Cities.FindAsync(id);
            if (existData is null) return NotFound();
             _context.Cities.Remove(existData);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id , [FromBody] City city)
        {
            var existCity = await _context.Cities.FindAsync(id);
            if (existCity is null) return NotFound();
            existCity.Name = city.Name;
            existCity.CountryId = city.CountryId;
            await _context.SaveChangesAsync();
            return Ok();
        }

    }
}
