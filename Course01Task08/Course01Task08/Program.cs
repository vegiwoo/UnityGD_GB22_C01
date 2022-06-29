#nullable enable
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using Task08Lib;

namespace Course01Task08 {
    internal class Program {
        
        #region Variables and constants
        /// <summary>
        /// Словарь задач домашнего задания.
        /// </summary>
        private static readonly Dictionary<ConsoleKeyInfo, Task> Tasks = new Dictionary<ConsoleKeyInfo, Task>
        {
            {
                new ConsoleKeyInfo('1', ConsoleKey.D1, false, false, false),
                new Task(1, 8, 1,
                    "Рефлексия в действии", 
                    "Все свойства структуры DateTime")
            },
            {
                new ConsoleKeyInfo('1', ConsoleKey.D2, false, false, false),
                new Task(1, 8, 2,
                    "Игра \"Верю-не верю\"", 
                    "Ответь на 10 вопросов",
                    "посмотри на свой результат")
            }
        };
        /// <summary>
        /// База данных вопросов.
        /// </summary>
        private static DataBase dataBase = new DataBase("questions.xml");
        /// <summary>
        /// Регулярное выражение для проверки текста вопроса.
        /// </summary>
        private static readonly Regex QuestionTextRegex = new Regex(DataBase.QuestionTextPattern);
        /// <summary>
        /// Рандомайзер для реалиазции псевдослучайного выбора.
        /// </summary>
        private static Random randomazer = new Random();
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
                            ShowDateTimeProperties(); 
                            break;
                        case ConsoleKey.D2:
                            ChoiceGameBelieveNotBelieve(choice.task);
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

