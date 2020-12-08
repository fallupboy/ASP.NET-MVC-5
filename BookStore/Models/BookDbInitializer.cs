using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BookStore.Models
{
    public class BookDbInitializer : DropCreateDatabaseAlways<BookContext>
    {
        protected override void Seed(BookContext db)
        {
            db.Books.Add(new Book { Name = "Game of Thrones", Author = "George R. R. Martin", Quantity = 5, Price = 45 });
            db.Books.Add(new Book { Name = "Fifty Shades of Grey", Author = "James, E. L.", Quantity = 0, Price = 25 });
            db.Books.Add(new Book { Name = "Harry Potter and the Deathly Hallows", Author = "Rowling, J.K.", Quantity = 2, Price = 30 });

            base.Seed(db);
        }
    }
}