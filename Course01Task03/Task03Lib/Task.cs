using System;

namespace Task03Lib {
    public class Task {
        /// <summary>
        /// Номер курса.
        /// </summary>
        private int courseNumber;

        /// <summary>
        /// Номер домашней работы.
        /// </summary>
        private int homeworkNumber;
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
        private string InputRule { get; }
        /// <summary>
        /// Ожидаемый результат решения задачи.
        /// </summary>
        private string? ExpectedResult { get; }

        public Task(int courseNumber, int homeworkNumber, int taskNumber, string name, string inputRule, string? expectedResult = null)
        {
            this.courseNumber = courseNumber;
            this.homeworkNumber = homeworkNumber;
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
        public string TaskDescription()
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