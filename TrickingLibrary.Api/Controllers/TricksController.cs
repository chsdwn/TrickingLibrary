using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrickingLibrary.Data;
using TrickingLibrary.Models;

namespace TrickingLibrary.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TricksController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public TricksController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IEnumerable<Trick> All() => _dbContext.Tricks.ToList();

        [HttpGet("{id}")]
        public Trick Get(string id) =>
            _dbContext.Tricks.FirstOrDefault(t => t.Id.Equals(id, StringComparison.InvariantCultureIgnoreCase));

        [HttpGet("{trickId}/submissions")]
        public IEnumerable<Submission> ListSubmissionsForTrick(string trickId) =>
            _dbContext.Submissions
                .Where(s => s.TrickId.Equals(trickId, StringComparison.InvariantCultureIgnoreCase))
                .ToList();

        [HttpPost]
        public async Task<Trick> Create([FromBody] Trick trick)
        {
            trick.Id = trick.Name.Replace(" ", "-").ToLowerInvariant();
            _dbContext.Tricks.Add(trick);

            await _dbContext.SaveChangesAsync();
            return trick;
        }

        [HttpPut]
        public async Task<Trick> Update([FromBody] Trick trick)
        {
            if (string.IsNullOrEmpty(trick.Id)) return null;

            _dbContext.Tricks.Update(trick);
            await _dbContext.SaveChangesAsync();
            return trick;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var trick = _dbContext.Tricks.FirstOrDefault(t => t.Id.Equals(id));
            if (trick == null) return NotFound();
            trick.Deleted = true;

            await _dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}