        #region Reflection
        /// <summary>
        /// Возвращает все атрибуты структуры DateTime.
        /// </summary>
        private static void ShowDateTimeProperties()
        {
            var dateTimeType = typeof(DateTime);
            var properties = dateTimeType.GetProperties();
            foreach (var property in properties)
            {
                Console.WriteLine($"Имя свойства: {property.Name}");
                Console.WriteLine($"Модуль типа: {property.Module}");
                Console.WriteLine($"Тип свойства: {property.PropertyType}");
                Console.WriteLine($"Доступ для чтения: {property.CanRead}");
                Console.WriteLine($"Доступ для записи: {property.CanWrite}");
                Console.WriteLine($"Пользовательские атрибуты: {property.CustomAttributes}");
                Console.WriteLine("-----------------------------------------");
            }
            Console.WriteLine("Нажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }
        #endregion

        #region BelieveNotBelieve
        /// <summary>
        /// Выбор игры "Верю-не верю".
        /// </summary>
        /// <param name="task">Задача.</param>
        private static void ChoiceGameBelieveNotBelieve(Task task)
        {
            var validKeys = new List<ConsoleKey>(3)
            {
                ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.Backspace 
            };

            ConsoleKey pressed;
            do
            {
                Console.Clear();
                Console.WriteLine($"\r{task.Name}");
                Console.WriteLine($"1: Редактировать вопросы\n" +
                                  $"2: Играть\n" +
                                  $"Backspace: Вернуться");
                pressed = Console.ReadKey().Key;
            } while (!validKeys.Contains(pressed));

            switch (pressed)
            {
                case ConsoleKey.D1:
                    GotoQuestionEditorBelieveNotBelieve();
                    ChoiceGameBelieveNotBelieve(task);
                    break;
                case ConsoleKey.D2:
                    GotoGameBelieveNotBelieve(task, 10, dataBase.GetQuestions());
                    ChoiceGameBelieveNotBelieve(task);
                    break;
            }

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine($"\u00A9 Dmitry Samartcev, Learning on \"GeekBrains.com\", 2022. No rights reserved.");
            Thread.Sleep(2000);
            Console.ForegroundColor = ConsoleColor.Blue;
        }

        /// <summary>
        /// Переход к редактору вопросов игры "Верю-не верю".
        /// </summary>
        private static void GotoQuestionEditorBelieveNotBelieve()
        {
            var validKeys = new List<ConsoleKey>(3)
            {
                ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.Backspace 
            };

            ConsoleKey pressed;
            do
            {
                Console.Clear();
                Console.WriteLine($"\r===================================");
                Console.WriteLine($"Редактор игры \"Верю-не верю\"");
                Console.WriteLine($"===================================");
                Console.WriteLine($"1: Добавить новый вопрос\n" +
                                  $"2: Посмотреть имеющиеся вопросы\n" +
                                  $"Backspace: Вернуться");
                pressed = Console.ReadKey().Key;
            } while (!validKeys.Contains(pressed));

            switch (pressed)
            {
                case ConsoleKey.D1:
                   AddNewQuestion();
                   GotoQuestionEditorBelieveNotBelieve();
                   break;
                case ConsoleKey.D2:
                    GotoAvailableQuestions(0, dataBase.GetQuestions());
                    GotoQuestionEditorBelieveNotBelieve();
                    break;
            }
        }
        
        /// <summary>
        /// Добавляет новый вопрос.
        /// </summary>
        private static void AddNewQuestion()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"Кириллица или латиница, пробелы и спецсимволы, до 150 знаков");
                Console.ForegroundColor = ConsoleColor.Blue;
                
                Console.Write($"Введите вопрос: ");
                var text = Console.ReadLine();
                
                if(string.IsNullOrEmpty(text) || !QuestionTextRegex.IsMatch(text)) 
                    continue;
                
                Console.Write($"Введите ответ (0 - не правда, 1 - правда): ");
                if (!int.TryParse(Console.ReadLine(), out var answer) || (answer != 0 && answer != 1))
                    continue;

                var saved = dataBase.Add(text, Convert.ToBoolean(answer));
                if (saved)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Новый вопрос успешно сохранен в базу данных");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Thread.Sleep(1500);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Новый вопрос не сохранен в базу данных");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Thread.Sleep(1500);
                }
                return;
            }
        }

        /// <summary>
        /// Показывает имеющиеся вопросы.
        /// </summary>
        /// <param name="index">Начальный индекс навигации.</param>
        /// <param name="questions">Коллекция вопросов для навигации.</param>
        private static void GotoAvailableQuestions(int index, List<Question> questions)
        { 
            Console.Clear();
            Console.WriteLine($"\rВопрос: {index + 1} из {questions.Count}\n" +
                              $"{questions[index].Text}\n" +
                              $"Ответ: {questions[index].Answer}");

            var validKeys = new List<ConsoleKey>(3)
            {
                ConsoleKey.LeftArrow, ConsoleKey.RightArrow, ConsoleKey.D, ConsoleKey.Backspace
            };

            ConsoleKey pressed;
            do
            {
                var output = string.Empty;
                
                if (index != 0)
                    output += "\u2190 НАЗАД, ";
                
                if (index != questions.Count - 1)
                    output += "\u2192 ВПЕРЕД, ";
                
                output += "D Удалить вопрос, Backspace ВЫХОД";
                Console.WriteLine($"{output}");

                pressed = Console.ReadKey().Key;
            } while (!validKeys.Contains(pressed));

            switch (pressed)
            {
                case ConsoleKey.LeftArrow:
                    if (index != 0) index--;
                    break;
                case ConsoleKey.RightArrow:
                    if (index != questions.Count - 1) index++;
                    break;
                case ConsoleKey.D:

                    Console.WriteLine(dataBase.Remove(index)
                        ? "\rВопрос успешно удалён из БД"
                        : "\rНе удалось удалить вопрос из БД");

                    Thread.Sleep(1500);
                    
                    questions = dataBase.GetQuestions();
                    
                    if (questions.Count > 0)
                    {
                        if (index == questions.Count - 1)
                            index--;
                        
                        GotoAvailableQuestions(index, questions);
                    }
                    else
                        return;
                    
                    break;
                case ConsoleKey.Backspace:
                    return;
            }

            GotoAvailableQuestions(index, questions);
        }

        /// <summary>
        /// Переход к игре "Верю-не верю".
        /// </summary>
        private static void GotoGameBelieveNotBelieve(Task task, int questionCount, IReadOnlyList<Question> questions)
        {
            var askedQuestions = new Dictionary<int, bool>(questionCount);
           
            var moveCounter = 0;
            
            // Условие победы - 70 % правильных ответов
            var victoryСondition = Convert.ToInt32(questionCount * 0.7) ;
            
            var validKeys = new List<ConsoleKey>(2)
            {
                ConsoleKey.D0, ConsoleKey.D1
            };

            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("=================================");
                Console.WriteLine($"{task.Name}");
                task.TaskDescription();
                Console.WriteLine("=================================");
                Console.ForegroundColor = ConsoleColor.Blue;

                int newQuestionIndex;
                while (true)
                {
                    newQuestionIndex = randomazer.Next(0, questions.Count);
                    if (askedQuestions.ContainsKey(newQuestionIndex)) continue;
                    askedQuestions[newQuestionIndex] = false;
                    break;
                }
                
                moveCounter++;
                
                Console.WriteLine($"Вопрос {moveCounter} / {questionCount}");

                ConsoleKey pressed = default;
                while (!validKeys.Contains(pressed))
                {
                    Console.WriteLine($"\r{questions[newQuestionIndex].Text}");
                    Console.WriteLine($"ДА - 1, НЕТ - 0");
                    pressed = Console.ReadKey().Key;
                }

                switch (pressed)
                {
                    case ConsoleKey.D0:
                        if (!questions[newQuestionIndex].Answer)
                            askedQuestions[newQuestionIndex] = true;
                        break;
                    case ConsoleKey.D1:
                        if (questions[newQuestionIndex].Answer)
                            askedQuestions[newQuestionIndex] = true;
                        break;
                }
            } while (moveCounter != questionCount);

            var rightAnswers = askedQuestions
                .Count(qu => qu.Value);

            var result = $"\rВы правильно ответили на {rightAnswers} из {questionCount} вопросов";

            if (rightAnswers >= victoryСondition)
                result += " и выиграли :)";
            else
                result += " и проиграли :(";
            
            Console.WriteLine(result);
            var select = TaskHelper.WaitUserAction();

            switch (select)
            {
                case ConsoleKey.Spacebar:
                    GotoGameBelieveNotBelieve(task, questionCount, questions);
                    break;
            }
        }
        #endregion
        #endregion
    }
}