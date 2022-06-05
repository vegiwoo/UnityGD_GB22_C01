/* Самарцев Дмитрий Владимирович, домашняя работа 1, задание 5
* а) Написать программу, которая выводит на экран ваше имя, фамилию и город проживания.
* б) Сделать задание, только вывод организовать в центре экрана.
* в) *Сделать задание б с использованием собственных методов (например, Print(string ms, int x,int y).
 */
using System;

namespace Presentation {
    /// <summary>
    /// Класс, представляющий данные текущего студента.
    /// </summary>
    public static class Bio {
        public static string FirstName { get; } = "Дмитрий";
        public static string LastName { get; } = "Самарцев";
        public static string City { get; } = "Сочи";

        public static void Representation(int x, int y)
        {
            Console.Clear();
            Console.SetCursorPosition(x, y);
            Console.WriteLine($"Имя: {FirstName}, Фамилия: {LastName}, Город: {City}.");
        }
    }

    internal class Program {
        public static void Main(string[] args)
        {
            ConsoleKey key;
            do
            {
                Console.Clear();
                Console.WriteLine("Q: Вывести данные студента\n" +
                                  "W: Вывести данные студента в центре экрана\n" +
                                  "E: Вывести данные студента из метода класса\n" +
                                  "Backspace: Выйти из программы");
                key = Console.ReadKey().Key;
            } while (key != ConsoleKey.Q && key != ConsoleKey.W && key != ConsoleKey.E && key != ConsoleKey.Backspace);

            if (key == ConsoleKey.Q)
            {
                Console.Clear();
                Console.WriteLine($"Имя: {Bio.FirstName}, Фамилия: {Bio.LastName}, Город: {Bio.City}.");
                GoBack();
            } else if (key == ConsoleKey.W)
            {
                Console.Clear();
                Console.SetCursorPosition(Console.WindowWidth / 2, Console.WindowHeight / 2);
                Console.WriteLine($"Имя: {Bio.FirstName}, Фамилия: {Bio.LastName}, Город: {Bio.City}.");
                GoBack();
            } else if (key == ConsoleKey.E)
            {
                int x = Console.WindowWidth / 2;
                int y = Console.WindowHeight / 2;
                Bio.Representation(x: x, y: y);
                GoBack();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Спасибо за использование программы.");
            }
        }

        private static void GoBack()
        {
            Console.WriteLine("Для продолжения нажмите любую клавишу...");
            Console.ReadKey();
            Main(new string[] { });
        }
    }
    
}