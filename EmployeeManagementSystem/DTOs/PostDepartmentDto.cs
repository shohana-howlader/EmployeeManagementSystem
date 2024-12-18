namespace EmployeeManagementSystem.DTOs
{
    public class PostDepartmentDto
    {
        public string Name { get; set; }
        public int ManagerId { get; set; }
        public decimal Budget { get; set; }
        public IEnumerable<int> EmployeeIds { get; set; } 
    }
}
