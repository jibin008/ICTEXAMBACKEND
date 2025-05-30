using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Dotnetapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/categories
        [HttpGet]
        public ActionResult<IEnumerable<CategoryDto>> GetCategories()
        {
            var categories = _context.Categories.Select(c => new CategoryDto
            {
                cId = c.cId,  // Include Category ID
                Name = c.Name,
                Description = c.Description
            }).ToList();

            return categories;
        }

        // POST: api/categories
        [HttpPost]
        public ActionResult<Category> PostCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetCategories), new { id = category.cId }, category);
        }
    }

    // DTO to include cId in GET response
    public class CategoryDto
    {
        public int cId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
