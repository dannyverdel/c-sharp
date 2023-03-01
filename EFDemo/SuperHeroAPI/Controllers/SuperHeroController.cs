using Microsoft.AspNetCore.Mvc;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly ISuperHeroService _superhero_service;

        public SuperHeroController(ISuperHeroService superhero_service) {
            _superhero_service = superhero_service;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> GetAllHeroes() => Ok(await _superhero_service.GetAllHeroes());

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> GetSingleHero(int id) {
            SuperHero? result = await _superhero_service.GetSingleHero(id);
            if ( result is null )
                return NotFound("Sorry, hero was not found.");

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHeroDto hero) => Ok(await _superhero_service.AddHero(hero));

        [HttpPut("{id}")]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(int id, SuperHeroDto request) {
            List<SuperHero>? result = await _superhero_service.UpdateHero(id, request);
            if ( result is null )
                return NotFound("Sorry, hero was not found.");

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> DeleteHero(int id) {
            List<SuperHero>? result = await _superhero_service.DeleteHero(id);
            if ( result is null )
                return NotFound("Sorry, hero was not found.");

            return Ok(result);
        }
    }
}
