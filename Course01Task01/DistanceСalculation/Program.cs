/* Самарцев Дмитрий Владимирович, домашняя работа 1, задание 3
* а) Написать программу, которая подсчитывает расстояние между точками с координатами x1, y1 и x2,y2 по формуле
 * r=Math.Sqrt(Math.Pow(x2-x1,2)+Math.Pow(y2-y1,2).
 * Вывести результат, используя спецификатор формата .2f (с двумя знаками после запятой);
 * *Выполнить предыдущее задание, оформив вычисления расстояния между точками в виде метода.
 */

using System;

namespace DistanceСalculation {
    
    internal class Program {
        
        private static readonly string InputRule = $"два целых числа через пробел";
        
        public static void Main(string[] args)
        {
            ConsoleKey key;
            do
            {
                Console.Clear();
                Console.WriteLine("Enter: Рассчитать расстояние между точками\nBackspace: Выйти из программы");
                key = Console.ReadKey().Key;
            } while (key != ConsoleKey.Enter && key != ConsoleKey.Backspace);

            if (key == ConsoleKey.Enter)
            {
                Console.Clear();

                Point? point01 = default;
                Point? point02 = default;
                
                while (point01 == null)
                {
                    Console.Write($"Введите координаты x и y первой точки ({InputRule}): ");
                    string inputString = Console.ReadLine();
                    string[] inputStrings = inputString.Split(' ');

                    if (inputStrings.Length == 2 &&
                        int.TryParse(inputStrings[0], out var x1Input) && 
                        int.TryParse(inputStrings[1], out var y1Input))
                    {
                        point01 = new Point(x1Input, y1Input);
                    }
                    else
                    {
                        NumberErrorInput(InputRule);
                    }
                }
                
                while (point02 == null)
                {
                    Console.Write($"Введите координаты x и y второй точки ({InputRule}): ");
                    string inputString = Console.ReadLine();
                    string[] inputStrings = inputString.Split(' ');

                    if (inputStrings.Length == 2 &&
                        int.TryParse(inputStrings[0], out var x2Input) && 
                        int.TryParse(inputStrings[1], out var y2Input))
                    {
                        point02 = new Point(x2Input, y2Input);
                    }
                    else
                    {
                        NumberErrorInput(InputRule);
                    }
                }

                Double distance = CalculateDistance(point01.GetValueOrDefault(), point02.GetValueOrDefault());
                Console.WriteLine($"Дистанция между точками - {distance:F2}.");
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

        /// <summary>
        /// Рассчитывает расстояние между двумя точками.
        /// </summary>
        /// <param name="point1">Первая точка</param>
        /// <param name="point2">Вторая точка</param>
        /// <returns>Расстояние между точками.</returns>
        private static double CalculateDistance(Point point1, Point point2)
        {
            return Math.Sqrt(Math.Pow(point2.X - point1.X, 2) + Math.Pow(point2.Y - point1.Y, 2));
        }
    }
}