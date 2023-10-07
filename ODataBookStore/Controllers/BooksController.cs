using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using ODataBookStore.Models;

namespace ODataBookStore.Controllers
{
    public class BooksController : ODataController
    {
        private BookStoreContext db;

        public BooksController(BookStoreContext context)
        {
            db = context;
            db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            if (context.Books.Count() == 0)
            {
                foreach (var book in DataSource.GetBooks())
                {
                    context.Books.Add(book);
                    context.Presses.Add(book.Press);
                }

                context.SaveChanges();  
            }
        }

        [EnableQuery(PageSize = 1)]
        public IActionResult Get()
        {
            return Ok(db.Books);
        }

        [EnableQuery]
        public IActionResult Get(int key, string version) 
        {
            return Ok(db.Books.FirstOrDefault(c => c.Id == key));
        }

        [EnableQuery]
        public IActionResult Post([FromBody] Book book)
        {
            db.Books.Add(book);
            db.SaveChanges();

            return Created(book);
        }

        [EnableQuery]
        public IActionResult Delete([FromBody] int key) {
            Book book = db.Books.FirstOrDefault(c => c.Id == key);

            if (book == null)
            {
                return NotFound();
            }

            db.Books.Remove(book);
            db.SaveChanges();

            return Ok();
        }
    }
}
