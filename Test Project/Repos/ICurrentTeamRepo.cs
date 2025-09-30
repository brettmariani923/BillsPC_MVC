using StatFinder.Models;
using Microsoft.AspNetCore.Mvc;

namespace StatFinder.Repos
{
    public interface ICurrentTeamRepo
    {
        public IActionResult Insert(int slot, string pokemonName);

        public IActionResult Update(int slot, string pokemonName);

        public IActionResult Remove(int slot);

        public List<PokemonInfo> GetAllPokemonInfo();
    }
}
