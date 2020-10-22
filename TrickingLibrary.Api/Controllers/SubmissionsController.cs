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
    public class SubmissionsController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public SubmissionsController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IEnumerable<Submission> All() => _dbContext.Submissions.ToList();

        [HttpPost]
        public async Task<Submission> Create([FromBody] Submission submission)
        {
            _dbContext.Submissions.Add(submission);
            await _dbContext.SaveChangesAsync();
            return submission;
        }

        [HttpPut]
        public async Task<Submission> Update([FromBody] Submission submission)
        {
            if (submission.Id == 0) return null;

            _dbContext.Submissions.Update(submission);
            await _dbContext.SaveChangesAsync();
            return submission;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var submission = _dbContext.Submissions.FirstOrDefault(t => t.Id.Equals(id));
            if (submission == null) return NotFound();
            submission.Deleted = true;

            await _dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}