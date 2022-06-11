using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Task03Lib;
using Task03Lib.Numbers;

namespace Course01Task03 {
    
    internal class Program {
        
        static readonly Dictionary<ConsoleKeyInfo, Task> Tasks = new Dictionary<ConsoleKeyInfo, Task>
        {
            {
                new ConsoleKeyInfo('1', ConsoleKey.D1, false,false,false), 
                new Task(1,3,1, 
                    "Работа с комплексными числами", 
                    "Введите данные для 2-х комплексных чисел (4 положительных или отрицательных числа в одну строку)\n", 
                    "получите результаты действия над комплексными числами:")
            },
            {
                new ConsoleKeyInfo('2', ConsoleKey.D2, false,false,false), 
                new Task(1,3,2, 
                    "Сумма нечётных положительных чисел v2.0", 
                    "Введите несколько целых чисел через Enter, завершите ввод цифрой 0\n", 
                    "получите сумму всех нечётных положительных чисел:")
            },
            {
                new ConsoleKeyInfo('3', ConsoleKey.D3, false,false,false), 
                new Task(1,3,3, 
                    "Работа с дробями", 
                    "Введите данные для 2-х дробей (4 целых положительных числа в одну строку)", 
                    "получите результаты действия дробями:")
            }
        }; 
       
        public static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Black;
            (ConsoleKey key, Task? task) choice;

            do
            {
                choice = ChoiceTask();
                Console.Clear();
                
                if (choice.task != null)
                {
                    switch (choice.key)
                    {
                        case ConsoleKey.D1:
                            WorkingWithComplexNumbers(choice.task);
                            break;
                        case ConsoleKey.D2:
                            GetSumOddPositiveNumbers(choice.task);
                            break;
                        case ConsoleKey.D3:
                            WorkingWithFractNumbers(choice.task);
                            break;
                    }
                }
            } while (choice.key != ConsoleKey.Backspace);
            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Спасибо за использование приложения.");
        }

        #region Task solving methods
        /// <summary>
        /// Работает с комплексными числами.
        /// </summary>
        /// <param name="task">Задача.</param>
        private static void WorkingWithComplexNumbers(Task task)
        {
            Console.WriteLine(task.TaskDescription());
            Console.WriteLine();
            List<float> items = new List<float>(4);

            while (items.Count != 4)
            {
                Console.WriteLine("Введите действительные и мнимые части 2-х чисел в формате 0,00 через пробел + Enter:");

                var numbersString = Console.ReadLine();
                if (string.IsNullOrEmpty(numbersString))
                {
                    task.ErrorDescription(); items.Clear(); continue;
                }

                var numbersStrings = numbersString.Trim().Split(' ');
                if (numbersStrings.Length != 4)
                {
                    task.ErrorDescription(); items.Clear(); continue;
                }

                foreach (var item in numbersStrings)
                {
                    if (!float.TryParse(item, out var number))
                    {
                        task.ErrorDescription(); items.Clear(); continue;
                    }
                    
                    items.Add(number);
                }
            }

            IComplexable c1 = new ComplexStruct(items[0], items[1]);
            IComplexable c2 = new ComplexClass(items[2], items[3]);

            Console.WriteLine($"Вы ввели два комплексных числа: {c1} и {c2}");
            
            ConsoleKey processKey;
            do
            {
                Console.WriteLine($"Выберите действие над числами:\n" +
                                  $"1: Сложение\n" +
                                  $"2: Вычитание\n" +
                                  $"3: Умножение\n" +
                                  $"Backspace: Выход");

                processKey = Console.ReadKey().Key;

                switch (processKey)
                {
                    case ConsoleKey.D1:
                        Console.Clear();
                        Console.WriteLine($"{c1} + {c2} = {c1.Addition(c1, c2)}");
                        Thread.Sleep(500);
                        break;
                    case ConsoleKey.D2:
                        Console.Clear();
                        Console.WriteLine($"{c1} - {c2} = {c2.Subtraction(c1, c2)}");
                        Thread.Sleep(500);
                        break;
                    case ConsoleKey.D3:
                        Console.Clear();
                        Console.WriteLine($"{c1} x {c2} = {c1.Multiplication(c1, c2)}");
                        Thread.Sleep(500);
                        break;
                    case ConsoleKey.Backspace:
                        break;
                }
               
            } while (processKey != ConsoleKey.Backspace);
            
            ConsoleKey key = InputHelper.WaitUserAction();
            switch (key)
            {
                case ConsoleKey.Backspace: return;
                case ConsoleKey.Spacebar: WorkingWithComplexNumbers(task); break;
            }
        }

