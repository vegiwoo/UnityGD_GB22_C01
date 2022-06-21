#nullable enable
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Task05Lib;
using Task06Lib;

namespace Course01Task06 {
    
    internal delegate double Fun(double x);
    internal delegate double Fun02(double x, double y);
    
    internal enum FuncKey {
        FirstFunc, SecondFunc, ThirdFunc, FourthFunc, FifthFunc
    }
    
    internal class Program {

        #region Variables and constants
        /// <summary>
        /// Словарь задач задания.
        /// </summary>
        private static readonly Dictionary<ConsoleKeyInfo, Task> Tasks = new Dictionary<ConsoleKeyInfo, Task>
        {
            {
                new ConsoleKeyInfo('1', ConsoleKey.D1, false, false, false),
                new Task(1, 6, 1,
                    "Делегаты и функции",
                    "Посмотрите распечатку работы функций",
                    "с делегатами в качестве параметров")
            },
            {
                new ConsoleKeyInfo('2', ConsoleKey.D2, false, false, false),
                new Task(1, 6, 2,
                    "Запись и чтение результатов функций",
                    "Выберите функцию, введите переменные",
                    "запишите или прочитайте результат")
            }
        };
        
        /// <summary>
        /// Словарь функций.
        /// </summary>
        private static readonly  Dictionary<FuncKey, (Fun02 func, string desc, string fileName)> Funcs = new ()
        {
            {
                FuncKey.FirstFunc,
                ((x, y) => x + Math.Sqrt(y), "a + \u221A из b", "firstFunc.txt")
            },
            {
                FuncKey.SecondFunc,
                ((x, y) => 4 * x + y, "4а + b", "secondFunc.txt")
            },
            {
                FuncKey.ThirdFunc,
                ((x, y) => -x + y * x, "−а + b * a", "thirdFunc.txt")
            },
            {
                FuncKey.FourthFunc,
                ((x, y) => -4 * x + y, "−4a + b", "fourthFunc.txt")
            },
            {
                FuncKey.FifthFunc,
                ((x, y) => -x * (4 * y + y), "−а * (4b + b)","fifthFunc.txt")
            }
        };
        
        #endregion

        public static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Student.Description();
            Console.Clear();

            (ConsoleKey key, Task? task) choice;

            do
            {
                choice = TaskHelper.ChoiseTask(Tasks);
                Console.Clear();

                if (choice.task != null)
                {
                    switch (choice.key)
                    {
                        case ConsoleKey.D1:
                            UsingDelegatesAsFuncParameters();
                            break;
                        case ConsoleKey.D2:
                            WritingAndReadingFuncResults();
                            break;
                    }
                }
            } while (choice.key != ConsoleKey.Backspace);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Спасибо за использование приложения.");
        }

        #region Task solving methods

        /// <summary>
        /// Использует делегаты дял представления работы функций.
        /// </summary>
        private static void UsingDelegatesAsFuncParameters()
        {
            Console.WriteLine("Таблица функции MyFunc:");
            Table(new Fun(MyFunc), -2, 2);
            Console.WriteLine("Еще раз та же таблица, но вызов организован по-новому");
            Table(MyFunc, -2, 2); // Упрощение(c C# 2.0).Делегат создается автоматически.
            Console.WriteLine("Таблица функции Sin:");
            Table(Math.Sin, -2, 2); // Можно передавать уже созданные
            Console.WriteLine("Таблица функции x^2:");
            // Упрощение(с C# 2.0). Использование анонимного метода
            Table(delegate(double x) { return Math.Pow(x, 2); }, 0, 3);

            // Решение 1 
            Console.WriteLine("--------a*x^2--------");
            Table02(new Fun02(MultiplyParamByXSquared), -3, 4);
            // Результат идентичен, но работаем через анонимый метод 
            Table02(delegate(double x, double a) { return a * Math.Pow(x, 2); }, -3, 4);

            // Решение 2
            Console.WriteLine("------a*sin(x)-------");
            Table02(MultiplyParamBySinOfX, 47, 50);
            // Результат идентичен, но работаем через лямбда-выражение
            Table02((a, x) => a * Math.Sin(Math.PI * x / 180.0), 47, 50);

            Console.ReadKey();
        }

