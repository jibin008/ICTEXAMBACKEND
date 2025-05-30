using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Dotnetapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AuthorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public class AuthorDto
        {
            public int aId { get; set; }
            public string Name { get; set; }
            public string Bio { get; set; }
        }

        // GET: api/authors
        [HttpGet("getauthors")]
        public ActionResult<IEnumerable<AuthorDto>> GetAuthors()
        {
            var authors = _context.Authors.Select(a => new AuthorDto
            {
                aId = a.aId,
                Name = a.Name,
                Bio = a.Bio
            }).ToList();

            return authors;
        }

        // POST: api/authors
        [HttpPost("addauthor")]
        public ActionResult<Author> PostAuthor(Author author)
        {
            _context.Authors.Add(author);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetAuthors), new { id = author.aId }, author);
        }
    }
}
