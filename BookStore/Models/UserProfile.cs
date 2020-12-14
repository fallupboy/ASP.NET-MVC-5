using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookStore.Models
{
    public class UserProfile
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string UserFirstName { get; set; }

        [Required]
        public string UserSecondName { get; set; }

        [Required]
        public string UserEmail { get; set; }

        [Required]
        public string UserPassword { get; set; }
    }
}