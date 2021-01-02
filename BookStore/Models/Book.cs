using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookStore.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required (ErrorMessage = "Please enter the book name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter the author's name")]
        public string Author { get; set; }

        [Required(ErrorMessage = "Please enter the quantity")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Please enter the price")]
        public int Price { get; set; }
    }
}