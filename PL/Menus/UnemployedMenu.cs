using System;
using PL.Helper;
using BLL.Models;
using BLL.Dependency;

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
                Console.WriteLine("1. Переглянути всіх");
                Console.WriteLine("2. Додати");
                Console.WriteLine("3. Редагувати");
                Console.WriteLine("4. Видалити");
                Console.WriteLine("5. Пошук");
                Console.WriteLine("6. Сортувати за ім’ям");
                Console.WriteLine("7. Сортувати за прізвищем");
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
                        case "5": Search(); break;
                        case "6": SortByFirstName(); break;
                        case "7": SortByLastName(); break;
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
            var list = Program.UnemployedService.GetAll();
            foreach (var u in list)
                Console.WriteLine($"{u.Id} | {u.FirstName} {u.LastName} | Email: {u.Email}");
        }

        private static void Add()
        {
            var f = InputHelper.ReadNonEmptyString("Ім’я: ");
            var l = InputHelper.ReadNonEmptyString("Прізвище: ");
            var email = InputHelper.ReadEmail("Email: ");

            var u = new UnemployedModel
            {
                Id = Guid.NewGuid(),
                FirstName = f,
                LastName = l,
                Email = email
            };

            Program.UnemployedService.Add(u);
            Console.WriteLine("Додано!");
        }

        private static void Edit()
        {
            var id = InputHelper.ReadGuid("ID: ");
            var u = Program.UnemployedService.GetById(id);

            u.FirstName = InputHelper.ReadNonEmptyString($"Ім’я ({u.FirstName}): ", u.FirstName);
            u.LastName = InputHelper.ReadNonEmptyString($"Прізвище ({u.LastName}): ", u.LastName);
            u.Email = InputHelper.ReadEmail($"Email ({u.Email}): ", u.Email);

            Program.UnemployedService.Update(u);
            Console.WriteLine("Оновлено!");
        }

        private static void Delete()
        {
            var id = InputHelper.ReadGuid("ID: ");
            Program.UnemployedService.Delete(id);
            Console.WriteLine("Видалено!");
        }

        private static void Search()
        {
            var keyword = InputHelper.ReadNonEmptyString("Ключове слово: ");
            var results = Program.UnemployedService.Search(keyword);

            foreach (var u in results)
                Console.WriteLine($"{u.Id} | {u.FirstName} {u.LastName} | {u.Email}");
        }

        private static void SortByFirstName()
        {
            var list = Program.UnemployedService.GetSortedByFirstName();
            foreach (var u in list)
                Console.WriteLine($"{u.Id} | {u.FirstName} {u.LastName} | {u.Email} | {u.Phone}");
        }

        private static void SortByLastName()
        {
            var list = Program.UnemployedService.GetSortedByLastName();
            foreach (var u in list)
                Console.WriteLine($"{u.Id} | {u.FirstName} {u.LastName} | {u.Email} | {u.Phone}");
        }

    }
}
