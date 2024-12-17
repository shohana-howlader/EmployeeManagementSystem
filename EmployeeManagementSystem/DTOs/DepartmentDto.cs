namespace EmployeeManagementSystem.DTOs
{
    public class DepartmentDto
    {
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public int ManagerId { get; set; }
        public decimal Budget { get; set; }
        public IEnumerable<EmployeeDto> Employees { get; set; }
    }
}
