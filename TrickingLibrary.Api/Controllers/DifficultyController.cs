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
    [Route("api/difficulties")]
    public class DifficultyController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public DifficultyController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IEnumerable<Difficulty> All() => _dbContext.Difficulties.ToList();

        [HttpGet("{id}")]
        public Difficulty Get(string id) =>
            _dbContext.Difficulties.FirstOrDefault(d => d.Id.Equals(id, StringComparison.InvariantCultureIgnoreCase));

        [HttpGet("{id}/tricks")]
        public IEnumerable<Trick> ListTricksForDifficulty(string id) =>
            _dbContext.Tricks.Where(t => t.Difficulty.Equals(id, StringComparison.InvariantCultureIgnoreCase))
                             .ToList();

        [HttpPost]
        public async Task<Difficulty> Create([FromBody] Difficulty difficulty)
        {
            difficulty.Id = difficulty.Name.Replace(" ", "-").ToLowerInvariant();
            _dbContext.Add(difficulty);
            await _dbContext.SaveChangesAsync();
            return difficulty;
        }
    }
}