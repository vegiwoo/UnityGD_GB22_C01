using System;
using System.Collections.Generic;
using System.Linq;
using Course01Lib;

namespace Homework {
    internal class Program {
        
        /// <summary>
        /// Словарь, содержащий задачи домашней работы.
        /// </summary>
        private static readonly Dictionary<ConsoleKeyInfo, Task> Programs = new Dictionary<ConsoleKeyInfo, Task>
        {
            {
                new ConsoleKeyInfo('1', ConsoleKey.D1, false,false,false), 
                new Task(1, 2, 1, 
                "Минимальное из трёх чисел",
                "Введите три целых числа через пробел", 
                "получите минимальное их них")
            },
            {
                new ConsoleKeyInfo('2', ConsoleKey.D2, false,false,false), 
                new Task(1,2,2,
                "Количество цифр в числе", 
                "Введите одно целое число", 
                "получите количество цифр в числе")
            },
            {
                new ConsoleKeyInfo('3', ConsoleKey.D3, false,false,false), 
                new Task(1,2,3,
                "Сумма нечётных положительных чисел", 
                "Введите несколько целых чисел через Enter, завершите ввод цифрой 0\n",
                "получите сумму всех нечётных положительных чисел.")
            },
            {
                new ConsoleKeyInfo('4', ConsoleKey.D4, false,false,false),
                new Task(1,2,4, 
                "Логин и пароль", 
                "Введите логин и пароль пользователя через Enter", 
                "за три попытки постарайтесь взломать Geekbrains")},
            { 
                new ConsoleKeyInfo('5', ConsoleKey.D5, false,false,false),
                new Task(1,2,5,"Индекс массы тела, v.2.0", 
                "Введите свой рост (в метрах, число с запятой) и вес (в кг, целое число) через пробел", 
                "получите рекомендацию по питанию.")
            },
            {
                new ConsoleKeyInfo('6', ConsoleKey.D6, false,false,false),
                new Task(1,2,6, "Хорошие числа",
                "Получите количество 'хороших' чисел в заданном диапазоне ")
            },
            {
                new ConsoleKeyInfo('7', ConsoleKey.D7, false,false,false),
                new Task(1,2,7, "Диапазон и сумма чисел (рекурсия)", 
                "Введите целые числа а и b (a < b) через Enter", 
                "получите диапазон и сумму чисел диапазона между ними.")
            }
        };

        public static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Clear();
            
            (ConsoleKeyInfo info, Task task) choice;
            
            do
            {
                choice = ChoiceTask();
                Console.Clear();
                
                switch (choice.info.Key)
                {
                    case ConsoleKey.D1:
                        GetMinOfThreeNumbers(choice.task);
                        break;
                    case ConsoleKey.D2:
                        GetNumberDigitsInNumber(choice.task);
                        break;
                    case ConsoleKey.D3:
                        GetSumOddPositiveNumbers(choice.task);
                        break;
                    case ConsoleKey.D4:
                        TryingToLoginGb(choice.task);
                        break;
                    case ConsoleKey.D5:
                        CalculateBodyMassIndex(choice.task);
                        break;
                    case ConsoleKey.D6:
                        var rangeTask06 = Enumerable.Range(1, 1_000_000_000);
                        ShowGoodNumbers(choice.task, rangeTask06);
                        break;
                    case ConsoleKey.D7:
                        GiveMeRecursion(choice.task);
                        break;
                }
            } while (choice.info.Key != ConsoleKey.Backspace);
            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Спасибо за использование приложения!");
        }

        #region Task solving methods
        /// <summary>
        /// Принимает от пользователя два целых числа и определяет минимальное из них.
        /// </summary>
        /// <param name="task">Задача.</param>
        private static void GetMinOfThreeNumbers(Task task)
        {
            List<int> values = new List<int>(3);
            
            while (values.Count < 3)
            {
                values.Clear();
                Console.WriteLine($"{task.Description()}");
                string input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                {
                    task.ErrorDescription(); continue;
                }

                string[] substrings = input.TrimEnd().Split(' ');

                if (substrings.Length != 3)
                {
                    task.ErrorDescription(); continue;
                }
                
                foreach (string item in substrings) 
                    if (int.TryParse(item, out var a)) 
                        values.Add(a);
            }

            int minValue = values[0] < values[1] && values[0] < values[2] ? values[0] :
                values[1] < values[2] ? values[1] : values[2];

            Console.WriteLine($"Минимальное число {minValue}.");
            
            ConsoleKey key = InputHelper.WaitUserAction();
            switch (key)
            {
                case ConsoleKey.Backspace: return;
                case ConsoleKey.Spacebar: GetMinOfThreeNumbers(task); break;
            }
        }
        /// <summary>
        /// Рассчитывает количество цифр в числе.
        /// </summary>
        /// <param name="task">Задача.</param>
        private static void GetNumberDigitsInNumber(Task task)
        {
            int inputNumber = default, digitCount = default;

            while (digitCount == 0)
            {
                Console.WriteLine($"{task.Description()}");
                string inputString = Console.ReadLine();

                if (string.IsNullOrEmpty(inputString) || !int.TryParse(inputString, out inputNumber))
                {
                    inputNumber = digitCount = default;
                    task.ErrorDescription(); continue;
                }

                digitCount = GetDigitCount(inputNumber);
            }
            
            Console.WriteLine($"Количество цифр в числе {inputNumber}: {digitCount}");

            ConsoleKey key = InputHelper.WaitUserAction();
            switch (key)
            {
                case ConsoleKey.Backspace: return;
                case ConsoleKey.Spacebar: GetNumberDigitsInNumber(task); break;
            }
        }

