﻿using FriendsCountry.Data;
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
    public class FriendsRepository : IFriendsRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Friend> _friends;

        public FriendsRepository(ApplicationDbContext context)
        {
            _context = context;
            _friends = context.Friends;
        }

        public async Task<Friend> AddAsync(Friend friend)
        {
            //await _friends.AddAsync(friend);
            await _context.Database.ExecuteSqlRawAsync(
                "EXECUTE dbo.InsertFriend {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}",
                friend.PhotoUri,
                friend.Name,
                friend.FamilyName,
                friend.Email,
                friend.Phone,
                friend.Birthdate,
                friend.CountryId,
                friend.StateId);

            await _context.SaveChangesAsync();

            return friend;
        }

        public async Task<IEnumerable<Friend>> GetAllAsync()
        {
            return await Task.FromResult(_friends.Include(f => f.Friends));
        }

        public async Task<IEnumerable<Friend>> GetByAsync(string keyword)
        {
            return await Task.FromResult(_friends.Include(f => f.Friends).Where(f => f.Name.ToLower().Contains(keyword) || f.FamilyName.ToLower().Contains(keyword)));
        }

        public async Task<Friend> GetByCountryAsync(Country country)
        {
            return await _friends.FindAsync(new
            {
                Country = country
            });
        }

        public async Task<Friend> GetByIdAsync(long id)
        {
            //return await _friends.Include(f => f.Friends).FirstOrDefaultAsync(f => f.Id == id);

            var friend = _context.Friends.FromSqlRaw("EXECUTE dbo.GetFriend {0}", id).FirstOrDefault();

            return await Task.FromResult(friend);
        }

        public async Task<Friend> GetByStateAsync(State state)
        {
            return await _friends.FindAsync(new
            {
                State = state
            });
        }

        public async Task RemoveAsync(Friend friend)
        {
            _friends.Remove(friend);
            await _context.SaveChangesAsync();
        }

        public async Task<Friend> UpdateAsync(Friend friend)
        {
            //_context.Update(friend);
            _context.Friends.FromSqlRaw("EXECUTE dbo.UpdateFriend {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}", friend.PhotoUri, friend.Name, friend.FamilyName, friend.Email, friend.Phone, friend.Birthdate, friend.CountryId, friend.StateId)
                            .FirstOrDefault();

            await _context.SaveChangesAsync();

            return friend;
        }
    }
}
