using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrickingLibrary.Data;
using TrickingLibrary.Models;

namespace TrickingLibrary.Api.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public CategoryController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IEnumerable<Category> All() => _dbContext.Categories.ToList();

        [HttpGet("{id}")]
        public Category Get(string id) =>
            _dbContext.Categories.FirstOrDefault(c => c.Id.Equals(id, StringComparison.InvariantCultureIgnoreCase));

        [HttpGet("{id}/tricks")]
        public IEnumerable<Trick> ListTricksForCategory(string id) =>
            _dbContext.TrickCategories.Include(tc => tc.Trick)
                                      .Where(tc => tc.CategoryId.Equals(id, StringComparison.InvariantCultureIgnoreCase))
                                      .Select(tc => tc.Trick)
                                      .ToList();

        [HttpPost]
        public async Task<Category> Create([FromBody] Category category)
        {
            category.Id = category.Name.Replace(" ", "-").ToLowerInvariant();
            _dbContext.Add(category);
            await _dbContext.SaveChangesAsync();
            return category;
        }
    }
}