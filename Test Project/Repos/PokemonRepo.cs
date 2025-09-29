using Dapper;
using System.Data;
using StatFinder.Models;

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

        public List<PokemonInfo> ReturnPokemonLike(string name, int limit = 250)
        {
            var parameters = new
            {
                NamePattern = $"%{name}%",
                Limit = limit,
            };
            
            var sql = @"SELECT TOP (@Limit) *
                        FROM dbo.Pokemon
                        WHERE Name LIKE @NamePattern;";
            
            return _db.Query<PokemonInfo>(sql, parameters).ToList();
        }



    }
}