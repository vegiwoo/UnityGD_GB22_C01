using System;

namespace Task04Lib; 

public class InputHelper {
    private static readonly InputHelper Instance = new InputHelper();
    
    public static InputHelper GetInstance() => Instance;
    
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