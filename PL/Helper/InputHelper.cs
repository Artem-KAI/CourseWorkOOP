using System;
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
                if (!string.IsNullOrWhiteSpace(input))
                    return input;
                if (defaultValue != null)
                    return defaultValue;
                Console.WriteLine("Поле не може бути пустим!");
            }
        }

        public static string ReadEmail(string prompt, string? defaultValue = null)
        {
            while (true)
            {
                Console.Write(prompt);
                var email = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(email) && defaultValue != null)
                    return defaultValue;

                if (!string.IsNullOrWhiteSpace(email) &&
                    Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                    return email;

                Console.WriteLine("Невірний формат email!");
            }
        }
        public static int ReadInt(string message, int? defaultValue = null)
        {
            Console.Write(message);
            var input = Console.ReadLine();

            if (int.TryParse(input, out int value))
                return value;

            if (defaultValue.HasValue && string.IsNullOrWhiteSpace(input))
                return defaultValue.Value;

            Console.WriteLine("Введіть ціле число!");
            return ReadInt(message, defaultValue);
        }

        public static bool ReadBool(string message, bool? defaultValue = null)
        {
            Console.Write(message);
            var input = Console.ReadLine();

            if (bool.TryParse(input, out bool value))
                return value;

            if (defaultValue.HasValue && string.IsNullOrWhiteSpace(input))
                return defaultValue.Value;

            Console.WriteLine("Введіть true або false!");
            return ReadBool(message, defaultValue);
        }

        public static Guid ReadGuid(string message)
        {
            Console.Write(message);
            var input = Console.ReadLine();

            if (Guid.TryParse(input, out Guid value))
                return value;

            Console.WriteLine("Введіть коректний GUID!");
            return ReadGuid(message);
        }
    }
}


//using System;
//using System.Text.RegularExpressions;

//namespace PL.Helper
//{
//    public static class InputHelper
//    {
//        public static string ReadNonEmptyString(string prompt, string? defaultValue = null)
//        {
//            while (true)
//            {
//                Console.Write(prompt);
//                var input = Console.ReadLine();
//                if (!string.IsNullOrWhiteSpace(input))
//                    return input;
//                if (defaultValue != null)
//                    return defaultValue;
//                Console.WriteLine("Поле не може бути пустим!");
//            }
//        }

//        public static Guid ReadGuid(string prompt)
//        {
//            while (true)
//            {
//                Console.Write(prompt);
//                if (Guid.TryParse(Console.ReadLine(), out Guid id))
//                    return id;
//                Console.WriteLine("Невірний формат GUID!");
//            }
//        }

//        public static string ReadEmail(string prompt, string? defaultValue = null)
//        {
//            while (true)
//            {
//                Console.Write(prompt);
//                var email = Console.ReadLine();
//                if (string.IsNullOrWhiteSpace(email) && defaultValue != null)
//                    return defaultValue;

//                if (!string.IsNullOrWhiteSpace(email) &&
//                    Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
//                    return email;

//                Console.WriteLine("Невірний формат email!");
//            }
//        }
//    }
//}
