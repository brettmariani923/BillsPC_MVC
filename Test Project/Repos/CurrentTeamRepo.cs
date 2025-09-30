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

        public async Task InsertAsync(int slot, string name)
        {
            const string sql = @"
            INSERT INTO dbo.CurrentTeam (Slot, PokemonID)
            SELECT @Slot, p.PokemonID
            FROM dbo.Pokemon p
            WHERE p.Name = @Name;";

            var rows = await _db.ExecuteAsync(sql, new { Slot = slot, Name = name });
            if (rows == 0) throw new InvalidOperationException($"No Pokémon found named '{name}'.");
        }

        public async Task UpdateAsync(int slot, string name)
        {
            const string sql = @"
            UPDATE ct
            SET ct.PokemonID = p.PokemonID
            FROM dbo.CurrentTeam ct
            JOIN dbo.Pokemon p ON p.Name = @Name
            WHERE ct.Slot = @Slot;";

            var rows = await _db.ExecuteAsync(sql, new { Slot = slot, Name = name });
            if (rows == 0) throw new InvalidOperationException($"No matching slot or Pokémon named '{name}'.");
        }

        public async Task RemoveAsync(int slot)
        {
            const string sql = "DELETE FROM dbo.CurrentTeam WHERE Slot = @Slot;";
            await _db.ExecuteAsync(sql, new { Slot = slot });
        }


        public async Task<List<CurrentTeamInfo>> GetCurrentTeamAsync()
        {
            const string sql = @"
            SELECT ct.Slot,
                   p.Name,
                   CAST(NULL AS nvarchar(200)) AS ImageUrl
            FROM dbo.CurrentTeam ct
            JOIN dbo.Pokemon p ON p.PokemonID = ct.PokemonID
            ORDER BY ct.Slot;";

            var team = await _db.QueryAsync<CurrentTeamInfo>(sql);
            return team.ToList();
        }

    }
}
