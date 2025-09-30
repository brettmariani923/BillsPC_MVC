using Dapper;
using System.Data;
using StatFinder.Models;
using StatFinder.Repos;

namespace StatFinder.Repos
{
    public class CurrentTeamRepo : ICurrentTeamRepo
    {
        private readonly IDbConnection _db;
        public CurrentTeamRepo(IDbConnection db)
        {
            _db = db;
        }

        public void InsertPokemonInSlot(int slot, string pokemonName)
        {
            const string sql = "INSERT INTO CurrentTeam (Slot, PokemonName) VALUES (@Slot, @PokemonName);";
            _db.Execute(sql, new { Slot = slot, PokemonName = pokemonName });
        }

        public void UpdatePokemonInSlot(int slot, string pokemonName)
        {
            const string sql = "UPDATE CurrentTeam SET PokemonName = @PokemonName WHERE Slot = @Slot;";
            _db.Execute(sql, new { Slot = slot, PokemonName = pokemonName });
        }

        public void RemoveFromSlot(int slot)
        {
            const string sql = "DELETE FROM CurrentTeam WHERE Slot = @Slot;";
            _db.Execute(sql, new { Slot = slot });
        }

        public List<CurrentTeamInfo> GetCurrentTeam()
        {
            const string sql = "SELECT Slot, PokemonName FROM CurrentTeam ORDER BY Slot;";
            return _db.Query<CurrentTeamInfo>(sql).ToList();
        }

    }
}
