using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Dotnetapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/books
        [HttpGet]
        public ActionResult<IEnumerable<BookResponse>> GetBooks()
        {
            var books = _context.Books
                .Select(b => new BookResponse
                {
                    bId = b.bId,
                    Title = b.Title,
                    Description = b.Description,
                    PublicationYear = b.PublicationYear,
                    Author = _context.Authors
                        .Where(a => a.aId == b.aId)
                        .Select(a => new AuthorResponse { aId = a.aId, Name = a.Name, Bio = a.Bio })
                        .FirstOrDefault(),
                    Category = _context.Categories
                        .Where(c => c.cId == b.cId)
                        .Select(c => new CategoryResponse { cId = c.cId, Name = c.Name, Description = c.Description })
                        .FirstOrDefault()
                }).ToList();

            return books;
        }

        // POST: api/books
        [HttpPost]
        public ActionResult<Book> PostBook(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetBooks), new { id = book.bId }, book);
        }
    }

    // Response Classes for structured JSON output
    public class BookResponse
    {
        public int bId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int PublicationYear { get; set; }
        public AuthorResponse Author { get; set; }
        public CategoryResponse Category { get; set; }
    }

    public class AuthorResponse
    {
        public int aId { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
    }

    public class CategoryResponse
    {
        public int cId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
