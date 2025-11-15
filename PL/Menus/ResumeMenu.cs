using PL.Helper;

namespace PL.Menus
{
    public static class ResumeMenu
    {
        public static void ShowMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Управління резюме ===");
                Console.WriteLine("1. Переглянути всі резюме");
                Console.WriteLine("2. Додати резюме");
                Console.WriteLine("3. Редагувати резюме");
                Console.WriteLine("4. Видалити резюме");
                Console.WriteLine("5. Пошук резюме по ключовому слову");
                Console.WriteLine("0. Назад");
                Console.Write("Виберіть дію: ");

                var choice = Console.ReadLine();
                try
                {
                    switch (choice)
                    {
                        case "1": ListResumes(); break;
                        case "2": AddResume(); break;
                        case "3": EditResume(); break;
                        case "4": DeleteResume(); break;
                        case "5": SearchResumes(); break;
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

        private static void ListResumes()
        {
            var list = Program.ResumeService.GetAll();
            foreach (var r in list)
                Console.WriteLine($"{r.Id} | {r.Title} | Кандидат: {r.UnemployedId} | Досвід: {r.ExperienceYears} років");
        }

        private static void AddResume()
        {
            var title = InputHelper.ReadNonEmptyString("Назва резюме: ");
            var unemployedId = InputHelper.ReadGuid("Id кандидата (безробітного): ");
            var experience = InputHelper.ReadNonEmptyString("Досвід роботи (років): ");

            var resume = new BLL.Models.ResumeModel
            {
                Id = Guid.NewGuid(),
                Title = title,
                UnemployedId = unemployedId,
                ExperienceYears = int.TryParse(experience, out var years) ? years : 0
            };
            Program.ResumeService.Add(resume);
            Console.WriteLine("Резюме додано успішно!");
        }

        private static void EditResume()
        {
            var id = InputHelper.ReadGuid("Введіть Id резюме для редагування: ");
            var r = Program.ResumeService.GetById(id);

            r.Title = InputHelper.ReadNonEmptyString($"Назва ({r.Title}): ", r.Title);
            var exp = InputHelper.ReadNonEmptyString($"Досвід ({r.ExperienceYears}): ", r.ExperienceYears.ToString());
            r.ExperienceYears = int.TryParse(exp, out var years) ? years : r.ExperienceYears;

            Program.ResumeService.Update(r);
            Console.WriteLine("Дані резюме оновлено успішно!");
        }

        private static void DeleteResume()
        {
            var id = InputHelper.ReadGuid("Введіть Id резюме для видалення: ");
            Program.ResumeService.Delete(id);
            Console.WriteLine("Резюме видалено успішно!");
        }

        private static void SearchResumes()
        {
            var keyword = InputHelper.ReadNonEmptyString("Введіть ключове слово для пошуку: ");
            var results = Program.ResumeService.GetAll()
                .Where(r => r.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (!results.Any())
            {
                Console.WriteLine("Резюме за цим ключовим словом не знайдено.");
                return;
            }

            Console.WriteLine("Результати пошуку:");
            foreach (var r in results)
                Console.WriteLine($"{r.Id} | {r.Title} | Кандидат: {r.UnemployedId} | Досвід: {r.ExperienceYears} років");
        }
    }
}
