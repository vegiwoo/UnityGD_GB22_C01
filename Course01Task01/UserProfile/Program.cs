/*
 * Самарцев Дмитрий Владимирович, домашняя работа 1, задание 1
 * Написать программу «Анкета».
 * Последовательно задаются вопросы (имя, фамилия, возраст, рост, вес).
 * В результате вся информация выводится в одну строчку:
 * а) используя склеивание;
 * б) используя форматированный вывод;
 * в) используя вывод со знаком $.
 */

using System;

namespace UserProfile {
    internal class Program {
        public static void Main(string[] args)
        {
            ConsoleKey key = default;
            do
            {
                Console.Clear();
                Console.WriteLine("Enter: Заполнить новую анкету пациента\nBackspace: Выйти из программы");
                key = Console.ReadKey().Key;
            } while (key != ConsoleKey.Enter && key != ConsoleKey.Backspace);

            if (key == ConsoleKey.Enter)
            {
                CreateNewProfile();

                Console.WriteLine("Для продолжения нажмите любую клавишу...");
                Console.ReadKey();
                Main(new string[] { });
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Спасибо за использование программы.");
            }
        }

        /// <summary>
        /// Создает новый профиль пациента.
        /// </summary>
        private static void CreateNewProfile()
        {
            Console.Clear();
            Console.WriteLine("Новый профиль пациента");
            string firstName = default, lastName = default;
            int age = default;
            float height = default, weight = default;

            while (string.IsNullOrEmpty(firstName))
            {
                Console.Write("Введите имя: ");
                firstName = Console.ReadLine();
            }

            while (string.IsNullOrEmpty(lastName))
            {
                Console.Write("Введите фамилию: ");
                lastName = Console.ReadLine();
            }

            while (age == default)
            {
                Console.Write($"Введите возраст ({Profile.AgeEntryRule}): ");
                if (int.TryParse(Console.ReadLine(), out var ageInput) &&
                    ageInput >= Profile.MinAge &&
                    ageInput <= Profile.MaxAge)
                    age = ageInput;
                else
                    NumberErrorInput(Profile.AgeEntryRule);
            }

            while (height == 0)
            {
                Console.Write($"Введите рост в см ({Profile.HeightEntryRule}): ");
                if (float.TryParse(Console.ReadLine(), out var heightInput) &&
                    heightInput >= Profile.MinHeight &&
                    heightInput <= Profile.MaxHeight)
                    height = heightInput;
                else
                    NumberErrorInput(Profile.HeightEntryRule);
            }

            while (weight == 0)
            {
                Console.Write($"Введите вес в кг ({Profile.WeightEntryRule}): ");
                if (float.TryParse(Console.ReadLine(), out var weightInput) &&
                    weightInput >= Profile.MinWeight &&
                    weightInput <= Profile.MaxWeight)
                    weight = weightInput;
                else
                    NumberErrorInput(Profile.WeightEntryRule);
            }

            _ = new Profile(firstName, lastName, age, height, weight);
        }

        /// <summary>
        /// Метод распечатки сообщения об ошибке.
        /// </summary>
        /// <param name="rule">Правило, применяемое для текущего ввода.</param>
        private static void NumberErrorInput(string rule)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Введите корректное значение: {rule}");
            Console.ForegroundColor = ConsoleColor.Black;
        }
    }
}