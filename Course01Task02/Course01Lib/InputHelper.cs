using System;

namespace Course01Lib; 

public class InputHelper {
    private static readonly InputHelper Instance = new InputHelper();
    
    public static InputHelper GetInstance()
    {
        return Instance;
    }

    /// <summary>
    /// Ожидает дальнейшие действия пользователя после решения задачи.
    /// </summary>
    /// <returns>Нажатая клавиша.</returns>
    public static ConsoleKey WaitUserAction()
    {
        ConsoleKey key = default;
        
        while (key != ConsoleKey.Spacebar && key != ConsoleKey.Backspace)
        {
            Console.WriteLine($"Повторить: Spacebar, вернуться в меню программ: Backspace");
            key = Console.ReadKey().Key;
        }
        
        Console.Clear();
        return key;
    }
}