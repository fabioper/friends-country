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
            await _states.AddAsync(state);

            await _context.SaveChangesAsync();

            return state;
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
            return await _states.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task RemoveAsync(State state)
        {
            _states.Remove(state);

            await _context.SaveChangesAsync();
        }

        public async Task<State> UpdateAsync(State state)
        {
            _context.Update(state);

            await _context.SaveChangesAsync();

            return state;
        }
    }
}
