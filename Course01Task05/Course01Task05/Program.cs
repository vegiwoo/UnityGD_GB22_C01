#nullable enable
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Task05Lib;

namespace Course01Task05 {
    internal class Program {

        #region Variables and constants
        private static readonly Random randomizer = new Random();
        private static readonly Stopwatch stopwatch = new Stopwatch();
        private const string SourceText = @"Пять коней подарил мне мой друг Люцифер
        И одно золотое с рубином кольцо,
        Чтобы мог я спускаться в глубины пещер
        И увидел небес молодое лицо.

        Кони фыркали, били копытом, маня
        Понестись на широком пространстве земном,
        И я верил, что солнце зажглось для меня,
        Просияв, как рубин на кольце золотом.

        Много звёздных ночей, много огненных дней
        Я скитался, не зная скитанью конца,
        Я смеялся порывам могучих коней
        И игре моего золотого кольца.

        Там, на высях сознанья — безумье и снег,
        Но коней я ударил свистящим бичем,
        Я на выси сознанья направил их бег
        И увидел там деву с печальным лицом.

        В тихом голосе слышались звоны струны,
        В странном взоре сливался с ответом вопрос,
        И я отдал кольцо этой деве луны
        За неверный оттенок разбросанных кос.

        И, смеясь надо мной, презирая меня,
        Люцифер распахнул мне ворота во тьму,
        Люцифер подарил мне шестого коня —
        И Отчаянье было названье ему.";
        
        private static readonly Dictionary<ConsoleKeyInfo, Task> Tasks = new Dictionary<ConsoleKeyInfo, Task>
        {
            {
                new ConsoleKeyInfo('1', ConsoleKey.D1, false, false, false),
                new Task(1, 5, 1, 
                    "Проверка ввода логина",
                    "Введите логин (строка 2-10 символов: буквы латинского алфавита или цифры, цифра не может быть первой", 
                    "и посмотрите на результаты проверки")
            },
            {
                new ConsoleKeyInfo('1', ConsoleKey.D2, false,false,false), 
                new Task(1,5,2, 
                    "Анализ текста классом Message",
                    "Cтихотворение Н.Гумилева \"Баллада\"")
            },
            {
                new ConsoleKeyInfo('1', ConsoleKey.D3, false,false,false), 
                new Task(1,5,3, 
                    "Перевернутая строка",
                    "Попробуйте набрать перевернутый вариант слова", 
                    "и проверьте свой результат.")
            },
            {
                new ConsoleKeyInfo('1', ConsoleKey.D4, false,false,false), 
                new Task(1,5,4, 
                    "Худшие ученики по ЕГЭ",
                    "Введите количество учеников, заполните БД", 
                    "и получите худших учеников по среднему баллу.")
            }
        }; 

