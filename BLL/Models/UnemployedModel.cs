namespace BLL.Models
{
    public class UnemployedModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime? DOB { get; set; }
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<Guid> ResumeIds { get; set; } = new List<Guid>();
    }
}
