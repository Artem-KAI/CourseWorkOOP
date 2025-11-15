using PL.Helper;

namespace PL.Menus
{
    public static class VacancyMenu
    {
        public static void ShowMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Управління вакансіями ===");
                Console.WriteLine("1. Переглянути всі вакансії");
                Console.WriteLine("2. Додати вакансію");
                Console.WriteLine("3. Редагувати вакансію");
                Console.WriteLine("4. Видалити вакансію");
                Console.WriteLine("5. Пошук вакансій по ключовому слову");
                Console.WriteLine("0. Назад");
                Console.Write("Виберіть дію: ");

                var choice = Console.ReadLine();
                try
                {
                    switch (choice)
                    {
                        case "1": ListVacancies(); break;
                        case "2": AddVacancy(); break;
                        case "3": EditVacancy(); break;
                        case "4": DeleteVacancy(); break;
                        case "5": SearchVacancies(); break;
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

        private static void ListVacancies()
        {
            var list = Program.VacancyService.GetAll();
            foreach (var v in list)
                Console.WriteLine($"{v.Id} | {v.Title} | Категорія: {v.Category} | Відкрита: {v.IsOpen}");
        }

        private static void AddVacancy()
        {
            var title = InputHelper.ReadNonEmptyString("Назва вакансії: ");
            var category = InputHelper.ReadNonEmptyString("Категорія: ");
            var isOpen = InputHelper.ReadNonEmptyString("Відкрита (true/false): ");

            var vacancy = new BLL.Models.VacancyModel
            {
                Id = Guid.NewGuid(),
                Title = title,
                Category = category,
                IsOpen = bool.TryParse(isOpen, out var res) ? res : true
            };
            Program.VacancyService.Add(vacancy);
            Console.WriteLine("Вакансію додано успішно!");
        }

        private static void EditVacancy()
        {
            var id = InputHelper.ReadGuid("Введіть Id вакансії для редагування: ");
            var v = Program.VacancyService.GetById(id);

            v.Title = InputHelper.ReadNonEmptyString($"Назва ({v.Title}): ", v.Title);
            v.Category = InputHelper.ReadNonEmptyString($"Категорія ({v.Category}): ", v.Category);
            var isOpenStr = InputHelper.ReadNonEmptyString($"Відкрита ({v.IsOpen}): ", v.IsOpen.ToString());
            v.IsOpen = bool.TryParse(isOpenStr, out var res) ? res : v.IsOpen;

            Program.VacancyService.Update(v);
            Console.WriteLine("Дані вакансії оновлено успішно!");
        }

        private static void DeleteVacancy()
        {
            var id = InputHelper.ReadGuid("Введіть Id вакансії для видалення: ");
            Program.VacancyService.Delete(id);
            Console.WriteLine("Вакансію видалено успішно!");
        }

        private static void SearchVacancies()
        {
            var keyword = InputHelper.ReadNonEmptyString("Введіть ключове слово для пошуку: ");
            var results = Program.VacancyService.GetAll()
                .Where(v => v.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase)
                         || v.Category.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (!results.Any())
            {
                Console.WriteLine("Вакансій за цим ключовим словом не знайдено.");
                return;
            }

            Console.WriteLine("Результати пошуку:");
            foreach (var v in results)
                Console.WriteLine($"{v.Id} | {v.Title} | Категорія: {v.Category} | Відкрита: {v.IsOpen}");
        }
    }
}