        /// <summary>
        /// Рассчитывает сумму нечётных положительных чисел.
        /// </summary>
        /// <param name="task">Задача.</param>
        private static void GetSumOddPositiveNumbers(Task task)
        {
            int sum = 0;
            Console.WriteLine($"{task.TaskDescription()}");

            while (true)
            {
                Console.Write("Введите целое число или 0 для проведения расчёта: ");
                
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
        /// Работает с дробями.
        /// </summary>
        /// <param name="task">Задача.</param>
        private static void WorkingWithFractNumbers(Task task)
        {
            Console.WriteLine(task.TaskDescription());
            Console.WriteLine();
            List<int> items = new List<int>(4);
            
            while (items.Count != 4)
            {
                Console.WriteLine("Введите 4 целых числа через пробел и нажмите Enter:");

                var numbersString = Console.ReadLine();
                if (string.IsNullOrEmpty(numbersString))
                {
                    task.ErrorDescription(); items.Clear(); continue;
                }

                var numbersStrings = numbersString.Trim().Split(' ');
                if (numbersStrings.Length != 4)
                {
                    task.ErrorDescription(); items.Clear(); continue;
                }

                foreach (var item in numbersStrings)
                {
                    if (!int.TryParse(item, out var number))
                    {
                        task.ErrorDescription(); items.Clear(); continue;
                    }
                    
                    items.Add(number);
                }
            }

            if (items[1] == 0 || items[3] == 0)
                throw new ArgumentException("Знаменатель дроби не может быть равен 0");
            
            Fraction f1 = new Fraction(items[0], items[1]);
            Fraction f2 = new Fraction(items[2], items[3]);

            Console.WriteLine($"Вы ввели две дроби: {f1} и {f2}");

            ConsoleKey processKey;
            do
            {
                Console.WriteLine($"Выберите действие над дробями:\n" +
                                  $"1: Сложение\n" +
                                  $"2: Вычитание\n" +
                                  $"3: Умножение\n" +
                                  $"4: Деление\n" +
                                  $"5: Десятичные дроби\n" +
                                  $"Backspace: Выход");

                processKey = Console.ReadKey().Key;

                switch (processKey)
                {
                    case ConsoleKey.D1:
                        Console.Clear();
                        Console.WriteLine("Выполняю операцию сложения...");
                        Console.WriteLine($"{f1} + {f2} = {Fraction.Addition(f1, f2)}");
                        Thread.Sleep(500);
                        break;
                    case ConsoleKey.D2:
                        Console.Clear();
                        Console.WriteLine("Выполняю операцию вычитания...");
                        Console.WriteLine($"{f1} - {f2} = {Fraction.Subtraction(f1, f2)}");
                        Thread.Sleep(500);
                        break;
                    case ConsoleKey.D3:
                        Console.Clear();
                        Console.WriteLine("Выполняю операцию умножения...");
                        Console.WriteLine($"{f1} x {f2} = {Fraction.Multiplication(f1, f2)}");
                        Thread.Sleep(500);
                        break;
                    case ConsoleKey.D4:
                        Console.Clear();
                        Console.WriteLine("Выполняю операцию деления...");
                        Console.WriteLine($"{f1} / {f2} = {Fraction.Division(f1, f2)}\n");
                        Thread.Sleep(500);
                        break;
                    case ConsoleKey.D5:
                        Console.Clear();
                        Console.WriteLine("Привожу к десятичным дробям...");
                        
                        Console.WriteLine($"Десятичные дроби: " +
                                          $"{f1.DecFraction}, {f2.DecFraction}\n");
                        Thread.Sleep(500);
                        break;
                    case ConsoleKey.Backspace:
                        break;
                }
               
            } while (processKey != ConsoleKey.Backspace);
            
            ConsoleKey key = InputHelper.WaitUserAction();
            switch (key)
            {
                case ConsoleKey.Backspace: return;
                case ConsoleKey.Spacebar: WorkingWithComplexNumbers(task); break;
            }
        }
        #endregion

        #region Helper Methods
        /// <summary>
        /// Выбирает задачу для выполнения.
        /// </summary>
        /// <returns>Ключ нажатой клавиши и задача</returns>
        private static (ConsoleKey key, Task? task) ChoiceTask()
        {
            ConsoleKey selectedKey;

            do 
            {
                Console.Clear();
                Student.GetInstance().Description();
                Console.WriteLine("Выберите номер задачи:");
                
                foreach (var task in Tasks)
                {
                    Console.WriteLine($"{task.Value.TaskNumber}: {task.Value.Name}");
                }
                Console.WriteLine("Или нажмите Backspace для выхода из программы");
                selectedKey = Console.ReadKey().Key;
            } while (selectedKey != ConsoleKey.D1 && 
                     selectedKey != ConsoleKey.D2 && 
                     selectedKey != ConsoleKey.D3 && 
                     selectedKey != ConsoleKey.Backspace);

            var selectedTask = Tasks
                .FirstOrDefault(el => el.Key.Key == selectedKey);
            
            return (selectedKey, selectedTask.Value);
        }
        #endregion
    }
}