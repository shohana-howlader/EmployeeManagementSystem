namespace EmployeeManagementSystem.DTOs
{
    public class PerformanceReviewDto
    {
        public int PerformanceReviewId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime ReviewDate { get; set; }
        public int ReviewScore { get; set; }
        public string ReviewNotes { get; set; }
    }
}