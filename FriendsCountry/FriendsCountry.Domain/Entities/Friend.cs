using System;
using System.Collections.Generic;
using System.Text;

namespace FriendsCountry.Domain.Entities
{
    public class Friend : Entity
    {
        public Friend() => Friends = new HashSet<Friend>();

        public string Name { get; set; }

        public string FamilyName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public DateTime Birthdate { get; set; }

        public ICollection<Friend> Friends { get; set; }

        public Country Country { get; set; }

        public State State { get; set; }
    }
}
