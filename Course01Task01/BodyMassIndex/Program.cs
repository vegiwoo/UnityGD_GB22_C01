/* Самарцев Дмитрий Владимирович, домашняя работа 1, задание 2
 * Ввести вес и рост человека. Рассчитать и вывести индекс массы тела
 * (ИМТ) по формуле I=m/(h*h); где m — масса тела в килограммах, h — рост в метрах.
 */

using System;
using System.Collections.Generic;
using System.Linq;

namespace BodyMassIndex {
    internal class Program {
        
        private const float MinWeight = 40.00f;
        private const float MaxWeight = 250.00f;
        private static readonly string WeightRule = $"целое или число, разделенное ',' - от {MinWeight} до {MaxWeight}";

        private const float MinHeight = 0.50f;
        private const float MaxHeight = 2.00f;
        private static readonly string HeightRule = $"целое или число, разделенное ',' - от {MinHeight} до {MaxHeight}";
        
        /// <summary>
        /// Рассчитывает индекс массы тела.
        /// </summary>
        /// <param name="weight">Вес в килограммах</param>
        /// <param name="height">Рост в метрах</param>
        /// <returns>Индекс массы тела и описание результата.</returns>
        private static (float bmi, string desc) CalculateBodyMassIndex(float weight, float height  )
        {
            float bmi = weight / (height * height);
            string description = default;
           
            if (bmi <= 16.0f)
            {
                description = "Выраженный дефицит массы тела";
            } else if (bmi >= 16.1f & bmi < 18.5f )
            {
                description = "Недостаточная (дефицит) масса тела";
            } else if (bmi >= 18.5f & bmi < 25.0f)
            {
                description = "Нормальная масса тела";
            } else if (bmi >= 25.1f & bmi < 30.0f)
            {
                description = "Избыточная масса тела (предожирение)";
            } else if (bmi >= 30.1 & bmi < 35)
            {
                description = "Ожирение первой степени";
            }
            else if(bmi >= 35.1 & bmi <= 40)
            {
                 description = "Ожирение второй степени";
            } else if (bmi > 40)
            {
                description = "Ожирение третьей степени (морбидное)";
            }

            return (bmi, description);
        }

        public static void Main(string[] args)
        {
            ConsoleKey key;
            do
            {
                Console.Clear();
                Console.WriteLine("Enter: Рассчитать индекс массы тела\nBackspace: Выйти из программы");
                key = Console.ReadKey().Key;
            } while (key != ConsoleKey.Enter && key != ConsoleKey.Backspace);
            
            if (key == ConsoleKey.Enter)
            {
                Console.Clear();

                float weight = default;
                while (weight == 0.00f)
                {
                    Console.Write($"Введите вес в кг ({WeightRule}): ");
                    if (int.TryParse(Console.ReadLine(), out var weightInput) &&
                        weightInput >= MinWeight &&
                        weightInput <= MaxWeight)
                        weight = weightInput;
                    else
                        NumberErrorInput(WeightRule);
                }

                float height = default;
                while (height == 0.00f)
                {
                    Console.Write($"Введите рост в м ({HeightRule}): ");
                    if (float.TryParse(Console.ReadLine(), out var heightInput) &&
                        heightInput >= MinHeight &&
                        heightInput <= MaxHeight)
                        height = heightInput;
                    else
                        NumberErrorInput(HeightRule);
                }

                var bmi = CalculateBodyMassIndex(weight: weight, height: height);
                Console.WriteLine($"Индекс массы тела {bmi.bmi:F2} - {bmi.desc}");
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