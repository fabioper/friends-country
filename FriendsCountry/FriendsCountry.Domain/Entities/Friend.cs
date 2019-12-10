using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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

        public long CountryId { get; set; }

        [ForeignKey(nameof(CountryId))]
        public Country Country { get; set; }


        public long StateId { get; set; }

        [ForeignKey(nameof(StateId))]
        public State State { get; set; }
    }
}
