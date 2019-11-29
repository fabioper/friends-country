using FriendsCountry.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FriendsCountry.Domain.Interfaces
{
    interface IRepository<T> where T: Entity
    {
    }
}
