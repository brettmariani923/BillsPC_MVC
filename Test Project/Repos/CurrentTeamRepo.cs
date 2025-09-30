using Dapper;
using System.Data;
using StatFinder.Models;

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
            var pattern = "%" + name + "%";

            const string sql = @"
            INSERT INTO dbo.CurrentTeam (Slot, PokemonID)
            VALUES (@Slot, (SELECT TOP (1) PokemonID FROM dbo.Pokemon WHERE Name LIKE @Pattern));";

            await _db.ExecuteAsync(sql, new { Slot = slot, Pattern = pattern });
        }


        public async Task UpdateAsync(int slot, string name)
        {
            var pattern = "%" + name + "%";

            const string sql = @"
            UPDATE dbo.CurrentTeam
            SET PokemonID = (SELECT TOP (1) PokemonID FROM dbo.Pokemon WHERE Name LIKE @Pattern)
            WHERE Slot = @Slot;";

            await _db.ExecuteAsync(sql, new { Slot = slot, Pattern = pattern });
        }


        public async Task RemoveAsync(int slot)
        {
            const string sql = "DELETE FROM dbo.CurrentTeam WHERE Slot = @Slot;";
            await _db.ExecuteAsync(sql, new { Slot = slot });
        }


        public async Task<List<CurrentTeamInfo>> GetCurrentTeamAsync()
        {
            const string sql = @"
            SELECT ct.Slot, p.Name, CAST(NULL AS nvarchar(200)) AS ImageUrl
            FROM dbo.CurrentTeam ct
            JOIN dbo.Pokemon p ON p.PokemonID = ct.PokemonID
            ORDER BY ct.Slot;";

            var team = await _db.QueryAsync<CurrentTeamInfo>(sql);
            return team.ToList();
        }


    }
}