        // Подготовленные данные для 4 задачи
        private static readonly HighSchoolStudent[] StudentsDB =
        {
            new ("Марьяна", "Воронова", new byte[] {4, 4, 3}),
            new ("Виктор", "Крылов", new byte[] {2, 2, 4}),
            new ("Елизавета", "Хромова", new byte[] {3, 5, 2}),
            new ("Матвей", "Ильин", new byte[] {5, 5, 5}),
            new ("Анастасия", "Демидова", new byte[] {3, 2, 4}),
            new ("Максим", "Фокин", new byte[] {4, 5, 5}),
            new ("Иван", "Семенов", new byte[] {2, 3, 2}),
            new ("Ярослав", "Карпов", new byte[] {5, 4, 5}),
            new ("Мария", "Лукьянова", new byte[] {3, 2, 3}),
            new ("Павел", "Бирюков", new byte[] {4, 4, 4}),
            new ("Серафима", "Куликова", new byte[] {5, 3, 4}),
            new ("Тимофей", "Митрофанов", new byte[] {5, 4, 5}),
            new ("Николь", "Плотникова", new byte[] {3, 4, 4}),
            new ("Зоя", "Наумова", new byte[] {4, 4, 3}),
            new ("Иван", "Ананьев", new byte[] {3, 4, 4})
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
                            CheckingLoginString(choice.task);
                            break;
                        case ConsoleKey.D2:
                            AnalyzeText(choice.task);
                            break;
                        case ConsoleKey.D3:
                            FlipString(choice.task);
                            break;
                        case ConsoleKey.D4:
                            FindWorstStudents(choice.task);
                            break;
                    }
                }
            } while (choice.key != ConsoleKey.Backspace);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Спасибо за использование приложения.");
        }
        
        #region Task solving methods
        /// <summary>
        /// Проверят строку логина на валидность. 
        /// </summary>
        /// <param name="task"></param>
        private static void CheckingLoginString(Task task)
        {
            Console.Clear();
            task.TaskDescription();
            stopwatch.Reset();
            
            Console.Write("Введите логин: ");
            var loginString = Console.ReadLine();

            if (string.IsNullOrEmpty(loginString))
            {
                task.ErrorDescription("Строка не может быть пустой");
            }
            else
            {
                // Случайные выбор метода выполнения проверки
                var choiceMethod = randomizer.Next(2);
                if (choiceMethod == 1)
                {
                    stopwatch.Start();
                    Console.WriteLine("Проверка с помощью LINQ");
                    if (CheckLogin(loginString))
                        Console.Write("Логин валиден! ");
                    else
                        task.ErrorDescription();
                    stopwatch.Stop();
                    Console.WriteLine("----------------------");
                    Console.WriteLine($"\rВыполнено за: {stopwatch.ElapsedMilliseconds} мс.");
                }
                else
                {
                    stopwatch.Start();
                    Console.WriteLine("Проверка с помощью Regex");
                    if (CheckLoginWithRegex(loginString))
                        Console.WriteLine("Логин валиден!");
                    else
                        task.ErrorDescription();
                    stopwatch.Stop();
                    Console.WriteLine("----------------------");
                    Console.WriteLine($"\rВыполнено за: {stopwatch.ElapsedMilliseconds} мс.");
                }
            }

            ConsoleKey selectedKey = TaskHelper.WaitUserAction();
            switch (selectedKey)
            {
                case ConsoleKey.Spacebar: CheckingLoginString(task); break;
                default: return;
            }
        }

        /// <summary>
        /// Проверка логина, v.1.0
        /// </summary>
        /// <param name="value">Строка для проверки</param>
        /// <returns>Флаг проверки.</returns>
        /// <remarks>Строка не может быть пустой, начинаться с цифры,
        /// быть меньше 2 или больше 10 символов и содержать что-то кроме
        /// латинских символов или цифр.
        /// </remarks>>
        private static bool CheckLogin(in string value)
        {
            return !char.IsDigit(value[0]) &&
                   value.Length is >= 2 and <= 10 &&
                   value.Count(ch => !char.IsLetterOrDigit(ch)) == 0;
        }
        
        /// <summary>
        /// Проверка логина, v.2.0
        /// </summary>
        /// <param name="value">Строка для проверки</param>
        /// <returns>Флаг проверки.</returns>
        /// <remarks>Строка не может быть пустой, начинаться с цифры,
        /// быть меньше 2 или больше 10 символов и содержать что-то кроме
        /// латинских символов или цифр.
        /// </remarks>>
        private static bool CheckLoginWithRegex(in string value)
        {
            return Message.Regexes[RegexKey.Login].IsMatch(value);
        }

        /// <summary>
        /// Анализирует исходный текст статическим классом Message.
        /// </summary>
        /// <param name="task">Задача.</param>
        private static void AnalyzeText(Task task)
        {
            Console.Clear();
            task.TaskDescription();
            stopwatch.Reset();
            
            List<ConsoleKey> validKeys = new List<ConsoleKey>
            {
                ConsoleKey.D1, ConsoleKey.D2,ConsoleKey.D3, ConsoleKey.D4, ConsoleKey.D5,ConsoleKey.D6
            };
            ConsoleKey selectedKey;

            do
            {
                stopwatch.Reset();
                Console.WriteLine($"1: Вывести текст на экран\n" +
                                  $"2: Выбрать слова не больше указанной длины\n" +
                                  $"3: Удалить слова, заканчивающиеся на определённый символ\n" +
                                  $"4: Найти самые длинные слова\n" +
                                  $"5: Найти самые длинные слова, вернуть строку\n" +
                                  $"6: Частота вхождения слов в текст\n" +
                                  $"Backspace: Выход");

                selectedKey = Console.ReadKey().Key;
            } while (!validKeys.Contains(selectedKey) && 
                     selectedKey != ConsoleKey.Backspace);
            
            switch (selectedKey)
            {
                case ConsoleKey.D1:
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine("\t" + SourceText);
                    break;
                case ConsoleKey.D2:
                    Console.Write("\rВведите длину слова: ");
                    if (int.TryParse(Console.ReadLine(), out var count))
                    {
                        stopwatch.Start();
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        var words = Message.FindSpecificWords(SourceText, count);
                        foreach (var word in words)
                            Console.Write($"{word} ");
                        stopwatch.Stop();
                        Console.WriteLine("\n----------------------");
                        Console.WriteLine($"\rВыполнено за: {stopwatch.ElapsedMilliseconds} мс.");
                    }
                    break;
                case ConsoleKey.D3:
                    char inputChar;
                    do
                    {
                        Console.Write("\rВведите символ русского алфавита: ");
                        if (!char.TryParse(Console.ReadLine(), out var character) || !Message.isRus(character))
                            continue;
                        inputChar = character;
                        break;
                    } while (true);

                    stopwatch.Start();
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    var dWords = Message.DeleteSpecificWords(SourceText, inputChar);
                    foreach (var word in dWords)
                        Console.Write($"{word} ");
                    stopwatch.Stop();
                    Console.WriteLine("\n\r----------------------");
                    Console.WriteLine($"\rВыполнено за: {stopwatch.ElapsedMilliseconds} мс.");
                    break;
                case ConsoleKey.D4:
                    stopwatch.Start();
                    var grouping = Message.FindMaxLengthWords(SourceText);
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine($"\rДлина самого длинного слова, символов: {grouping.Key}");
                    Console.WriteLine($"\rСамых длинных слов в тексте: {grouping.Count()}");
                    foreach (var word in grouping)
                        Console.Write($"{word} ");
                    stopwatch.Stop();
                    Console.WriteLine("\n----------------------");
                    Console.WriteLine($"\rВыполнено за: {stopwatch.ElapsedMilliseconds} мс.");
                    break;
                case ConsoleKey.D5:
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    stopwatch.Start();
                    var maxWordsString = Message.FindMaxLengthWordsAnaMakeString(SourceText);
                    Console.Write($"\rСтрока из самых длинных слов: {maxWordsString}");
                    stopwatch.Stop();
                    Console.WriteLine("\n----------------------");
                    Console.WriteLine($"\rВыполнено за: {stopwatch.ElapsedMilliseconds} мс.");
                    break;
                case ConsoleKey.D6:
                    string[]? sWords = default;
                    do
                    {
                        Console.WriteLine("\t" + SourceText);
                        Console.WriteLine();
                        Console.Write($"Введите через пробел слова для поиска (кириллица, мин 2 знака): ");
                        
                        var inputString = Console.ReadLine();
                        if (string.IsNullOrEmpty(inputString)) continue;
                        
                        var inputWords = inputString.Trim().Split(' ');
                        if (inputWords.All(w => Message.Regexes[RegexKey.WordRus].IsMatch(w)))
                            sWords = inputWords;
                    } while (sWords == null);
                    
                    stopwatch.Start();
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    var frequencyWords = Message.GetFrequencyAnalysisOfText(SourceText, sWords);
                    foreach (var group in frequencyWords)
                        foreach (var word in group)
                                Console.WriteLine($"Слово \"{word}\" встречается раз: {group.Key}");
                    
                    stopwatch.Stop();
                    Console.WriteLine("----------------------");
                    Console.WriteLine($"\rВыполнено за: {stopwatch.ElapsedMilliseconds} мс.");
                    break;
                default:
                    return;
            }
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
            AnalyzeText(task);
        }
        
        /// <summary>
        /// Проверяет реверсированный вариант исходной строки.
        /// </summary>
        /// <param name="task">Задача.</param>
        private static void FlipString(Task task) {
            Console.Clear();
            task.TaskDescription();
            stopwatch.Reset();

            var words = new List<string>
            {
                "неделя", "слово", "соглашение", "революция", "огонь", "животное"
            };
            
            var i = randomizer.Next(0, words.Count - 1);
            var chechedWord = words[i];
            
            string? inputString = default;
            do
            {
                Console.WriteLine($"Выбранное слово: {chechedWord}");
                Console.Write($"Напечатайте это слово наоборот: ");
                
                var input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input))
                    inputString = input.Trim();

            } while (inputString == null);

            stopwatch.Start();
            var reverse = new string(chechedWord.ToCharArray().Reverse().ToArray());
            if (inputString.Equals(reverse))
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine($"Верно, \"{inputString}\" перевернутый вариант слова \"{chechedWord}\".");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"Неверно, перевернутый вариант слова \"{chechedWord}\" - \"{reverse}\".");
            }
            stopwatch.Stop();
            Console.WriteLine("----------------------");
            Console.WriteLine($"\rВыполнено за: {stopwatch.ElapsedMilliseconds} мс.");
            
            Console.ForegroundColor = ConsoleColor.Blue;
            var selectedKey = TaskHelper.WaitUserAction();
            switch (selectedKey)
            {
                case ConsoleKey.Spacebar: FlipString(task); break;
                default: return;
            }
        }

        /// <summary>
        /// Находит худших учеников по среднему баллу ЕГЭ.
        /// </summary>
        /// <param name="task">Задача.</param>
        private static void FindWorstStudents(Task task)
        {
            Console.Clear();
            task.TaskDescription();

            HighSchoolStudent[]? students = default;
            
            Console.Write("Введите количество учеников 9\"Б\" класса (от 10 до 100): ");
            if (int.TryParse(Console.ReadLine(), out var count) && count is >= 10 and <= 100)
            {
                students = new HighSchoolStudent[count];
                
                // Заполняем массив заранее подготовленными данными.
                if (students.Length >= StudentsDB.GetLength(0))
                {
                    for (var i = 0; i < StudentsDB.Length; i++)
                        students[i] = StudentsDB[i];
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine($"Список учеников заполнен из БД, сейчас в нем {students.Count(st => st != null)} записей");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine($"Список учеников не удалось заполнить из БД, сейчас в нем {students.Length} записей");
                }
            }
            else
            {
                task.ErrorDescription("Необходимо целое число от 10 до 100");
                FindWorstStudents(task);
            }
            Console.ForegroundColor = ConsoleColor.Blue;
            Thread.Sleep(2000);
            WorkingWithStudentDb(ref students!);
        }

        /// <summary>
        /// Работает с базой данных учеников. 
        /// </summary>
        /// <param name="students">Массив учеников.</param>
        private static void WorkingWithStudentDb(ref HighSchoolStudent[] students)
        {
            Console.Clear();
            stopwatch.Reset();
            
            List<ConsoleKey> validKeys = new List<ConsoleKey>
            {
                ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.Backspace
            };
            
            ConsoleKey selectKey = default;
            do
            {
                Console.WriteLine($"1: Ввести данные нового ученика\n" +
                                  $"2: Найти худших учеников по среднему баллу");
                selectKey = Console.ReadKey().Key;
            } while (!validKeys.Contains(selectKey));

            switch (selectKey)
            {
                case ConsoleKey.D1:
                    var newStudent = EnterNewStudentDetails();
                    Array.Resize(ref students, students!.Length + 1);
                    students[students.Length - 1] = newStudent;
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine($"В БД добавлен новый ученик {newStudent}");
                    Console.WriteLine($"Записей в БД: {students.Count(st => st != null)}");
                    Thread.Sleep(2000);
                    Console.ForegroundColor = ConsoleColor.Blue;
                    WorkingWithStudentDb(ref students);
                    break;
                case ConsoleKey.D2:
                    stopwatch.Start();
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine("=====Худшие ученики по среднему баллу ЕГЭ=====");
                    
                    var worstStudents = new List<HighSchoolStudent>();
                    var groups = HighSchoolStudent.FindWorstStudents(students);
                    
                    foreach (var group in groups)
                        if (worstStudents.Count() < 3)
                            worstStudents.AddRange(group);
                        else 
                            break;
                    
                    foreach (var student in worstStudents)
                        Console.WriteLine(student);
                    
                    stopwatch.Stop();
                    Console.WriteLine("----------------------");
                    Console.WriteLine($"\rВыполнено за: {stopwatch.ElapsedMilliseconds} мс.");
                    Console.WriteLine("Нажмите любую клавишу для выхода ...");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.ReadKey();
                    break;
                default:
                    return;
            }
        }

        /// <summary>
        /// Организует ввод информации и записи о новом ученике.
        /// </summary>
        /// <returns>Созданная запись об ученике.</returns>
        private static HighSchoolStudent EnterNewStudentDetails()
        {
            HighSchoolStudent? newStudent = default;
            while (newStudent == null)
            {
                Console.Clear();
                Console.WriteLine("Введите фамилию (до 20 симв), имя (до 15 симв) и три оценки ЕГЭ");
                Console.Write("\r(например Иванов Петр 4 5 3): ");
                var input = Console.ReadLine();

                try
                {
                    newStudent = HighSchoolStudent.ParseStudent(input);
                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine(e.Message);
                    Thread.Sleep(1500);
                    Console.ForegroundColor = ConsoleColor.Blue;
                    EnterNewStudentDetails();
                }
            }
            return newStudent;
        }
        #endregion
    }
}