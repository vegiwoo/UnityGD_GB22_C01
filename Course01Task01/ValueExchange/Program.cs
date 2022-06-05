/* Самарцев Дмитрий Владимирович, домашняя работа 1, задание 4
 * Написать программу обмена значениями двух переменных типа int
 * без использования вспомогательных методов.
 * а) с использованием третьей переменной;
 * б) *без использования третьей переменной.
 */

using System;

namespace ValueExchange {
    internal class Program {
        
        private static readonly string InputRule = "целое число";
        
    public static void Main(string[] args)
        {
            ConsoleKey key;
            do
            {
                Console.Clear();
                Console.WriteLine("Enter: Обменять значения двух переменных\nBackspace: Выйти из программы");
                key = Console.ReadKey().Key;
            } while (key != ConsoleKey.Enter && key != ConsoleKey.Backspace);

            if (key == ConsoleKey.Enter)
            {
                Console.Clear();
                
                int a = default, b = default;

                while (a == 0)
                {
                    Console.Write($"Введите значение первой переменной {InputRule}: ");
                    if (int.TryParse(Console.ReadLine(), out var aInput))
                    {
                        a = aInput;
                    }
                    else
                    {
                        NumberErrorInput(InputRule);
                    }
                }

                while (b == 0)
                {
                    Console.Write($"Введите значение второй переменной {InputRule}: ");
                    if (int.TryParse(Console.ReadLine(), out var bInput))
                    {
                        b = bInput;
                    }
                    else
                    {
                        NumberErrorInput(InputRule);
                    }
                }

                Console.WriteLine($"Значение переменных: a = {a}, b = {b}");
                
                // Первый способ обмена
                int tmp = a;  a = b;  b = tmp;
                Console.WriteLine($"Значение переменных после обмена (1-й способ): a = {a}, b = {b}");
                
                // Второй способ
                (a, b) = (b, a);
                Console.WriteLine($"Значение переменных после обмена (2-й способ): a = {a}, b = {b}");
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