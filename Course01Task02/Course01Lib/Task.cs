#nullable enable
using System;
using System.Linq;

namespace Course01Lib {
    public class Task {
        /// <summary>
        /// Номер курса.
        /// </summary>
        private int CourseNumber { get; }
        /// <summary>
        /// Номер домашней работы.
        /// </summary>
        private int HomeworkNumber { get; }
        /// <summary>
        /// Номер задачи.
        /// </summary>
        public int TaskNumber { get;  }
        /// <summary>
        /// Название задачи.
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// Правило ввода для пользователя.
        /// </summary>
        public string InputRule { get; }
        /// <summary>
        /// Ожидаемый результат решения задачи.
        /// </summary>
        private string? ExpectedResult { get; }

        public Task(int courseNumber, int homeworkNumber, int taskNumber, string name, string inputRule, string? expectedResult = null)
        {
            CourseNumber = courseNumber;
            HomeworkNumber = homeworkNumber;
            TaskNumber = taskNumber;
            Name = name;
            InputRule = inputRule;
            ExpectedResult = expectedResult;
        }

        /// <summary>
        /// Описание задачи.
        /// </summary>
        /// <returns>
        /// Строка, содержащая правило ввода и описание ожидаемого результата (опционально).
        /// </returns>
        public string Description()
        {
            return ExpectedResult != null ? 
                $"{InputRule} и {ExpectedResult}" : 
                $"{InputRule}";
        }

        public void ErrorDescription()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{InputRule}");
            Console.ForegroundColor = ConsoleColor.Black;
        }
    }
}