        /// <summary>
        /// Рассчитывает сумму нечётных положительных чисел.
        /// </summary>
        /// <param name="task">Задача.</param>
        private static void GetSumOddPositiveNumbers(Task task)
        {
            int sum = 0;
            Console.WriteLine($"{task.Description()}");

            while (true)
            {
                Console.Write("Введите целое число или 0 для расчёта: ");
                
                string input = Console.ReadLine();

                if (string.IsNullOrEmpty(input) || !int.TryParse(input, out var inputNumber))
                {
                    task.ErrorDescription(); continue;
                }
                
                if (Math.Sign(inputNumber) == 0) break;

                if (inputNumber % 2 != 0 && Math.Sign(inputNumber) == 1)
                {
                    sum += inputNumber;
                }
            }
            
            Console.Write($"Сумма введённых нечётных положительных чисел: {sum}\n");
            
            ConsoleKey key = InputHelper.WaitUserAction();
            switch (key)
            {
                case ConsoleKey.Backspace: return;
                case ConsoleKey.Spacebar: GetSumOddPositiveNumbers(task); break;
            }
        }

        /// <summary>
        /// Проверяет введённые логин и пароль на корректность.
        /// </summary>
        /// <param name="task">Задача.</param>
        private static void TryingToLoginGb(Task task) 
        {
            Console.WriteLine($"{task.Description()}");

            bool success;
            int numberAttempts = 3;

            do
            {
                Console.Write($"Осталось попыток: {numberAttempts}\n");
                Console.Write("Введите логин: ");
                string login = Console.ReadLine();
                Console.Write("Введите пароль: ");
                string password = Console.ReadLine();

                success = CheckLoginAndPassword(login, password);
                if (success) break;

                Console.WriteLine("Неверно, попробуй еще раз!");
                numberAttempts--;
            } while (numberAttempts != 0);

            Console.ForegroundColor = success ? ConsoleColor.Green : ConsoleColor.Red;
            Console.WriteLine(success ? 
                $"Вам удалось взломать GeekBrains и получить пожизненный доступ ко всем курсам!" : 
                $"Вам не удалось взломать GeekBrains и за Вами уже выехали!");
            Console.ForegroundColor = ConsoleColor.Black;

            ConsoleKey key = InputHelper.WaitUserAction();
            switch (key)
            {
                case ConsoleKey.Backspace: return;
                case ConsoleKey.Spacebar: TryingToLoginGb(task); break;
            }
        }

        /// <summary>
        /// Вычисляет индекс массы тела и даёт рекомендации по коррекции.
        /// </summary>
        /// <param name="task"></param>
        private static void CalculateBodyMassIndex(Task task)
        {
            Console.Clear();
            Console.WriteLine($"{task.Description()}");

            Bmi bmi = default;
            string input;

            while (bmi == null)
            {
                input = Console.ReadLine();
                
                if (string.IsNullOrEmpty(input))
                {
                    task.ErrorDescription(); continue;
                }

                string[] substrings = input.Split(' ');
                if (substrings.Length != 2)
                {
                    task.ErrorDescription(); continue;
                }

                if (double.TryParse(substrings[0], out var height) &&
                    double.TryParse(substrings[1], out var weight))
                {
                    bmi = new Bmi(height, weight);
                }
                else
                {
                    task.ErrorDescription();
                }
            }
            
            Console.WriteLine($"Индекс массы тела: {bmi.Value} ({bmi.Description})");
            Console.WriteLine($"{bmi.Recomendation}");

            ConsoleKey key = InputHelper.WaitUserAction();
            switch (key)
            {
                case ConsoleKey.Backspace: return;
                case ConsoleKey.Spacebar: CalculateBodyMassIndex(task); break;
            }
        }

        /// <summary>
        /// Показывает "хорошие" числа в заданном диапазоне.
        /// </summary>
        /// <param name="task">Задача.</param>
        /// <param name="range">Заданный диапазон значений.</param>
        /// <remarks>«Хорошим» называется число, которое делится на сумму своих цифр.</remarks>>
        private static void ShowGoodNumbers(Task task, IEnumerable<int> range)
        {
            var numbersList = range.ToList();
            Console.WriteLine($"{task.Description()} от {numbersList.First()} до {numbersList.Last()}");
            Console.WriteLine("Выполняю...");
            
            int goodNumbers = default;
            DateTime start = DateTime.Now;
            
            foreach (int number in numbersList)
            {
                int numberCount = GetDigitCount(number);
                if (number % numberCount != 0) continue;
                goodNumbers++;
            }
            
            Console.WriteLine($"В диапазоне между {numbersList.First()} и {numbersList.Last()} 'хороших' чисел: {goodNumbers}.");
            
            TimeSpan sp = DateTime.Now - start;
            Console.WriteLine($"Программа выполнена за {sp.Seconds} сек.");
            // Но по-хорошему есть Stopwatch :)
            
            ConsoleKey key = InputHelper.WaitUserAction();
            switch (key)
            {
                case ConsoleKey.Backspace: return;
                case ConsoleKey.Spacebar: ShowGoodNumbers(task, range); break;
            }
        }
        
