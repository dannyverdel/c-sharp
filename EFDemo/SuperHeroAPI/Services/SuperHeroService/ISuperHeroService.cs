namespace SuperHeroAPI.Services.SuperHeroService
{
    public interface ISuperHeroService
    {
        Task<List<SuperHero>> AddHero(SuperHeroDto hero);
        Task<List<SuperHero>?> DeleteHero(int id);
        Task<List<SuperHero>> GetAllHeroes();
        Task<SuperHero?> GetSingleHero(int id);
        Task<List<SuperHero>?> UpdateHero(int id, SuperHeroDto request);
    }
}