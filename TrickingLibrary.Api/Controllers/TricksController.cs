using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrickingLibrary.Api.Form;
using TrickingLibrary.Api.ViewModel;
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
        public IEnumerable<object> All() => _dbContext.Tricks.Select(TrickViewModels.Default).ToList();

        [HttpGet("{id}")]
        public object Get(string id) =>
            _dbContext.Tricks.Where(t => t.Id.Equals(id, StringComparison.InvariantCultureIgnoreCase))
                             .Select(TrickViewModels.Default)
                             .FirstOrDefault();

        [HttpGet("{trickId}/submissions")]
        public IEnumerable<Submission> ListSubmissionsForTrick(string trickId) =>
            _dbContext.Submissions
                .Where(s => s.TrickId.Equals(trickId, StringComparison.InvariantCultureIgnoreCase))
                .ToList();

        [HttpPost]
        public async Task<object> Create([FromBody] TrickForm trickForm)
        {
            var trick = new Trick
            {
                Id = trickForm.Name.Replace(" ", "-").ToLowerInvariant(),
                Name = trickForm.Name,
                Description = trickForm.Description,
                Difficulty = trickForm.Difficulty,
                TrickCategories = trickForm.Categories.Select(c => new TrickCategory { CategoryId = c }).ToList()
            };
            _dbContext.Tricks.Add(trick);

            await _dbContext.SaveChangesAsync();
            return TrickViewModels.Default.Compile().Invoke(trick);
        }

        [HttpPut]
        public async Task<object> Update([FromBody] Trick trick)
        {
            if (string.IsNullOrEmpty(trick.Id)) return null;

            _dbContext.Tricks.Update(trick);
            await _dbContext.SaveChangesAsync();
            return TrickViewModels.Default.Compile().Invoke(trick);
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