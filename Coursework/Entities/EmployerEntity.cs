using DAL.Interfaces;

namespace DAL.Entities
{
    public class EmployerEntity : IEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();// створюється унікальний номер, коли створюється новий об'єкт
        public string CompanyName { get; set; } = string.Empty;
        public string ContactPerson { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<Guid> VacancyIds { get; set; } = new List<Guid>();// створює список для зберігання інших унікальних ідентифікаторів
    }
}
