using Dapper;
using System.Data;
using StatFinder.Models;
using System.Threading.Tasks;

namespace StatFinder.Repos
{
    public class CurrentTeamRepo : ICurrentTeamRepo
    {
        private readonly IDbConnection _db;
        public CurrentTeamRepo(IDbConnection db)
        {
            _db = db;
        }

        public async Task InsertAsync(int slot, string pokemonName)
        {
            const string sql = "INSERT INTO CurrentTeam (Slot, PokemonName) VALUES (@Slot, @PokemonName);";
            await _db.ExecuteAsync(sql, new { Slot = slot, PokemonName = pokemonName });
        }

        public async Task UpdateAsync(int slot, string pokemonName)
        {
            const string sql = "UPDATE CurrentTeam SET PokemonName = @PokemonName WHERE Slot = @Slot;";
            await _db.ExecuteAsync(sql, new { Slot = slot, PokemonName = pokemonName });
        }

        public async Task RemoveAsync(int slot)
        {
            const string sql = "DELETE FROM CurrentTeam WHERE Slot = @Slot;";
            await _db.ExecuteAsync(sql, new { Slot = slot });
        }

        public async Task<List<CurrentTeamInfo>> GetCurrentTeamAsync()
        {
            const string sql = "SELECT Slot, PokemonName FROM CurrentTeam ORDER BY Slot;";
            var team = await _db.QueryAsync<CurrentTeamInfo>(sql);
            return team.ToList();
        }
    }
}
