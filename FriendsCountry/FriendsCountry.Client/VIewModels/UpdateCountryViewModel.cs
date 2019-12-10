using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FriendsCountry.Client.VIewModels
{
    public class UpdateCountryViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}
