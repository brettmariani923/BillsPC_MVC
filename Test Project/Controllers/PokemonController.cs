using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Mvc;
using StatFinder.Models;
using System.Collections.Generic;
using StatFinder.Services;
using Microsoft.Extensions.Caching.Memory;

namespace StatFinder.Controllers
{
    public class PokemonController : Controller
    {
        public readonly Repos.IPokemonRepo _repo;
        public readonly PokeApiService _pokeApiService;

        public PokemonController(Repos.IPokemonRepo repo, PokeApiService pokeApiService)
        {
            _repo = repo;
            _pokeApiService = pokeApiService;
        }

        public async Task<IActionResult> ReturnAllPokemon()
        {
            List<PokemonInfo> allPokemon = _repo.GetAllPokemonInfo();

            var semaphore = new SemaphoreSlim(50); 

            var tasks = allPokemon.Select(async p =>
            {
                await semaphore.WaitAsync();
                try
                {
                    p.ImageUrl = await _pokeApiService.GetPokemonImageUrlAsync(p.Name);
                }
                finally
                {
                    semaphore.Release();
                }
            }).ToList();

            await Task.WhenAll(tasks);

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
            // Get all matching Pokémon from database
            List<PokemonInfo> results = _repo.ReturnPokemonLike(model.Name);

            // Parallelize image fetching from the API
            var tasks = results.Select(async pokemon =>
            {
                pokemon.ImageUrl = await _pokeApiService.GetPokemonImageUrlAsync(pokemon.Name);
            }).ToList();

            await Task.WhenAll(tasks);

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
