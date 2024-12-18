using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.DTOs;

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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PerformanceReviewDto>>> GetPerformanceReviews()
        {
            var reviews = await _context.PerformanceReviews
                .Include(r => r.Employee)
                .Select(r => new PerformanceReviewDto
                {
                    PerformanceReviewId = r.PerformanceReviewId,
                  //  EmployeeName = r.Employee.Name,
                    ReviewDate = r.ReviewDate,
                    ReviewScore = r.ReviewScore,
                    ReviewNotes = r.ReviewNotes
                })
                .ToListAsync();

            return Ok(reviews);
        }

        [HttpPost]
        public async Task<ActionResult> CreatePerformanceReview(PerformanceReviewDto reviewDto)
        {
            var review = new PerformanceReview
            {
                EmployeeId = reviewDto.EmployeeId,
                ReviewDate = reviewDto.ReviewDate,
                ReviewScore = reviewDto.ReviewScore,
                ReviewNotes = reviewDto.ReviewNotes
            };

            _context.PerformanceReviews.Add(review);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPerformanceReviews), new { id = review.PerformanceReviewId }, reviewDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePerformanceReview(int id, PerformanceReviewDto reviewDto)
        {
            var review = await _context.PerformanceReviews.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }

            review.ReviewDate = reviewDto.ReviewDate;
            review.ReviewScore = reviewDto.ReviewScore;
            review.ReviewNotes = reviewDto.ReviewNotes;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerformanceReview(int id)
        {
            var review = await _context.PerformanceReviews.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }

            _context.PerformanceReviews.Remove(review);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
