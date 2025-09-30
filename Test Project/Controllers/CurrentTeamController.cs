using Microsoft.AspNetCore.Mvc;
using StatFinder.Repos;

public class CurrentTeamController : Controller
{
    private readonly ICurrentTeamRepo _repo;

    public CurrentTeamController(ICurrentTeamRepo repo) => _repo = repo;

    [HttpPost]
    public async Task<IActionResult> Insert(int slot, string pokemonName)
    {
        await _repo.InsertAsync(slot, pokemonName);
        var team = await _repo.GetCurrentTeamAsync();
        return View("CurrentTeam", team);
    }

    [HttpPost]
    public async Task<IActionResult> Update(int slot, string pokemonName)
    {
        await _repo.UpdateAsync(slot, pokemonName);
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

