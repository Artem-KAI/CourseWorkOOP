namespace DAL.Interfaces
{
    // Базова сутність: гарантує наявність Id
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}
