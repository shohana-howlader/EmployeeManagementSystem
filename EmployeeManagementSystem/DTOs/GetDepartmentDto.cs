namespace EmployeeManagementSystem.DTOs
{
    public class GetDepartmentDto
    {
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public int ManagerId { get; set; }
        public string ManagerName { get; set; }
        public decimal Budget { get; set; }
        public IEnumerable<EmployeeDto> Employees { get; set; }
    }
}