        /// <summary>
        /// Распечатывает числа из заданного диапазона от а до b, где a меньше b, и выводит их сумму
        /// </summary>
        /// <param name="task">Задача</param>
        /// <remarks>Используются рекурсивные методы.</remarks>>
        private static void GiveMeRecursion(Task task)
        {
            Console.WriteLine(task.Description());
            List<int> range = default;

            while (range == null)
            {
                Console.Write("Введите первое целое число: ");
                if (!int.TryParse(Console.ReadLine(), out var firstNumber))
                {
                    task.ErrorDescription(); continue;
                }

                Console.Write("Введите второе целое число: ");
                if (!int.TryParse(Console.ReadLine(), out var secondNumber))
                {
                    task.ErrorDescription();  continue;
                }

                int min = Math.Min(firstNumber, secondNumber);
                int max = Math.Max(firstNumber, secondNumber);
                int capacity = max - min + 1;
                range = Enumerable.Range(min, capacity).ToList();
            }

            Console.WriteLine("Полученный диапазон:");
            RecursivePrint(range, 0);
            Console.WriteLine();
            RecursiveSum(range,0,0);
            
            ConsoleKey key = InputHelper.WaitUserAction();
            switch (key)
            {
                case ConsoleKey.Backspace: return;
                case ConsoleKey.Spacebar: GiveMeRecursion(task); break;
            }
        }
        #endregion

        #region  Helper Methods
        /// <summary>
        /// Выбирает задачу для выполнения.
        /// </summary>
        /// <returns>Ключ нажатой клавиши и выбранная задача.</returns>
        private static (ConsoleKeyInfo info, Task task) ChoiceTask()
        {
            ConsoleKeyInfo key;
            do
            {
                Student.GetInstance().Description();
                Console.WriteLine("Выберите номер задачи:");
                foreach (var item in Programs)
                {
                    Console.WriteLine($"{item.Key.KeyChar}: {item.Value.Name}");
                }
                Console.WriteLine("Или нажмите Backspace для выхода из программы");
                key = Console.ReadKey();
            } while (!Programs.ContainsKey(key) && key.Key != ConsoleKey.Backspace);

            Task task = Programs
                .Where(pr => pr.Key == key)
                .Select(pr => pr.Value)
                .FirstOrDefault();
            
            return (key, task);
        }

        /// <summary>
        /// Проверяет корректность логина и пароля от GB.
        /// </summary>
        /// <param name="login">Представленный логин.</param>
        /// <param name="password">Представленный пароль.</param>
        /// <returns>Флаг поверки.</returns>
        private static bool CheckLoginAndPassword(string login, string password)
        {
            return login == "root" && password == "GeekBrains";
        }
        
        /// <summary>
        /// Возвращает количество чисел в заданном числе.
        /// </summary>
        /// <param name="number">Заданное число.</param>
        /// <returns>Количество цифр.</returns>
        private static int GetDigitCount(int number)
        {
            return (int) Math.Log10(Math.Abs(number)) + 1;
        }

        /// <summary>
        /// Распечатывает числа из заданного диапазона от а до b, где a меньше b
        /// </summary>
        /// <param name="range">Заданный диапазон.</param>
        /// <param name="currentIndex">Текущий индекс проверки.</param>
        /// <remarks>Универсальный метод.</remarks>>
        private static void RecursivePrint<T>(List<T> range, int currentIndex)
        {
            // Базовый случай 
            if(range.Count - 1 == currentIndex) {
                Console.Write($"{range[currentIndex]}"); return;
            }

            // Рекурсивный случай 
            Console.Write($"{range[currentIndex]} ");
            RecursivePrint(range, currentIndex + 1);
        }

        /// <summary>
        /// Получает сумму из заданного диапазона чисел от а до b, где a меньше b
        /// </summary>
        /// <param name="range">Заданный диапазон.</param>
        /// <param name="currentIndex">Текущий индекс проверки.</param>
        /// <param name="currentSum">Текущая сумма.</param>
        private static void RecursiveSum(List<int> range, int currentIndex, int currentSum)
        {
            if(range.Count - 1 == currentIndex) {
                // Базовый случай 
                Console.WriteLine($"Сумма элементов диапазона равна: {range[currentIndex] + currentSum}");
            }
            else
            {
                // Рекурсивный случай 
                RecursiveSum(range, currentIndex + 1, currentSum += range[currentIndex]);
            }
        }
        #endregion
    }
}