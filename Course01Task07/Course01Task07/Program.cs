using System;
using System.Collections.Generic;
using System.Linq;
using Task07Lib;

namespace Course01Task07 {
    internal class Program {
        
        #region Variables and constants
        /// <summary>
        /// Словарь задач задания.
        /// </summary>
        private static readonly Dictionary<ConsoleKeyInfo, Task> Tasks = new Dictionary<ConsoleKeyInfo, Task>
        {
            {
                new ConsoleKeyInfo('1', ConsoleKey.D1, false, false, false),
                new Task(1, 7, 1,
                    "Игра \"Удвоитель\"", "")
            },
            {
                new ConsoleKeyInfo('2', ConsoleKey.D2, false, false, false),
                new Task(1, 7, 2,
                    "Игра \"Угадай число\"",
                    "Введите целое число")
            }
        };
        
        private static readonly Random randomizer = new();
        #endregion
        
        public static void Main(string[] args)
        {
            (ConsoleKey key, Task? task) choice;
            do
            {
                Console.Clear();
                Student.Description();
                choice = TaskHelper.ChoiseTask(Tasks);
                
                if (choice.task != null)
                {
                    switch (choice.key)
                    {
                        case ConsoleKey.D1:
                            PlayDoubler(1, (15, 34));
                            break;
                        case ConsoleKey.D2:
                            PlayGuessNumber(choice.task, (0, 100));
                            break;
                        default:
                            continue;
                    }
                }
            } while (choice.key != ConsoleKey.Backspace);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Спасибо за использование приложения.");
        }
        
        #region Task solving methods
        #region Game "Doubler"
        /// <summary>
        /// Запускает игру "Удвоитель".
        /// </summary>
        /// <param name="currentNumber">Число, с которого начинается игра.</param>
        /// <param name="boundes">Границы, в которых генерируется загаданное число.</param>
        private static void PlayDoubler(int currentNumber, (int low, int high) boundes)
        {
            Console.Clear();
            int hiddenNumber = default, minNumberMoves = default;

            int Plus(int i) => i + 1;
            int Multiplication(int i) => i * 2;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("================================================================================");
            Console.WriteLine("Правила игры \"Удвоитель\":");
            Console.WriteLine($"Попробуйте получить загаданное число из текущего за минимальное кол-во ходов.\n" +
                              $"Доступные операции над текущим числом: + 1 и х 2");
            Console.WriteLine("================================================================================");
            Console.ForegroundColor = ConsoleColor.Blue;

            do
            {
                var isRoundWasPlayed = hiddenNumber != 0 && minNumberMoves != 0;
                
                Console.WriteLine("\rEnter: Начать новую партию.");
                if(isRoundWasPlayed) Console.WriteLine("SpaceBar: Сыграть предыдущую партию.");
                Console.WriteLine("Backspace: Выйти из игры.");
                
                var selectKey = Console.ReadKey().Key;
                if(selectKey == ConsoleKey.Backspace) break;
                if(selectKey != ConsoleKey.Enter && selectKey != ConsoleKey.Spacebar) continue;
                
                if(selectKey == ConsoleKey.Enter)
                    ResetsGameParamsDoubler(currentNumber, boundes, out hiddenNumber, out minNumberMoves);
                
                PlayPartyGameDoubler(in currentNumber, in hiddenNumber, in minNumberMoves, Plus, Multiplication);
            } while (true);
        }

        /// <summary>
        /// Сбрасывает параметры игры "Удвоитель".
        /// </summary>
        /// <param name="currentNumber">Текущее (начальное) число.</param>
        /// <param name="boundes">Границы, в которых генерируется hiddenNumber.</param>
        /// <param name="hiddenNumber">Конечное число, которое необходимо угадать.</param>
        /// <param name="minNumberMoves">Минимальное количество ходов.</param>
        private static void ResetsGameParamsDoubler(int currentNumber, (int low, int high) boundes, out int hiddenNumber, out int minNumberMoves)
        {
            hiddenNumber = randomizer.Next(boundes.low,boundes.high + 1);
            minNumberMoves = FindMinimumNumberMovesForDoubler(currentNumber, hiddenNumber);
        }

        /// <summary>
        /// Запускает новую / предыдущую партию игры "Удвоитель".
        /// </summary>
        /// <param name="startNumber">Текущее (начальное) число.</param>
        /// <param name="endNumber">Конечное число, которое необходимо угадать.</param>
        /// <param name="minNumberMoves">Минимальное количество ходов.</param>
        /// <param name="plus">Делегат (функция) прибавления на 1.</param>
        /// <param name="multiplication">Делегат (функция) умножения на 2.</param>
        private static void PlayPartyGameDoubler(in int startNumber, in int endNumber, in int minNumberMoves, 
            Func<int,int> plus, Func<int,int> multiplication)
        {
            var currentNumber = startNumber;
            var steps = new Stack<int>();
            var currentNumberMoves = 0;
            
            while (currentNumber < endNumber)
            {
                var isNumbersOnStack = steps.Count > 0;
                Console.Clear();
                Console.WriteLine($"Ходов: минимальное количество: {minNumberMoves}, сделано: {currentNumberMoves}\n" +
                                  $"Текущее число: {currentNumber}, загаданное число: {endNumber}\n" +
                                  $"1: {currentNumber} + 1\n" +
                                  $"2: {currentNumber} х 2");
                if (startNumber != currentNumber) Console.WriteLine($"3: Отменить xoд\n");
                
                var selectedKey = Console.ReadKey().Key;
                switch (selectedKey)
                {
                    case ConsoleKey.D1:
                        currentNumber = plus.Invoke(currentNumber);
                        break;
                    case ConsoleKey.D2: 
                        currentNumber = multiplication.Invoke(currentNumber);
                        break;
                    case ConsoleKey.D3:
                        if (isNumbersOnStack)
                        {
                            if (currentNumber == steps.Peek()) steps.Pop();
                            currentNumber = steps.Pop();
                            currentNumberMoves--;
                        }
                        else
                        {
                            currentNumber = startNumber;
                            currentNumberMoves = 0;
                        }
                        break;
                }

                if (selectedKey is not (ConsoleKey.D1 or ConsoleKey.D2)) continue;
                
                currentNumberMoves = plus.Invoke(currentNumberMoves);
                steps.Push(currentNumber);
            }
            
            if (currentNumber > endNumber)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"\rВы проиграли, текущее число {currentNumber} больше загаданного {endNumber}.");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine($"\rВы выиграли, текущее число {currentNumber} равно загаданному {endNumber}.");
                
