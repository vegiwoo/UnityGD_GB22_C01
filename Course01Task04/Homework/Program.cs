#nullable enable
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Task04Lib;
using Task04Lib.CustomArrays;

namespace Homework {
    internal class Program {
        
        private static readonly Random randomizer = new Random();
        private static string AccountsPath => AppDomain.CurrentDomain.BaseDirectory + "accounts.json";
        
        static readonly Dictionary<ConsoleKeyInfo, Task> Tasks = new Dictionary<ConsoleKeyInfo, Task>
        {
            {
                new ConsoleKeyInfo('1', ConsoleKey.D1, false,false,false), 
                new Task(1,4,1, 
                    "Поиск пар в массиве v1.0 (массив из 20 случайных чисел от -10 000 до 10 000 в методе)",
                    "Введите делитель - целое число, отличное от 0\n", 
                    "и получите количество пар чисел, только одно из которых делится на делитель.")
            },
            {
                new ConsoleKeyInfo('2', ConsoleKey.D2, false,false,false), 
                new Task(1,4,2, 
                    "Поиск пар в массиве v2.0 (массив из 20 случайных чисел от -10 000 до 10 000 в классе)",
                    "Введите делитель - целое число, отличное от 0\n", 
                    "и получите количество пар чисел, только одно из которых делится на делитель.")
            },
            {
                new ConsoleKeyInfo('3', ConsoleKey.D3, false,false,false), 
                new Task(1,4,3, 
                    "Одномерный массив целых чисел (в обертке OneDimIntArray)", 
                    "Презентация работы класса", 
                    "и распечатка сводки по нему")
            },
            {
                new ConsoleKeyInfo('4', ConsoleKey.D4, false,false,false), 
                new Task(1,4,4, 
                    "Вход на GeekBrains, v2.0", 
                    "Введите логин и пароль\n", 
                    "и попробуйте зайти на GeekBrains за несколько попыток.")
            },
            {
                new ConsoleKeyInfo('5', ConsoleKey.D5, false,false,false), 
                new Task(1,4,5, 
                    "Двумерный массив целых чисел (в обертке TwoDimIntArray)", 
                    "Презентация работы класса",
                    "и распечатка сводки по нем")
            }
        }; 
        