        // Создаём метод, который принимает делегат
        // На практике этот метод сможет принимать любой метод с такой же сигнатурой, как у делегата.
        private static void Table(Fun F, double x, double b)
        {
            Console.WriteLine("----- X ----- Y -----");
            while (x <= b)
            {
                Console.WriteLine("| {0,8:0.000} | {1,8:0.000} |", x, F(x));
                x += 1;
            }

            Console.WriteLine("---------------------");
        }
        
        private static double MyFunc(double x)
        {
            return Math.Pow(x, 3);
        }
        
        #region First task
        /// <summary>
        /// Метод принимает делегат в качестве первого параметра. 
        /// </summary>
        /// <param name="F2">Делегат.</param>
        /// <param name="x">Первый параметр.</param>
        /// <param name="a">Второй параметр.</param>
        private static void Table02(Fun02 F2, double x, double a)
        {
            Console.WriteLine("----- X ------- Y -----");
            while (x <= a)
            {
                Console.WriteLine("| {0,8:0.00} | {1,8:0.00} |", x, F2(a, x));
                x += 1.0;
            }

            Console.WriteLine("---------------------");
            Console.WriteLine("Для возврата в основное меню нажмите любую клавишу...");
        }
        
        /// <summary>
        /// Функция произведения первого числа на второе число в квадрате.
        /// </summary>
        /// <param name="a">Множимое.</param>
        /// <param name="x">Множитель.</param>
        /// <returns>Произведение как результат работы функции.</returns>
        private static double MultiplyParamByXSquared(double a, double x)
        {
            return a * Math.Pow(x, 2);
        }

        /// <summary>
        /// Функция умножения первого числа на синус от второго числа.
        /// </summary>
        /// <param name="a">Множимое.</param>
        /// <param name="x">Множитель.</param>
        /// <returns>Произведение как результат работы функции.</returns>
        private static double MultiplyParamBySinOfX(double a, double x)
        {
            return a * Math.Sin(Math.PI * x / 180.0);
        }
        #endregion
        
        #region Second task
        /// <summary>
        /// Записывает в файл и читает из файла результаты работы функций.
        /// </summary>
        private static void WritingAndReadingFuncResults()
        {
            var validKeys = new List<ConsoleKey>(3)
            {
                ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.Backspace
            };

            ConsoleKey selection;
            
            do
            {
                Console.WriteLine($"1. Рассчитать результаты и записать в файл");
                Console.WriteLine($"2. Прочитать результаты из файла");
                Console.WriteLine($"Backspace: Выйти в предыдущее меню");

                selection = Console.ReadKey().Key;
            } while (!validKeys.Contains(selection));
            
            switch (selection)
            {
                case  ConsoleKey.D1:
                    WriteFuncResults();
                    break;
                case ConsoleKey.D2:
                    ReadingFuncResults();
                    break;
                default:
                    return;
            }
        }
        
        /// <summary>
        /// Рассчитывает и записывает в файл результаты работы функции.
        /// </summary>
        private static void WriteFuncResults()
        {
            var selected = SelectFunc();

            if (selected == null) return;

            double a = default, b = default, h = default;
            do
            {
                Console.Clear();
                Console.WriteLine("\rВведите через пробел значения переменных a, b и шага h");
                Console.Write("\r(2 + или - числа в формате 0,00 (где a < b) и 1 + число формате 0,00): ");

                var numbersString = Console.ReadLine();
                if (string.IsNullOrEmpty(numbersString)) continue;

                var numbersStrings = numbersString.Split(' ');
                if (numbersStrings.Length != 3) continue;

                double.TryParse(numbersStrings[0], out a);
                double.TryParse(numbersStrings[1], out b);
                double.TryParse(numbersStrings[2], out h);
            } while (new double?[] {a, b, h}.Any(p => p == null));

            SaveFunc(selected.Value.fileName, selected.Value.func, a,b,h);
            
            var nextKey = TaskHelper.WaitUserAction();
            switch (nextKey)
            {
                case ConsoleKey.Spacebar: WriteFuncResults(); break;
                case ConsoleKey.Backspace: return;
            }
        }
        
