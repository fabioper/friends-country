using System;
using System.Collections.Generic;
using System.Text;

namespace FriendsCountry.Domain.Entities
{
    public class Country : Entity
    {
        public Country() => States = new HashSet<State>();

        public string FlagUri { get; set; }

        public string Name { get; set; }

        public ICollection<State> States { get; set; }
    }
}
