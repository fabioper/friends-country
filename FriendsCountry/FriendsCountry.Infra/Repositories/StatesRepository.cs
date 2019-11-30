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
        private readonly DbSet<State> _states;

        public StatesRepository(ApplicationDbContext context)
        {
            _states = context.States;
        }

        public async Task<State> Add(State state)
        {
            await _states.AddAsync(state);

            return state;
        }

        public async Task<IEnumerable<State>> GetAll()
        {
            return await Task.FromResult(_states);
        }

        public async Task<IEnumerable<State>> GetBy(string keyword)
        {
            return await Task.FromResult(_states.Where(s => s.Name.ToLower().Contains(keyword)));
        }

        public async Task<State> GetById(long id)
        {
            return await _states.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task Remove(State state)
        {
            await Task.FromResult(_states.Remove(state));
        }

        public Task<State> Update(State state) => throw new NotImplementedException();
    }
}