                if (currentNumberMoves == minNumberMoves)
                    Console.WriteLine($"\rBONUS: (+ 100500 к карме), Вы уложились в минимальное количество ходов.");
            }
            Console.ForegroundColor = ConsoleColor.Blue;
        }

        /// <summary>
        /// Вычисляет минимальное количество ходов нахождения числа для игры "Удвоитель".
        /// </summary>
        /// <param name="startNumber">Начальное число.</param>
        /// <param name="endNumber">Конечное число.</param>
        /// <returns>Минимальное количество ходов.</returns>
        /// <remarks>Ходы просчитываются с учётом возможности
        /// двух действий: умножение на 2 на прибавление 1.</remarks>>
        private static int FindMinimumNumberMovesForDoubler(int startNumber, int endNumber)
        {
            var currentNumber = endNumber;
            var count = 0;
            
            while (currentNumber != startNumber)
            {
                var div = currentNumber / 2;
                if (currentNumber % 2 == 0 && div > startNumber)
                    currentNumber = div;
                else
                    currentNumber--;
                count++;
            }
            return count; 
        }
        #endregion
        
        #region Game "Guess number"
        private static void PlayGuessNumber(Task task, (int low, int high) boundes)
        {
            Console.Clear();
            var hiddenNumber = randomizer.Next(boundes.low, boundes.high + 1);
            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("================================================================================");
            Console.WriteLine("Правила игры \"Угадай число\":");
            Console.WriteLine($"Компьютер загадал целое число.\n" +
                              $"Попробуй угадать это число за минимальное количество попыток.");
            Console.WriteLine("================================================================================");
            Console.ForegroundColor = ConsoleColor.Blue;

            var enteredNumbers = new List<int>(14);
            int currentNumberMoves = default;
            
            IReadOnlyList<int> range = Enumerable.Range(boundes.low, boundes.high).ToList();
            FindMinimumNumberMovesForGuessNumber(hiddenNumber, range , out var minNumberMoves);

            Console.WriteLine("Нажмите любую клавишу для начала игры...");
            Console.ReadKey();
            
            int currentNumber;
            do
            {
                Console.WriteLine($"\rЯ загадал какое-то число от {boundes.low} до {boundes.high}");
                Console.WriteLine($"Попыток: минимальное количество {minNumberMoves}, сделано {currentNumberMoves}");
                Console.Write("Введите число: ");

                if (!int.TryParse(Console.ReadLine(), out currentNumber) || 
                    currentNumber < boundes.low ||
                    currentNumber > boundes.high)
                {
                    task.ErrorDescription();
                    continue;
                }
                
                if (enteredNumbers.Contains(currentNumber))
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Такое число уже было");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    continue;
                }

                enteredNumbers.Add(currentNumber);

                if (currentNumber < hiddenNumber)
                {
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.WriteLine("Мое число больше");
                }
                else if (currentNumber > hiddenNumber)
                {
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine("Мое число меньше");
                }
                else
                    break;
                
                Console.ForegroundColor = ConsoleColor.Blue;
                
                currentNumberMoves++;
            } while (true);
            
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            
            Console.WriteLine($"\rВы выиграли, текущее число {currentNumber} равно загаданному {hiddenNumber}.");
            
            if (currentNumberMoves == minNumberMoves || currentNumberMoves < minNumberMoves) 
                Console.WriteLine($"\rBONUS: (+ 100500 к карме), Вы уложились в минимальное количество ходов.");

            var selectKey = TaskHelper.WaitUserAction();
            switch (selectKey)
            {
               case ConsoleKey.Spacebar:
                   PlayGuessNumber(task, boundes);
                   break;
               default:
                   return;
            }
        }
        
        /// <summary>
        /// Рассчитывает минимальное количество ходов для игры "Угадай число".
        /// </summary>
        /// <param name="item">Элемент для поиска.</param>
        /// <param name="range">Коллекция для поиска элемента.</param>
        /// <param name="movesCount">Количество проходов поиска.</param>
        /// <typeparam name="T">Универсальный тип для метода.</typeparam>
        /// <remarks>Коллекция для поиска элемента должна быть отсортирована.</remarks>>
        private static void FindMinimumNumberMovesForGuessNumber<T>(T item, IReadOnlyList<T> range, out int movesCount)
            where T : IComparable<T>
        {
            int low = 0, high = range.Count() - 1;
            movesCount = 0;
            
            while (low <= high)
            {
                var mid = (low + high) / 2; 
                movesCount++;
                
                var result = range[mid].CompareTo(item);
                if (result > 0)
                    high = mid - 1;
                else if(result < 0)
                    low = mid + 1;
                else
                    break;
            }
        }
        #endregion
        #endregion
    }
}
