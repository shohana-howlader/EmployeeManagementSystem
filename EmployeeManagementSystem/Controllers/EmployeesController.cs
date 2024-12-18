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
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<EmployeeDto>> GetEmployees()
        {
            var employees = _context.Employees
                .Where(e => !e.IsDeleted)
                .Select(e => new EmployeeDto
                {
                    EmployeeId = e.EmployeeId,
                    Name = e.Name,
                    Email = e.Email,
                    Phone = e.Phone,
                    Position = e.Position,
                    JoinDate = e.JoinDate,
                    DepartmentId = e.DepartmentId,
                    DepartmentName = e.Department.Name,
                    PerformanceReviews = e.PerformanceReviews.Select(r => new PerformanceReviewDto
                    {
                        PerformanceReviewId = r.PerformanceReviewId,
                        EmployeeId = r.EmployeeId,
                        ReviewDate = r.ReviewDate,
                        ReviewScore = r.ReviewScore,
                        ReviewNotes = r.ReviewNotes
                    })
                })
                .ToList();

            return Ok(employees);
        }

        // GET: api/employees/{id}
        [HttpGet("{id}")]
        public ActionResult<EmployeeDto> GetEmployee(int id)
        {
            var employee = _context.Employees
                .Where(e => e.EmployeeId == id && !e.IsDeleted)
                .Select(e => new EmployeeDto
                {
                    EmployeeId = e.EmployeeId,
                    Name = e.Name,
                    Email = e.Email,
                    Phone = e.Phone,
                    Position = e.Position,
                    JoinDate = e.JoinDate,
                    DepartmentId = e.DepartmentId,
                    DepartmentName = e.Department.Name,
                    PerformanceReviews = e.PerformanceReviews.Select(r => new PerformanceReviewDto
                    {
                        PerformanceReviewId = r.PerformanceReviewId,
                        EmployeeId = r.EmployeeId,
                        ReviewDate = r.ReviewDate,
                        ReviewScore = r.ReviewScore,
                        ReviewNotes = r.ReviewNotes
                    })
                })
                .FirstOrDefault();

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        // POST: api/employees
        [HttpPost]
        public ActionResult<EmployeeDto> CreateEmployee(EmployeeDto employeeDto)
        {
            var employee = new Employee
            {
                Name = employeeDto.Name,
                Email = employeeDto.Email,
                Phone = employeeDto.Phone,
                Position = employeeDto.Position,
                JoinDate = employeeDto.JoinDate,
                DepartmentId = employeeDto.DepartmentId
            };

            _context.Employees.Add(employee);
            _context.SaveChanges();

            employeeDto.EmployeeId = employee.EmployeeId;
            return CreatedAtAction(nameof(GetEmployee), new { id = employee.EmployeeId }, employeeDto);
        }

        // PUT: api/employees/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateEmployee(int id, EmployeeDto employeeDto)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            employee.Name = employeeDto.Name;
            employee.Email = employeeDto.Email;
            employee.Phone = employeeDto.Phone;
            employee.Position = employeeDto.Position;
            employee.JoinDate = employeeDto.JoinDate;
            employee.DepartmentId = employeeDto.DepartmentId;

            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/employees/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            employee.IsDeleted = true;
            _context.SaveChanges();

            return NoContent();
        }
    }
}
