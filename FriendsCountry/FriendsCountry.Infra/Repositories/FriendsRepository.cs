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
    class FriendsRepository : IFriendsRepository
    {
        private readonly DbSet<Friend> _friends;

        public FriendsRepository(ApplicationDbContext context)
        {
            _friends = context.Friends;
        }

        public async Task<Friend> Add(Friend friend)
        {
            await _friends.AddAsync(friend);

            return friend;
        }

        public async Task<IEnumerable<Friend>> GetAll()
        {
            return await Task.FromResult(_friends);
        }

        public async Task<IEnumerable<Friend>> GetBy(string keyword)
        {
            return await Task.FromResult(_friends.Where(f => f.Name.ToLower().Contains(keyword) || f.FamilyName.ToLower().Contains(keyword)));
        }

        public async Task<Friend> GetByCountry(Country country)
        {
            return await _friends.FindAsync(new
            {
                Country = country
            });
        }

        public async Task<Friend> GetById(long id)
        {
            return await _friends.FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<Friend> GetByState(State state)
        {
            return await _friends.FindAsync(new
            {
                State = state
            });
        }

        public async Task Remove(Friend friend)
        {
            await Task.FromResult(_friends.Remove(friend));
        }

        public Task<Friend> Update(Friend friend)
        {
            throw new NotImplementedException();
        }
    }
}
