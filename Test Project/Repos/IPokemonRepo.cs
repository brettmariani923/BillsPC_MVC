using StatFinder.Models;

namespace StatFinder.Repos
{
    public interface IPokemonRepo
    {
        public List<PokemonInfo> GetAllPokemonInfo();
        public List<PokemonInfo> ReturnPokemonLike(string name, int limit = 25);
    }
}