using FriendsCountry.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FriendsCountry.Domain.Interfaces
{
    public interface IRepository<T> where T: Entity
    {
        Task<IEnumerable<T>> GetAll();

        Task<T> GetById(long id);

        Task<IEnumerable<T>> GetBy(string keyword);

        Task<T> Add(T entity);

        Task<T> Update(T entity);

        Task Remove(T entity);
    }
}
