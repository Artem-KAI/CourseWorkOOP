namespace BLL.Models
{
    public class EmployerModel
    {
        public Guid Id { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string ContactPerson => $"{FirstName} {LastName}";
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<Guid> VacancyIds { get; set; } = new List<Guid>();
    }
}
