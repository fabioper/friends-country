using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FriendsCountry.Countries.API.Models
{
    public class CreateFriend
    {
        [Required]
        public string PhotoUri { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string FamilyName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public DateTime Birthdate { get; set; }

        [Required]
        public long CountryId { get; set; }

        [Required]
        public long StateId { get; set; }
    }
}
