using FriendsCountry.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FriendsCountry.Domain.Interfaces
{
    public interface IFriendsRepository : IRepository<Friend>
    {
        Task<Friend> GetByCountryAsync(Country country);

        Task<Friend> GetByStateAsync(State state);
    }
}
