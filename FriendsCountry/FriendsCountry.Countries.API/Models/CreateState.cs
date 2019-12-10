using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriendsCountry.Countries.API.Models
{
    public class CreateState
    {
        [Required]
        public string FlagUri { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