        /// <summary>
        /// Выбирает функцию для выполнения.
        /// </summary>
        /// <returns>Функция, описание и имя файла из словаря.</returns>
        private static (Fun02 func, string desc, string fileName)? SelectFunc()
        {
            var validKeys = new List<ConsoleKey>(6)
            {
                ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.D3, ConsoleKey.D4,
                ConsoleKey.D5, ConsoleKey.Backspace
            };

            ConsoleKey key;
            do
            {
                Console.WriteLine("\rВыберите функцию:\n" +
                                  $"1. {Funcs[FuncKey.FirstFunc].desc}\n" +
                                  $"2. {Funcs[FuncKey.SecondFunc].desc}\n" +
                                  $"3. {Funcs[FuncKey.ThirdFunc].desc}\n" +
                                  $"4. {Funcs[FuncKey.FourthFunc].desc}\n" +
                                  $"5. {Funcs[FuncKey.FifthFunc].desc}\n" +
                                  "или нажмите Backspace для выхода...");
                key = Console.ReadKey().Key;
            } while (!validKeys.Contains(key));

            (Fun02 func, string desc, string fileName)? selectedFunc = null;
            switch (key)
            {
                case ConsoleKey.D1:
                    selectedFunc = Funcs[FuncKey.FirstFunc];
                    break;
                case ConsoleKey.D2:
                    selectedFunc = Funcs[FuncKey.SecondFunc];
                    break;
                case ConsoleKey.D3:
                    selectedFunc = Funcs[FuncKey.ThirdFunc];
                    break;
                case ConsoleKey.D4:
                    selectedFunc = Funcs[FuncKey.FourthFunc];
                    break;
                case ConsoleKey.D5:
                    selectedFunc = Funcs[FuncKey.FifthFunc];
                    break;
            }
            return selectedFunc;
        }

        /// <summary>
        /// Читает из файла результаты работы функции и находит ее минимум.
        /// </summary>
        private static void ReadingFuncResults()
        {
            var selected = SelectFunc();

            if (selected == null) return;
            
            Console.Clear();
            Console.WriteLine($"\rВыбрана функция \"{selected.Value.desc}\"");
            
            var path = AppDomain.CurrentDomain.BaseDirectory + selected.Value.fileName;
            
            if(!File.Exists(path))
                Console.WriteLine($"Файл с результатами работы функции не найден, сначала запишите работу функции.");
            
            var values = Load(selected.Value.fileName, out var minimum);
            for (var i = 0; i < values.Count; i++)
            {
                var text = $"{values[i]}";
                if (i != values.Count -1) text += " ";
                Console.Write(text);
            }

            Console.WriteLine();
            Console.WriteLine($"Минимум функции {minimum}");

            var nextKey = TaskHelper.WaitUserAction();
            switch (nextKey)
            {
                case ConsoleKey.Spacebar: ReadingFuncResults(); break;
                case ConsoleKey.Backspace: return;
            }
        }
        
        /// <summary>
        /// Записывает данные функции в файл.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="F">Делегат</param>
        /// <param name="a">Первое значение промежутка.</param>
        /// <param name="b">Первое значение промежутка.</param>
        /// <param name="h">Шаг работы функции.</param>
        private static void SaveFunc(string fileName, Fun02 F, double a, double b, double h)
        {
            using (var fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                var writer = new BinaryWriter(fileStream);
                var x = a;
                Console.WriteLine("------- X ------- Y ------");
                while (x <= b)
                {
                    Console.WriteLine("| {0,8:0.00} | {1,8:0.00} |", x, F(x, b));
                     writer.Write(Math.Round(F(x,b),2));
                     x += h;
                }
                Console.WriteLine("---------------------");
                Console.WriteLine($"Результаты функции записаны в файл {fileName}.");
            }
        }

        /// <summary>
        /// Считывает результаты работы функции из файла и находит минимум функции.
        /// </summary>
        /// <param name="fileName">Имя файла для загрузки.</param>
        /// <param name="minimum">Минимум функции.</param>
        /// <returns></returns>
        private static List<double> Load(string fileName, out double minimum) 
        {
            using (var fileStream= new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                var reader = new BinaryReader(fileStream);
                var min = double.MaxValue;
                var values = new List<double>();
                
                for(var i = 0; i < fileStream.Length/sizeof(double); i++)
                {
                    var d = reader.ReadDouble();
                    values.Add(d);
                    if (d < min) min = d;
                }
                
                minimum = min;
                return values;
            }
        }
        #endregion
        
        #endregion
    }
}
