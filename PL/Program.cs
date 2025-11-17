using System;
using System.Text;
using BLL.Dependency;
using BLL.Interfaces;
using PL.Menus;

namespace PL
{
    internal class Program
    {
        public static IVacancyService VacancyService { get; private set; } = null!;
        public static IResumeService ResumeService { get; private set; } = null!;
        public static IUnemployedService UnemployedService { get; private set; } = null!;
        public static IEmployerService EmployerService { get; private set; } = null!;

        private static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            VacancyService = ServiceFactory.GetVacancyService();
            ResumeService = ServiceFactory.GetResumeService();
            UnemployedService = ServiceFactory.GetUnemployedService();
            EmployerService = ServiceFactory.GetEmployerService();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Головне меню ===");
                Console.WriteLine("1. Вакансії");
                Console.WriteLine("2. Резюме");
                Console.WriteLine("3. Безробітні");
                Console.WriteLine("4. Роботодавці");
                Console.WriteLine("0. Вихід");

                Console.Write("Виберіть дію: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": VacancyMenu.ShowMenu(); break;
                    case "2": ResumeMenu.ShowMenu(); break;
                    case "3": UnemployedMenu.ShowMenu(); break;
                    case "4": EmployerMenu.ShowMenu(); break;
                    case "0": return;
                    default: Console.WriteLine("Некоректний вибір."); break;
                }

                Console.WriteLine("Натисніть будь-яку клавішу...");
                Console.ReadKey();
            }
        }
    }
}
