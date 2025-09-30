using Microsoft.AspNetCore.Mvc;
using StatFinder.Repos;
using StatFinder.Services;

public class CurrentTeamController : Controller
{
    private readonly ICurrentTeamRepo _repo;
    private readonly PokeApiService _pokeApiService;

    public CurrentTeamController(ICurrentTeamRepo repo, PokeApiService pokeApi)
    {
        _repo = repo;
        _pokeApiService = pokeApi;
    }

    [HttpGet]
    public async Task<IActionResult> CurrentTeam()
    {
        var team = await _repo.GetCurrentTeamAsync();

        // fill sprites for the ~6 team members
        await Task.WhenAll(team.Select(async m =>
        {
            if (!string.IsNullOrWhiteSpace(m.Name))
                m.ImageUrl = await _pokeApiService.GetPokemonImageUrlAsync(m.Name);
        }));

        return View("CurrentTeam", team);
    }


    [HttpPost]
    public async Task<IActionResult> Insert(int slot, string Name)
    {
        await _repo.InsertAsync(slot, Name);
        var team = await _repo.GetCurrentTeamAsync();
        return View("CurrentTeam", team);
    }

    [HttpPost]
    public async Task<IActionResult> Update(int slot, string Name)
    {
        await _repo.UpdateAsync(slot, Name);
        var team = await _repo.GetCurrentTeamAsync();
        return View("CurrentTeam", team);
    }

    [HttpPost]
    public async Task<IActionResult> Remove(int slot)
    {
        await _repo.RemoveAsync(slot);
        var team = await _repo.GetCurrentTeamAsync();
        return View("CurrentTeam", team);
    }
}


