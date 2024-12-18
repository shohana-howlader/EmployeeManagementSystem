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
    public class DepartmentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DepartmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetDepartmentDto>>> GetDepartments()
        {
            var departments = await _context.Departments
                .Include(d => d.Employees)
                .Select(d => new GetDepartmentDto
                {
                    DepartmentId = d.DepartmentId,
                    Name = d.Name,
                    ManagerId = d.ManagerId,
                    ManagerName = _context.Employees
                      .Where(e => e.EmployeeId == d.ManagerId)
                      .Select(e => e.Name)
                      .FirstOrDefault(),
                    Budget = d.Budget,
                    Employees = d.Employees.Select(e => new EmployeeDto
                    {
                        EmployeeId = e.EmployeeId,
                        Name = e.Name,
                        Email = e.Email
                    }).ToList()
                })
                .ToListAsync();

            return Ok(departments);
        }

        [HttpPost]
        public async Task<ActionResult> CreateDepartment(PostDepartmentDto postDepartmentDto)
        {
            var department = new Department
            {
                Name = postDepartmentDto.Name,
                ManagerId = postDepartmentDto.ManagerId,
                Budget = postDepartmentDto.Budget
            };

            _context.Departments.Add(department);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDepartments), new { id = department.DepartmentId }, department);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, PostDepartmentDto departmentDto)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }

            department.Name = departmentDto.Name;
            department.ManagerId = departmentDto.ManagerId;
            department.Budget = departmentDto.Budget;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }

            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