        public static void Main(string[] args)
        { 
            // Формирование БД для задачи 4, запись в в файл
            CreateUserDB();
            
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Black;
            Student.GetInstance().Description();
            
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
                            GetPairNumsOneOfWhichIsDivByDivisorV01(choice.task);
                            break;
                        case ConsoleKey.D2:
                            GetPairNumsOneOfWhichIsDivByDivisorV02(choice.task);
                            break;
                        case ConsoleKey.D3:
                            UseOneDimIntArray(choice.task);
                            break;
                        case ConsoleKey.D4:
                            TryingToLoginGb(choice.task);
                            break;
                        case ConsoleKey.D5:
                            UseTwoDimIntArray(choice.task);
                            break;
                    }
                }
            } while (choice.key != ConsoleKey.Backspace);
            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Спасибо за использование приложения.");
        }
        
        #region Task solving methods
        /// <summary>
        /// Возвращает пары чисел в массиве из которых только одно делится на указанны делитель.
        /// </summary>
        /// <param name="task">Задача.</param>
        /// <exception cref="DivideByZeroException">Исключение - деление на 0</exception>
        /// <remarks>Используется метод.</remarks>>
        private static void GetPairNumsOneOfWhichIsDivByDivisorV01(Task task)
        {
            Console.Clear();
            task.TaskDescription();

            var array = MakeIntArray(-10_000, 10_000, 20);
            Console.WriteLine("Сгенерирован массив:");
            Print(array);
            
            int divider;
            while (true)
            {
                Console.Write("Введите делитель: ");
                try
                {
                    var input = Console.ReadLine();
                    divider = int.Parse(input);
                    if (divider == 0)
                        throw new DivideByZeroException("На 0 делить нельзя, повторите");
                    break;
                }
                catch (FormatException e)
                {
                    task.ErrorDescription($"{e.Message}\nНекорректный формат целого числа, повторите");
                }
                catch (DivideByZeroException divideByZero)
                {
                    task.ErrorDescription(divideByZero.Message);
                }
            }

            var pairs = new List<(int f, int s)>(5);
            int i = default, count = default;
            while (i + 1 != array.Length)
            {
                if (array[i] % divider == 0 && array[i + 1] % divider != 0)
                {
                    pairs.Add((array[i], array[i + 1]));
                    count++;
                }
                i++;
            }
            
            Console.WriteLine($"В массиве {count} пар чисел, одно из которых делится на {divider}");
            if (pairs.Count > 0)
            {
                var last = pairs.Last();
                foreach (var pair in pairs)
                {
                    Console.Write(pair == last ? 
                        $"{pair.f} и {pair.s}" : 
                        $"{pair.f} и {pair.s}, ");
                }
            }
            
            Console.WriteLine();
            ConsoleKey key = InputHelper.WaitUserAction(); 
            switch (key) 
            { 
                case ConsoleKey.Backspace: return; 
                case ConsoleKey.Spacebar: GetPairNumsOneOfWhichIsDivByDivisorV01(task); break;
            }
        }
        /// <summary>
        /// Возвращает пары чисел в массиве из которых только одно делится на указанны делитель.
        /// </summary>
        /// <param name="task">Задача.</param>
        /// <exception cref="DivideByZeroException">Исключение - деление на 0</exception>
        /// <remarks>Используется статический класс.</remarks>>
        private static void GetPairNumsOneOfWhichIsDivByDivisorV02(Task task)
        {
            Console.Clear();
            task.TaskDescription();
            
            var array = MakeIntArray(-10_000, 10_000, 20);
            StaticClass.GetPairNumsOneOfWhichIsDivide(array);
        }
        /// <summary>
        /// Демонстрация работы кастомного одномерного массива.
        /// </summary>
        /// <param name="task">Задача.</param>
        private static void UseOneDimIntArray(Task task)
        {
            Console.Clear();
            task.TaskDescription();

            ConsoleKey selectKey = default;
            List<ConsoleKey> validKeys = new List<ConsoleKey>
            {
                ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.D3, ConsoleKey.D4
            };
                
            do
            {
                Console.WriteLine($"1: Из заданного массива\n" +
                                  $"2: Генерация массива(начальное значение, шаг и длина)\n" +
                                  $"3: Генерация массива (случайные числа) с заданным диапазоном и длиной\n" +
                                  $"4: Чтение массива из файла");
                selectKey = Console.ReadKey().Key;
            } while (!validKeys.Contains(selectKey));

            switch (selectKey)
            {
                case ConsoleKey.D1:
                    // Создание кастомного одномерного массива - из имеющегося массива.
                    var existingArray = new int[] {-3, 15, 8, -56, 11, 23, 13, -9, 0, 13, 15, 8};
                    var oneDimIntArray01 = new OneDimIntArray(existingArray);
                    oneDimIntArray01.ToSummary();
                    break;
                case ConsoleKey.D2:
                    // Генерация массива от начального значения, с заданным шагом и длиной 
                    var oneDimIntArray02 = new OneDimIntArray(12, 3, -3);
                    oneDimIntArray02.ToSummary();
                    break;
                case ConsoleKey.D3:
                    // Генерация массива случайными числами с заданным диапазоном и длиной.
                    var oneDimIntArray03 = new OneDimIntArray(15, (-100, 200));
                    oneDimIntArray03.ToSummary();
                    break;
                case ConsoleKey.D4:
                    //  Чтение массива из файла (сначала необходимо создать файл в предыдущих заданиях!).
                    var oneDimIntArray04 = new OneDimIntArray("someArray.txt");
                    oneDimIntArray04.ToSummary();
                    break;
            }
            Console.WriteLine("Для продолжения нажмите любую клавишу...");
            Console.ReadKey();
        }
        /// <summary>
        /// Проверяет введённые логин и пароль на корректность.
        /// </summary>
        /// <param name="task">Задача.</param>
        private static void TryingToLoginGb(Task task) 
        {
            Console.WriteLine($"{task.TaskDescription()}");

            var accounts = LoadUserDB();
            if (accounts is not {Length: > 0}) return;
            var numberAttempts = 3;
            var success = false;

            do
            {
                Console.Write($"Осталось попыток: {numberAttempts}\n");
                Console.Write("Введите логин: ");
                var login = Console.ReadLine();
                if(string.IsNullOrEmpty(login)) continue;
                Console.Write("Введите пароль: ");
                var password = Console.ReadLine();
                if(string.IsNullOrEmpty(password)) continue;

                success = accounts.Any(ac => ac.Login == login && ac.Password == password);
                if (success) break;

                Console.WriteLine("Неверно, попробуй еще раз!");
                numberAttempts--;
            } while (numberAttempts != 0);

            Console.ForegroundColor = success ? ConsoleColor.Green : ConsoleColor.Red;
            Console.WriteLine(success ? 
                $"Вам удалось взломать GeekBrains и получить пожизненный доступ ко всем курсам!" : 
                $"Вам не удалось взломать GeekBrains и за Вами уже выехали!");
            Console.ForegroundColor = ConsoleColor.Black;

            var key = InputHelper.WaitUserAction();
            switch (key)
            {
                case ConsoleKey.Backspace: return;
                case ConsoleKey.Spacebar: TryingToLoginGb(task); break;
            }
        }
        
        /// <summary>
        /// Создаёт массив аккаунтов пользователей и записывает его в файл.
        /// </summary>
        /// <returns></returns>
        private static bool CreateUserDB()
        {
            var accounts = new Account[10];
            accounts[0] = new Account("GAteNTen", "UfKCHnmx");
            accounts[1] = new Account("orehIver", "sEXcKYvS");
            accounts[2] = new Account("vIustILe", "vyKBhBsk");
            accounts[3] = new Account("NShiThea", "HdAArYea");
            accounts[4] = new Account("oMaNCeHe", "NKsVtNfY");
            accounts[5] = new Account("root", "GeekBrains");
            accounts[6] = new Account("atcoNheR", "rraBAAgD");
            accounts[7] = new Account("aDlIdaTI", "GnKGszyM");
            accounts[8] = new Account("DroSCUre", "USmEgPKh");
            accounts[9] = new Account("UsemErIa", "juBARgYP");

            if (!File.Exists(AccountsPath))
                File.Create(AccountsPath);

            var options = new JsonSerializerOptions { WriteIndented = true };
            var jsonString = JsonSerializer.Serialize(accounts, options);

            using var writer = new StreamWriter(AccountsPath);
            writer.Write(jsonString);
            
            return true;
        }

        /// <summary>
        /// Загружает строку БД пользователей из файла и десериализует в массив.
        /// </summary>
        /// <returns>Полученный массив данных.</returns>
        private static Account[]? LoadUserDB()
        {
            if (!File.Exists(AccountsPath)) return null;

            string jsonString = File.ReadAllText(AccountsPath);
            var options = new JsonSerializerOptions { WriteIndented = true };
            var accounts = JsonSerializer.Deserialize<Account[]>(jsonString,options);
            return accounts;
        }

        /// <summary>
        /// Демонстрация работы кастомного двумерного массива.
        /// </summary>
        /// <param name="task">Задача.</param>
        private static void UseTwoDimIntArray(Task task)
        {
            Console.Clear();
            task.TaskDescription();

            ConsoleKey selectKey;
            List<ConsoleKey> validKeys = new List<ConsoleKey>
            {
                ConsoleKey.D1, ConsoleKey.D2
            };
            
            do
            {
                Console.WriteLine($"1: Генерация массива (количество строк, столбцов и диапазон)\n" +
                                  $"2: Загрузка ранее сохраненного массива из файла");
                selectKey = Console.ReadKey().Key;
            } while (!validKeys.Contains(selectKey));

            switch (selectKey)
            {
                case ConsoleKey.D1:
                    // Генерация массива по количеству строк и столбцов в заданном диапазоне
                    var twoDimIntArray01 = new TwoDimIntArray((5, 4), (10, 99));
                    twoDimIntArray01.ToSummary();
                    break;
                case ConsoleKey.D2:
                    // Загрузка ранее сохраненного массива из файла.
                    var twoDimIntArray02 = new TwoDimIntArray();
                    twoDimIntArray02.ToSummary();
                    break;
            }
            
            Console.WriteLine("Для продолжения нажмите любую клавишу...");
            Console.ReadKey();
        }
        #endregion

        #region Helper Methods
        /// <summary>
        /// Выбирает задачу для выполнения.
        /// </summary>
        /// <returns>Ключ нажатой клавиши и задача</returns>
        private static (ConsoleKey key, Task? task) ChoiceTask()
        {
            List<ConsoleKey> validKeys = new List<ConsoleKey>
            {
                ConsoleKey.D1,ConsoleKey.D2, ConsoleKey.D3, ConsoleKey.D4, ConsoleKey.D5,ConsoleKey.Backspace
            };

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
                Console.WriteLine("ИЛИ нажмите Backspace для выхода из программы");
                selectedKey = Console.ReadKey().Key;
            } while (!validKeys.Contains(selectedKey));

            var selectedTask = Tasks
                .FirstOrDefault(el => el.Key.Key == selectedKey);
            
            return (selectedKey, selectedTask.Value);
        }

        private static int[] MakeIntArray(int lowerBound, int upperBound, int lenght)
        {
            var array = new int[lenght];
            for (var index = 0; index < array.Length; index++)
                array[index] = randomizer.Next(lowerBound, upperBound + 1);
            return array;
        }

        private static void Print(int[] array)
        {
            foreach (var item in array)
                Console.Write(item == array.Last() ? $"{item}" : $"{item} ");
            Console.WriteLine();
        }
        #endregion
    }
}


//  логины и пароли считать из файла в массив.
//  Создайте структуру Account, содержащую Login и Password.