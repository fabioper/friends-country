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
            await _context.Database.ExecuteSqlInterpolatedAsync($"EXECUTE dbo.InsertCountry {country.FlagUri}, {country.Name}");
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
            var country = _context.Countries.FromSqlInterpolated($"EXECUTE dbo.GetCountry {id}").AsEnumerable<Country>().FirstOrDefault();

            return await Task.FromResult(country);
        }

        public async Task RemoveAsync(Country country)
        {
            _countries.Remove(country);
            await _context.SaveChangesAsync();
        }

        public async Task<Country> UpdateAsync(Country country)
        {
            await _context.Database.ExecuteSqlInterpolatedAsync($"EXECUTE dbo.UpdateCountry {country.Id}, {country.FlagUri}, {country.Name}");

            await _context.SaveChangesAsync();

            return country;
        }
    }
}
