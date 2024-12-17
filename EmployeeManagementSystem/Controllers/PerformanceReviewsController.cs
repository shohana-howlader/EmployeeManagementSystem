using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerformanceReviewsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PerformanceReviewsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/PerformanceReviews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PerformanceReview>>> GetPerformanceReviews()
        {
            return await _context.PerformanceReviews.ToListAsync();
        }

        // GET: api/PerformanceReviews/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PerformanceReview>> GetPerformanceReview(int id)
        {
            var performanceReview = await _context.PerformanceReviews.FindAsync(id);

            if (performanceReview == null)
            {
                return NotFound();
            }

            return performanceReview;
        }

        // PUT: api/PerformanceReviews/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerformanceReview(int id, PerformanceReview performanceReview)
        {
            if (id != performanceReview.PerformanceReviewId)
            {
                return BadRequest();
            }

            _context.Entry(performanceReview).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PerformanceReviewExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/PerformanceReviews
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PerformanceReview>> PostPerformanceReview(PerformanceReview performanceReview)
        {
            _context.PerformanceReviews.Add(performanceReview);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPerformanceReview", new { id = performanceReview.PerformanceReviewId }, performanceReview);
        }

        // DELETE: api/PerformanceReviews/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerformanceReview(int id)
        {
            var performanceReview = await _context.PerformanceReviews.FindAsync(id);
            if (performanceReview == null)
            {
                return NotFound();
            }

            _context.PerformanceReviews.Remove(performanceReview);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PerformanceReviewExists(int id)
        {
            return _context.PerformanceReviews.Any(e => e.PerformanceReviewId == id);
        }
    }
}
