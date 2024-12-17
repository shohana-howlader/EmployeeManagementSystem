namespace EmployeeManagementSystem.DTOs
{
    public class EmployeeDto
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Position { get; set; }
        public DateTime JoinDate { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public IEnumerable<PerformanceReviewDto> PerformanceReviews { get; set; }
    }
}
