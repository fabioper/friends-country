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
    public class CountriesRepository : ICountriesRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Country> _countries;

        public CountriesRepository(ApplicationDbContext context)
        {
            _context = context;
            _countries = context.Countries;
        }

        public async Task<Country> AddAsync(Country country)
        {
            var addedCountry = await _countries.AddAsync(country);
            await _context.SaveChangesAsync();

            return country;
        }

        public async Task<IEnumerable<Country>> GetAllAsync()
        {
            return await Task.FromResult(_countries.Include(c => c.States));
        }

        public async Task<IEnumerable<Country>> GetByAsync(string keyword)
        {
            return await Task.FromResult(_countries.Include(c => c.States).Where(c => c.Name.ToLower().Contains(keyword)));
        }

        public async Task<Country> GetByIdAsync(long id)
        {
            return await _countries.Include(c => c.States).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task RemoveAsync(Country country)
        {
            _countries.Remove(country);
            await _context.SaveChangesAsync();
        }

        public async Task<Country> UpdateAsync(Country country)
        {
            _context.Update(country);

            await _context.SaveChangesAsync();

            return country;
        }
    }
}
