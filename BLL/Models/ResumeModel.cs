namespace BLL.Models
{
    public class ResumeModel
    {
        public Guid Id { get; set; }
        public Guid UnemployedId { get; set; }
        public string Title { get; set; } = string.Empty;
        public List<string> Skills { get; set; } = new List<string>();
        public int ExperienceYears { get; set; }
        public string Education { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public string? Category { get; set; }
    }
}
