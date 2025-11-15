using BLL.Dependency;
using BLL.Interfaces;
using PL.Menus;
using System.Text;

namespace PL
{
    internal class Program
    {
        // Сервіси доступні глобально в PL через ServiceFactory
        public static IVacancyService VacancyService { get; private set; } = null!;
        public static IResumeService ResumeService { get; private set; } = null!;
        public static IUnemployedService UnemployedService { get; private set; } = null!;
        public static IEmployerService EmployerService { get; private set; } = null!;

        private static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
            // Ініціалізація сервісів через фабрику
            VacancyService = ServiceFactory.CreateVacancyService();
            ResumeService = ServiceFactory.CreateResumeService();
            UnemployedService = ServiceFactory.CreateUnemployedService();
            EmployerService = ServiceFactory.CreateEmployerService();

            ShowMainMenu();
        }

        private static void ShowMainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Біржа праці - Головне меню ===");
                Console.WriteLine("1. Управління безробітними");
                Console.WriteLine("2. Управління резюме");
                Console.WriteLine("3. Управління вакансіями");
                Console.WriteLine("4. Управління замовниками (фірми)");
                Console.WriteLine("0. Вихід");
                Console.Write("Виберіть дію: ");

                var choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1": UnemployedMenu.ShowMenu(); break;
                        case "2": ResumeMenu.ShowMenu(); break;
                        case "3": VacancyMenu.ShowMenu(); break;
                        case "4": EmployerMenu.ShowMenu(); break;
                        case "0": return;
                        default: Console.WriteLine("Невірний вибір!"); break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Помилка: {ex.Message}");
                }

                Console.WriteLine("Натисніть будь-яку клавішу для продовження...");
                Console.ReadKey();
            }
        }
    }
}
