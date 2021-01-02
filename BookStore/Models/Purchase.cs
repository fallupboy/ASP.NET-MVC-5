using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookStore.Models
{
    public class Purchase
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }

        [Required(ErrorMessage = "Please enter the quantity")]
        public int Amount { get; set; }

        [Required(ErrorMessage = "Please enter the address")]
        public string Address { get; set; }

        public DateTime Date { get; set; }
    }
}