using DAL.Interfaces;

namespace DAL.Entities
{
    public class ResumeEntity : IEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UnemployedId { get; set; }
        public string Title { get; set; } = string.Empty; // бажана посада
        public List<string> Skills { get; set; } = new List<string>();
        public int ExperienceYears { get; set; } = 0;
        public string Education { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public string? Category { get; set; }
    }
}
