using Microsoft.AspNetCore.Mvc;
using StatFinder.Models;
using StatFinder.Repos;
using StatFinder.Services;
public class CurrentTeamController : Controller
{
    public readonly ICurrentTeamRepo _repo;
    public readonly PokeApiService _pokeApiService;

    public CurrentTeamController(IPokemonRepo repo, PokeApiService pokeApiService)
    {
        _repo = repo;
        _pokeApiService = pokeApiService;
    }

    [HttpPost]
    public IActionResult Insert(int slot, string pokemonName)
    {
        _repo.InsertPokemonInSlot(slot, pokemonName);
        return View("CurrentTeam", _repo.GetCurrentTeam());
    }

    [HttpPost]
    public IActionResult Update(int slot, string pokemonName)
    {
        _repo.UpdatePokemonInSlot(slot, pokemonName);
        return View("CurrentTeam", _repo.GetCurrentTeam());
    }

    [HttpPost]
    public IActionResult Remove(int slot)
    {
        _repo.RemoveFromSlot(slot);
        return View("CurrentTeam", _repo.GetCurrentTeam());
    }

}
