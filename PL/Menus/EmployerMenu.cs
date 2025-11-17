using System;
using PL.Helper;
using BLL.Dependency;
using BLL.Models;

namespace PL.Menus
{
    public static class EmployerMenu
    {
        public static void ShowMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Управління роботодавцями ===");
                Console.WriteLine("1. Переглянути всіх");
                Console.WriteLine("2. Додати");
                Console.WriteLine("3. Редагувати");
                Console.WriteLine("4. Видалити");
                Console.WriteLine("5. Сортувати за ім’ям");
                Console.WriteLine("6. Сортувати за прізвищем");
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
                        case "5": SortByFirstname(); break;
                        case "6": SortByLastname(); break;
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

        private static void ListEmployers()
        {
            var list = Program.EmployerService.GetAll();
            foreach (var e in list)
                Console.WriteLine($"{e.Id} | {e.FirstName} {e.LastName} | Email: {e.Email}");
        }

        private static void AddEmployer()
        {
            var firstName = InputHelper.ReadNonEmptyString("Ім’я: ");
            var lastName = InputHelper.ReadNonEmptyString("Прізвище: ");
            var email = InputHelper.ReadEmail("Email: ");

            var emp = new EmployerModel
            {
                Id = Guid.NewGuid(),
                FirstName = firstName,
                LastName = lastName,
                Email = email
            };
            Program.EmployerService.Add(emp);
            Console.WriteLine("Роботодавця додано!");
        }

        private static void EditEmployer()
        {
            var id = InputHelper.ReadGuid("ID для редагування: ");
            var emp = Program.EmployerService.GetById(id);

            emp.FirstName = InputHelper.ReadNonEmptyString($"Ім’я ({emp.FirstName}): ", emp.FirstName);
            emp.LastName = InputHelper.ReadNonEmptyString($"Прізвище ({emp.LastName}): ", emp.LastName);
            emp.Email = InputHelper.ReadEmail($"Email ({emp.Email}): ", emp.Email);

            Program.EmployerService.Update(emp);
            Console.WriteLine("Оновлено!");
        }

        private static void DeleteEmployer()
        {
            var id = InputHelper.ReadGuid("ID для видалення: ");
            Program.EmployerService.Delete(id);
            Console.WriteLine("Видалено!");
        }

        private static void SortByFirstname()
        {
            var list = Program.EmployerService.GetSortedByFirstName();
            foreach (var e in list)
                Console.WriteLine($"{e.Id} | {e.FirstName} {e.LastName} | {e.Email}");
        }

        private static void SortByLastname()
        {
            var list = Program.EmployerService.GetSortedByLastName();
            foreach (var e in list)
                Console.WriteLine($"{e.Id} | {e.FirstName} {e.LastName} | {e.Email}");
        }
    }
}
