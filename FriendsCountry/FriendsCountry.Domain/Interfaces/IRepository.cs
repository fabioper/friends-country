﻿using FriendsCountry.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FriendsCountry.Domain.Interfaces
{
    public interface IRepository<T> where T: Entity
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetByIdAsync(long id);

        Task<IEnumerable<T>> GetByAsync(string keyword);

        Task<T> AddAsync(T entity);

        Task<T> UpdateAsync(T entity);

        Task RemoveAsync(T entity);

        Task<int> Count();
    }
}
