using System;
using System.Linq;
using PL.Helper;
using BLL.Models;
using BLL.Dependency;

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
                Console.WriteLine("1. Переглянути всі");
                Console.WriteLine("2. Додати");
                Console.WriteLine("3. Редагувати");
                Console.WriteLine("4. Видалити");
                Console.WriteLine("5. Додати категорію");
                Console.WriteLine("6. Видалити категорію");
                Console.WriteLine("7. Сортувати за назвою");
                Console.WriteLine("8. Сортувати за категорією");
                Console.WriteLine("0. Назад");
                Console.Write("Виберіть дію: ");

                var choice = Console.ReadLine();
                try
                {
                    switch (choice)
                    {
                        case "1": ListAll(); break;
                        case "2": Add(); break;
                        case "3": Edit(); break;
                        case "4": Delete(); break;
                        case "5": AddCategory(); break;
                        case "6": RemoveCategory(); break;
                        case "7": SortByTitle(); break;
                        case "8": SortByCategory(); break;
                        case "0": return;
                        default: Console.WriteLine("Невірний вибір!"); break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Помилка: {ex.Message}");
                }

                Console.WriteLine("Натисніть будь-яку клавішу...");
                Console.ReadKey();
            }
        }

        private static void ListAll()
        {
            var list = Program.VacancyService.GetAll();
            foreach (var v in list)
                Console.WriteLine($"{v.Id} | {v.Title} | Категорія: {v.Category} | Відкрита: {v.IsOpen}");
        }

        private static void Add()
        {
            var title = InputHelper.ReadNonEmptyString("Назва вакансії: ");
            var category = InputHelper.ReadNonEmptyString("Категорія: ");
            var isOpen = InputHelper.ReadBool("Відкрита (true/false): ");

            var v = new VacancyModel
            {
                Id = Guid.NewGuid(),
                Title = title,
                Category = category,
                IsOpen = isOpen
            };

            Program.VacancyService.Add(v);
            Console.WriteLine("Вакансію додано!");
        }

        private static void Edit()
        {
            var id = InputHelper.ReadGuid("ID вакансії: ");
            var v = Program.VacancyService.GetById(id);

            v.Title = InputHelper.ReadNonEmptyString($"Назва ({v.Title}): ", v.Title);
            v.Category = InputHelper.ReadNonEmptyString($"Категорія ({v.Category}): ", v.Category);
            v.IsOpen = InputHelper.ReadBool($"Відкрита ({v.IsOpen}): ", v.IsOpen);

            Program.VacancyService.Update(v);
            Console.WriteLine("Оновлено!");
        }

        private static void Delete()
        {
            var id = InputHelper.ReadGuid("ID вакансії: ");
            Program.VacancyService.Delete(id);
            Console.WriteLine("Видалено!");
        }

        private static void AddCategory()
        {
            var id = InputHelper.ReadGuid("ID вакансії: ");
            var cat = InputHelper.ReadNonEmptyString("Категорія: ");
            Program.VacancyService.AddCategory(id, cat);
            Console.WriteLine("Категорію додано!");
        }

        private static void RemoveCategory()
        {
            var id = InputHelper.ReadGuid("ID вакансії: ");
            Program.VacancyService.RemoveCategory(id);
            Console.WriteLine("Категорію видалено!");
        }

        private static void SortByTitle()
        {
            var list = Program.VacancyService.GetSortedByTitle();
            foreach (var v in list)
                Console.WriteLine($"{v.Id} | {v.Title} | {v.Category}");
        }

        private static void SortByCategory()
        {
            var list = Program.VacancyService.GetSortedByCategory();
            foreach (var v in list)
                Console.WriteLine($"{v.Id} | {v.Title} | {v.Category}");
        }
    }
}
