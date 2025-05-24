using Project.Models;

namespace Project.Repos
{
    public interface IPokemonRepo
    {
        Task<List<PokemonInfo>> GetAllPokemons();
        Task<PokemonInfo> GetPokemonById(int id);
        Task<PokemonInfo> AddPokemon(PokemonInfo pokemon);
        Task<PokemonInfo> UpdatePokemon(PokemonInfo pokemon);
        Task<bool> DeletePokemon(int id);
    }
}