using FriendsCountry.Data;
using FriendsCountry.Domain.Entities;
using FriendsCountry.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendsCountry.Infra.Repositories
{
    public class StatesRepository : IStatesRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<State> _states;

        public StatesRepository(ApplicationDbContext context)
        {
            _context = context;
            _states = context.States;
        }

        public async Task<State> AddAsync(State state)
        {
            await _context.Database.ExecuteSqlInterpolatedAsync($"EXECUTE dbo.InsertState {state.Name}, {state.FlagUri}");
            await _context.SaveChangesAsync();

            return state;
        }

        public Task<int> Count()
        {
            var result = _context.Database.ExecuteSqlRaw(@"SELECT COUNT(Id) FROM dbo.States");

            return Task.FromResult(result);
        }

        public async Task<IEnumerable<State>> GetAllAsync()
        {
            return await Task.FromResult(_states);
        }

        public async Task<IEnumerable<State>> GetByAsync(string keyword)
        {
            return await Task.FromResult(_states.Where(s => s.Name.ToLower().Contains(keyword)));
        }

        public async Task<State> GetByIdAsync(long id)
        {
            var state = _context.States.FromSqlInterpolated($"EXECUTE dbo.GetState {id}").AsEnumerable().FirstOrDefault();

            return await Task.FromResult(state);
        }

        public async Task RemoveAsync(State state)
        {
            await _context.Database.ExecuteSqlInterpolatedAsync($"EXECUTE dbo.DeleteState {state.Id}");

            await _context.SaveChangesAsync();
        }

        public async Task<State> UpdateAsync(State state)
        {
            await _context.Database.ExecuteSqlInterpolatedAsync($"EXECUTE dbo.UpdateState {state.Id}, {state.Name}, {state.FlagUri}");

            await _context.SaveChangesAsync();

            return state;
        }
    }
}
