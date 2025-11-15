using DAL.Interfaces;

namespace DAL.Entities
{
    public class VacancyEntity : IEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public Guid EmployerId { get; set; }
        public DateTime PostedDate { get; set; } = DateTime.UtcNow;
        public decimal? SalaryFrom { get; set; }
        public decimal? SalaryTo { get; set; }
        public bool IsOpen { get; set; } = true;
    }
}
