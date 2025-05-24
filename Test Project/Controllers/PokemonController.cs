using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Mvc;
using StatFinder.Models;
using System.Collections.Generic;

namespace StatFinder.Controllers
{
    public class PokemonController : Controller
    {
        public readonly Repos.IPokemonRepo _repo;

        public PokemonController(Repos.IPokemonRepo repo)
        {
            _repo = repo;
        }

        public IActionResult ReturnAllPokemon()
        {
            List<PokemonInfo> allPokemon = _repo.GetAllPokemonInfo();
            return View("ReturnAllPokemon", allPokemon);
        }

        // GET: Shows the form to enter a Pokemon name
        [HttpGet]
        public IActionResult EnterName()
        {
            // Return the form view with an empty PokemonInfo model (or you can use a ViewModel if preferred)
            return View(new PokemonInfo());
        }

        [HttpPost]
        public IActionResult EnterName(PokemonInfo model)
        {

            List<PokemonInfo> results = _repo.ReturnPokemonLike(model.Name);

            return View("SearchResults", results);
        }

    }
}
