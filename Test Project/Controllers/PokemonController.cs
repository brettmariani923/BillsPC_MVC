using Microsoft.AspNetCore.Mvc;
using StatFinder.Models;
using StatFinder.Repos;
using StatFinder.Services;

namespace StatFinder.Controllers
{
    public class PokemonController : Controller
    {
        public readonly IPokemonRepo _repo;
        public readonly PokeApiService _pokeApiService;

        public PokemonController(IPokemonRepo repo, PokeApiService pokeApiService)
        {
            _repo = repo;
            _pokeApiService = pokeApiService;
        }

        public async Task<IActionResult> ReturnAllPokemon()
        {
            List<PokemonInfo> allPokemon = _repo.GetAllPokemonInfo();

            await Task.WhenAll(allPokemon.Select(async p =>
               p.ImageUrl = await _pokeApiService.GetPokemonImageUrlAsync(p.Name)));

            return View("ReturnAllPokemon", allPokemon);
        }

        [HttpGet]
        public IActionResult EnterName()
        {
            return View(new PokemonInfo());
        }

        [HttpPost]
        public async Task<IActionResult> EnterName(PokemonInfo model)
        {
            // Get matching Pokémon from database
            List<PokemonInfo> results = _repo.ReturnPokemonLike(model.Name);

            // image fetching from the API
            await Task.WhenAll(results.Select(async p =>
                p.ImageUrl = await _pokeApiService.GetPokemonImageUrlAsync(p.Name)));

            // Return results to the view
            return View("SearchResults", results);
        }

        public IActionResult About()
        {
            ViewBag.Title = "About This Pokémon Website";
            return View();
        }

    }
}
