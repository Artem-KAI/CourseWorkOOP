using System.Text.RegularExpressions;

namespace PL.Helper
{
    public static class InputHelper
    {
        public static string ReadNonEmptyString(string prompt, string? defaultValue = null)
        {
            while (true)
            {
                Console.Write(prompt);
                var input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    if (!string.IsNullOrEmpty(defaultValue))
                        return defaultValue;
                    Console.WriteLine("Поле не може бути порожнім!");
                    continue;
                }
                return input;
            }
        }

        public static string ReadEmail(string prompt, string? defaultValue = null)
        {
            var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            while (true)
            {
                Console.Write(prompt);
                var input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    if (!string.IsNullOrEmpty(defaultValue))
                        return defaultValue;
                    Console.WriteLine("Email не може бути порожнім!");
                    continue;
                }
                if (!emailRegex.IsMatch(input))
                {
                    Console.WriteLine("Некоректний формат email!");
                    continue;
                }
                return input;
            }
        }

        public static Guid ReadGuid(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                var input = Console.ReadLine();
                if (Guid.TryParse(input, out var guid))
                    return guid;
                Console.WriteLine("Некоректний формат Id. Повторіть спробу.");
            }
        }
    }
}
