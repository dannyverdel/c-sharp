namespace SuperHeroAPI.Services.SuperHeroService;

public class SuperHeroService : ISuperHeroService
{
    private readonly DataContext _context;

    public SuperHeroService(DataContext context) {
        _context = context;
    }

    public async Task<List<SuperHero>> GetAllHeroes() {
        return await _context.SuperHeroes.ToListAsync();
    }

    public async Task<SuperHero?> GetSingleHero(int id) {
        SuperHero? hero = await _context.SuperHeroes.FindAsync(id);
        if ( hero is null )
            return null;

        return hero;
    }

    public async Task<List<SuperHero>> AddHero(SuperHeroDto hero) {
        _context.SuperHeroes.Add(new SuperHero { Name = hero.Name, FirstName = hero.FirstName, LastName = hero.LastName, Place = hero.Place, Comic = hero.Comic });
        await _context.SaveChangesAsync();
        return await GetAllHeroes();
    }

    public async Task<List<SuperHero>?> UpdateHero(int id, SuperHeroDto request) {
        SuperHero? hero = await _context.SuperHeroes.FindAsync(id);
        if ( hero is null )
            return null;

        hero.FirstName = request.FirstName;
        hero.LastName = request.LastName;
        hero.Name = request.Name;
        hero.Place = request.Place;
        hero.Comic = request.Comic;

        await _context.SaveChangesAsync();

        return await GetAllHeroes();
    }

    public async Task<List<SuperHero>?> DeleteHero(int id) {
        SuperHero? hero = _context.SuperHeroes.Find(id);
        if ( hero is null )
            return null;

        _context.SuperHeroes.Remove(hero);

        await _context.SaveChangesAsync();

        return await GetAllHeroes();
    }
}
