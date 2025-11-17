using System;
using System.Linq;
using PL.Helper;
using BLL.Models;
using BLL.Dependency;  

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
                        case "1": ListResumes(); break;
                        case "2": AddResume(); break;
                        case "3": EditResume(); break;
                        case "4": DeleteResume(); break;
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

        private static void ListResumes()
        {
            var list = Program.ResumeService.GetAll();
            foreach (var r in list)
                Console.WriteLine($"{r.Id} | {r.Title} | Категорія: {r.Category} | Досвід: {r.ExperienceYears} років | Кандидат: {r.UnemployedId}");
        }

        private static void AddResume()
        {
            var title = InputHelper.ReadNonEmptyString("Назва резюме: ");
            var unemployedId = InputHelper.ReadGuid("ID безробітного: ");
            var experience = InputHelper.ReadInt("Досвід (років): ");

            var resume = new ResumeModel
            {
                Id = Guid.NewGuid(),
                Title = title,
                UnemployedId = unemployedId,
                ExperienceYears = experience
            };

            Program.ResumeService.Add(resume);
            Console.WriteLine("Резюме додано!");
        }

        private static void EditResume()
        {
            var id = InputHelper.ReadGuid("ID резюме для редагування: ");
            var r = Program.ResumeService.GetById(id);

            r.Title = InputHelper.ReadNonEmptyString($"Назва ({r.Title}): ", r.Title);
            r.ExperienceYears = InputHelper.ReadInt($"Досвід ({r.ExperienceYears}): ", r.ExperienceYears);

            Program.ResumeService.Update(r);
            Console.WriteLine("Дані оновлено!");
        }

        private static void DeleteResume()
        {
            var id = InputHelper.ReadGuid("ID резюме для видалення: ");
            Program.ResumeService.Delete(id);
            Console.WriteLine("Резюме видалено!");
        }

        private static void AddCategory()
        {
            var id = InputHelper.ReadGuid("ID резюме: ");
            var cat = InputHelper.ReadNonEmptyString("Нова категорія: ");
            Program.ResumeService.AddCategory(id, cat);
            Console.WriteLine("Категорію додано!");
        }

        private static void RemoveCategory()
        {
            var id = InputHelper.ReadGuid("ID резюме: ");
            Program.ResumeService.RemoveCategory(id);
            Console.WriteLine("Категорію видалено!");
        }

        private static void SortByTitle()
        {
            var list = Program.ResumeService.GetSortedByTitle();
            foreach (var r in list)
                Console.WriteLine($"{r.Id} | {r.Title} | {r.Category}");
        }

        private static void SortByCategory()
        {
            var list = Program.ResumeService.GetSortedByCategory();
            foreach (var r in list)
                Console.WriteLine($"{r.Id} | {r.Title} | {r.Category}");
        }
    }
}
