using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHeroAPI.Data;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly DataContext _context;
        
        public SuperHeroController(DataContext context)
        {
            this._context = context;
        }
        
        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> GetSuperHeroes()
        {
            return Ok(await _context.SuperHeroes.ToListAsync());
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> GetSuperHero(int id)
        {
            return Ok(await _context.SuperHeroes.FindAsync(id));
        }
        
        [HttpPost]
        public async Task<ActionResult<SuperHero>> CreateSuperHero(SuperHero superHero)
        {
            _context.SuperHeroes.Add(superHero);
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult<SuperHero>> UpdateSuperHero(SuperHero superHero)
        {
            var superHeroFromDb = await _context.SuperHeroes.FindAsync(superHero.Id);
            
            if (superHeroFromDb == null)
            {
                return NotFound();
            }
            
            superHeroFromDb.Name = superHero.Name;
            superHeroFromDb.FirstName = superHero.FirstName;
            superHeroFromDb.LastName = superHero.LastName;
            superHeroFromDb.Place = superHero.Place;
            await _context.SaveChangesAsync();
            
            return Ok(await _context.SuperHeroes.ToListAsync());
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult<SuperHero>> DeleteSuperHero(int id)
        {
            var superHeroFromDb = await _context.SuperHeroes.FindAsync(id);
            
            if (superHeroFromDb == null)
            {
                return NotFound();
            }
            
            _context.SuperHeroes.Remove(superHeroFromDb);
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());
        }
    }
}
