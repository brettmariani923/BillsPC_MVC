using Dapper;
using System.Data;
using StatFinder.Models;
using System.Data.Common;

namespace StatFinder.Repos
{
    public class PokemonRepo : IPokemonRepo
    {
        private readonly IDbConnection _db;
        public PokemonRepo(IDbConnection db)
        {
            _db = db;
        }
        public List<PokemonInfo> GetAllPokemonInfo()
        {
            var sql = "SELECT * FROM dbo.Pokemon";
            return _db.Query<PokemonInfo>(sql).ToList();
        }

        public List<PokemonInfo> ReturnPokemonLike(string name, int limit = 25)
        {
            return _db.Query<PokemonInfo>(
                @"SELECT TOP (@Limit) * 
                FROM dbo.Pokemon 
                WHERE Name LIKE @NamePattern",
                new
                {
                    Limit = limit,
                    NamePattern = $"%{name}%"
                }
            ).ToList();
        }


    }
}