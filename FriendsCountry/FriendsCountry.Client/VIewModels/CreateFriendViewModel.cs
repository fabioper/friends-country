using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FriendsCountry.Client.VIewModels
{
    public class CreateFriendViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string FamilyName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Birthdate { get; set; }

        [Required]
        public long Country { get; set; }

        [Required]
        public long State { get; set; }
    }
}
