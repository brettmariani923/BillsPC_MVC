using System.Collections.Generic;
using System.Threading.Tasks;
using StatFinder.Models;

namespace StatFinder.Repos
{
    public interface ICurrentTeamRepo
    {
        public Task InsertAsync(int slot, string name);   // add new row
        public Task UpdateAsync(int slot, string name);   // update existing row
        public Task RemoveAsync(int slot);                       // delete row
        Task<List<CurrentTeamInfo>> GetCurrentTeamAsync();      // read team
    }
}
