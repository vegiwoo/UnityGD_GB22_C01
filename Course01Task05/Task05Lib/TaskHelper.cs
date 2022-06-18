using System;
using System.Collections.Generic;
using System.Linq;

namespace Task05Lib {
    public static class TaskHelper {
        /// <summary>
        /// Предоставляет пользователю программы выбор задачи для выполнения.
        /// </summary>
        /// <param name="Tasks">Словарь задач домашней работы.</param>
        /// <returns>Нажатая клавиша и выбранная задача.</returns>
        public static (ConsoleKey keyInfo, Task task) ChoiseTask(Dictionary<ConsoleKeyInfo, Task> Tasks)
        {
            var validKeys = Tasks.Keys
                .Select(k => k.Key)
                .ToList();
    
            ConsoleKey selectedKey;
            do 
            {
                Console.Clear();
                Console.WriteLine("Выберите номер задачи:");
                    
                foreach (var task in Tasks)
                {
                    Console.WriteLine($"{task.Value.TaskNumber}: {task.Value.Name}");
                }
                Console.WriteLine("ИЛИ нажмите Backspace для выхода из программы");
                selectedKey = Console.ReadKey().Key;
            } while (!validKeys.Contains(selectedKey) && selectedKey != ConsoleKey.Backspace);
    
            var selectedTask = Tasks
                .FirstOrDefault(el => el.Key.Key == selectedKey);
                
            return (selectedKey, selectedTask.Value);
        }
        /// <summary>
        /// Ожидает дальнейшие действия пользователя после решения задачи.
        /// </summary>
        /// <returns>Нажатая клавиша.</returns>
        /// <remarks>Ожидает нажания Spacebar или Backspace</remarks>>
        public static ConsoleKey WaitUserAction()
        {
            ConsoleKey key = default;
            while (key != ConsoleKey.Spacebar && key != ConsoleKey.Backspace)
            {
                Console.WriteLine($"ПОВТОРИТЬ задачу: Spacebar, ВЕРНУТЬСЯ в меню программ: Backspace");
                key = Console.ReadKey().Key;
            }
            
            Console.Clear();
            return key;
        }
    }
}
