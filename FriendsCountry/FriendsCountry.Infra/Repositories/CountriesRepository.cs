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
        private readonly DbSet<Country> _countries;

        public CountriesRepository(ApplicationDbContext context)
        {
            _countries = context.Countries;
        }

        public async Task<Country> Add(Country country)
        {
            await _countries.AddAsync(country);

            return country;
        }

        public async Task<IEnumerable<Country>> GetAll()
        {
            return await Task.FromResult(_countries);
        }

        public async Task<IEnumerable<Country>> GetBy(string keyword)
        {
            return await Task.FromResult(_countries.Where(c => c.Name.ToLower().Contains(keyword)));
        }

        public async Task<Country> GetById(long id)
        {
            return await _countries.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task Remove(Country country)
        {
            await Task.FromResult(_countries.Remove(country));
        }

        public Task<Country> Update(Country country) => throw new NotImplementedException();
    }
}
