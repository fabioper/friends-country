using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FriendsCountry.Countries.API.Models
{
    public class CreateCountry
    {
        //[Required]
        //public string FlagUri { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
