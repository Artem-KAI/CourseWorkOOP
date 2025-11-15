using PL.Helper;

namespace PL.Menus
{
    public static class EmployerMenu
    {
        public static void ShowMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Управління замовниками (фірми) ===");
                Console.WriteLine("1. Переглянути всіх замовників");
                Console.WriteLine("2. Додати замовника");
                Console.WriteLine("3. Редагувати замовника");
                Console.WriteLine("4. Видалити замовника");
                Console.WriteLine("0. Назад");
                Console.Write("Виберіть дію: ");

                var choice = Console.ReadLine();
                try
                {
                    switch (choice)
                    {
                        case "1": ListEmployers(); break;
                        case "2": AddEmployer(); break;
                        case "3": EditEmployer(); break;
                        case "4": DeleteEmployer(); break;
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

        private static void ListEmployers()
        {
            var list = Program.EmployerService.GetAll();
            foreach (var e in list)
                Console.WriteLine($"{e.Id} | {e.FirstName} {e.LastName} | {e.CompanyName} | Email: {e.Email}");
        }

        private static void AddEmployer()
        {
            var firstName = InputHelper.ReadNonEmptyString("Ім'я: ");
            var lastName = InputHelper.ReadNonEmptyString("Прізвище: ");
            var company = InputHelper.ReadNonEmptyString("Назва компанії: ");
            var email = InputHelper.ReadEmail("Email: ");

            var employer = new BLL.Models.EmployerModel
            {
                Id = Guid.NewGuid(),
                FirstName = firstName,
                LastName = lastName,
                CompanyName = company,
                Email = email
            };

            Program.EmployerService.Add(employer);
            Console.WriteLine("Замовника додано успішно!");
        }

        private static void EditEmployer()
        {
            var id = InputHelper.ReadGuid("Введіть Id замовника для редагування: ");
            var e = Program.EmployerService.GetById(id);

            e.FirstName = InputHelper.ReadNonEmptyString($"Ім'я ({e.FirstName}): ", e.FirstName);
            e.LastName = InputHelper.ReadNonEmptyString($"Прізвище ({e.LastName}): ", e.LastName);
            e.CompanyName = InputHelper.ReadNonEmptyString($"Компанія ({e.CompanyName}): ", e.CompanyName);
            e.Email = InputHelper.ReadEmail($"Email ({e.Email}): ", e.Email);

            Program.EmployerService.Update(e);
            Console.WriteLine("Дані замовника оновлено успішно!");
        }

        private static void DeleteEmployer()
        {
            var id = InputHelper.ReadGuid("Введіть Id замовника для видалення: ");
            Program.EmployerService.Delete(id);
            Console.WriteLine("Замовника видалено успішно!");
        }
    }
}
