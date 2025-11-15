namespace BLL.Models
{
    public class VacancyModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public Guid EmployerId { get; set; }
        public DateTime PostedDate { get; set; }
        public decimal? SalaryFrom { get; set; }
        public decimal? SalaryTo { get; set; }
        public bool IsOpen { get; set; }
    }
}
