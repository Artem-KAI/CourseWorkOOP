using PL.Helper;

namespace PL.Menus
{
    public static class UnemployedMenu
    {
        public static void ShowMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Управління безробітними ===");
                Console.WriteLine("1. Переглянути всіх безробітних");
                Console.WriteLine("2. Додати безробітного");
                Console.WriteLine("3. Редагувати безробітного");
                Console.WriteLine("4. Видалити безробітного");
                Console.WriteLine("5. Сортування по імені");
                Console.WriteLine("6. Сортування по прізвищу");
                Console.WriteLine("0. Назад");
                Console.Write("Виберіть дію: ");

                var choice = Console.ReadLine();
                try
                {
                    switch (choice)
                    {
                        case "1": ListUnemployed(); break;
                        case "2": AddUnemployed(); break;
                        case "3": EditUnemployed(); break;
                        case "4": DeleteUnemployed(); break;
                        case "5": SortByFirstName(); break;
                        case "6": SortByLastName(); break;
                        case "0": return;
                        default: Console.WriteLine("Невірний вибір!"); break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Помилка: {ex.Message}");
                }

                Console.WriteLine("Натисніть будь-яку клавішу, щоб продовжити...");
                Console.ReadKey();
            }
        }

        private static void ListUnemployed()
        {
            var list = Program.UnemployedService.GetAll();
            foreach (var u in list)
                Console.WriteLine($"{u.Id} | {u.FirstName} {u.LastName} | Email: {u.Email}");
        }

        private static void AddUnemployed()
        {
            var firstName = InputHelper.ReadNonEmptyString("Введіть ім'я: ");
            var lastName = InputHelper.ReadNonEmptyString("Введіть прізвище: ");
            var email = InputHelper.ReadEmail("Введіть email: ");

            var unemployed = new BLL.Models.UnemployedModel
            {
                Id = Guid.NewGuid(),
                FirstName = firstName,
                LastName = lastName,
                Email = email
            };

            Program.UnemployedService.Add(unemployed);
            Console.WriteLine("Безробітного додано успішно!");
        }

        private static void EditUnemployed()
        {
            var id = InputHelper.ReadGuid("Введіть Id безробітного для редагування: ");
            var u = Program.UnemployedService.GetById(id);

            u.FirstName = InputHelper.ReadNonEmptyString($"Ім'я ({u.FirstName}): ", u.FirstName);
            u.LastName = InputHelper.ReadNonEmptyString($"Прізвище ({u.LastName}): ", u.LastName);
            u.Email = InputHelper.ReadEmail($"Email ({u.Email}): ", u.Email);

            Program.UnemployedService.Update(u);
            Console.WriteLine("Дані безробітного оновлено успішно!");
        }

        private static void DeleteUnemployed()
        {
            var id = InputHelper.ReadGuid("Введіть Id безробітного для видалення: ");
            Program.UnemployedService.Delete(id);
            Console.WriteLine("Безробітного видалено успішно!");
        }

        private static void SortByFirstName()
        {
            var list = Program.UnemployedService.GetAll().OrderBy(u => u.FirstName);
            Console.WriteLine("Список по імені:");
            foreach (var u in list)
                Console.WriteLine($"{u.Id} | {u.FirstName} {u.LastName} | Email: {u.Email}");
        }

        private static void SortByLastName()
        {
            var list = Program.UnemployedService.GetAll().OrderBy(u => u.LastName);
            Console.WriteLine("Список по прізвищу:");
            foreach (var u in list)
                Console.WriteLine($"{u.Id} | {u.FirstName} {u.LastName} | Email: {u.Email}");
        }
    }
}
