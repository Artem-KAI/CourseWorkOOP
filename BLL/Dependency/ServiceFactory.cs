using BLL.Services;
using DAL.Storage;
using DAL.Repositories;
using DAL.Interfaces;
using BLL.Interfaces;

namespace BLL.Dependency
{
    public static class ServiceFactory
    {
        private static readonly FileDataContext _context = new FileDataContext(new DAL.Storage.FileStorageSettings());

        public static IVacancyService CreateVacancyService()
        {
            IVacancyRepository repo = new VacancyRepository(_context);
            return new VacancyService(repo);
        }

        public static IResumeService CreateResumeService()
        {
            IResumeRepository repo = new ResumeRepository(_context);
            return new ResumeService(repo);
        }

        public static IUnemployedService CreateUnemployedService()
        {
            IUnemployedRepository repo = new UnemployedRepository(_context);
            return new UnemployedService(repo);
        }

        public static IEmployerService CreateEmployerService()
        {
            IEmployerRepository repo = new EmployerRepository(_context);
            return new EmployerService(repo);
        }
       
        // АЛІАСИ ДЛЯ PL
        public static IVacancyService GetVacancyService() => CreateVacancyService();
        public static IResumeService GetResumeService() => CreateResumeService();
        public static IUnemployedService GetUnemployedService() => CreateUnemployedService();
        public static IEmployerService GetEmployerService() => CreateEmployerService();
    }
}
