using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace Task04Lib.CustomArrays
{
    public static class StaticClass {

    #region Variables and constants
    private static string FilePath { get; } = AppDomain.CurrentDomain.BaseDirectory + "someArray.txt";
    #endregion
    
    #region Methods
    /// <summary>
    /// Ищет пары чисел в переданном массиве, в которых только одно число делится на делитель.
    /// </summary>
    /// <param name="array">Переданный массив.</param>
    /// <exception cref="DivideByZeroException">Ошибка деления на 0</exception>
    public static void GetPairNumsOneOfWhichIsDivide(int[] array)
    {
        // Основная операция
        Console.Clear();
        Console.WriteLine("=======Основная операция=======");
        Console.WriteLine("Сгенерирован массив:");
        Print(array);
        
        int divider;
        while (true)
        {
            Console.Write("Введите делитель: ");
            try
            {
                string input = Console.ReadLine();
                divider = int.Parse(input);
                if (divider == 0)
                    throw new DivideByZeroException("На 0 делить нельзя, повторите");
                break;
            }
            catch (FormatException e)
            {
                ErrorDescription($"{e.Message}\nНекорректный формат целого числа, повторите");
            }
            catch (DivideByZeroException divideByZero)
            {
                ErrorDescription(divideByZero.Message);
            }
        }

        var pairs = new List<(int f, int s)>(5);
        int i = default, count = default;
        while (i + 1 != array.Length)
        {
            if (array[i] % divider == 0 && array[i + 1] % divider != 0)
            {
                pairs.Add((array[i], array[i + 1])); count++;
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
        Console.WriteLine("====Дополнительная операция====");
        ConsoleKey nextOperationKey; 
        var validKeys = new List<ConsoleKey> 
        {
            ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.Spacebar, ConsoleKey.Backspace
        };

        do
        {
            Console.WriteLine($"1: Записать полученный массив в файл\n" +
                              $"2: Прочитать другой массив из файла\n" +
                              $"Spacebar: Повторить задачу\n" +
                              $"Backspace: Выйти");
            nextOperationKey = Console.ReadKey().Key;

        } while (!validKeys.Contains(nextOperationKey));
        
        switch (nextOperationKey)
        {
            case ConsoleKey.D1:
                WriteArrayFromFile(array);
                break;
            case ConsoleKey.D2:
                ReadArrayFromfile();
                break;
            case ConsoleKey.Spacebar:
                GetPairNumsOneOfWhichIsDivide(array);
                break;
        }
    }
    
    /// <summary>
    /// Записывает массив целых чисел в текстовый файл.
    /// </summary>
    /// <param name="array">Переданный массив для записи.</param>
    /// <exception cref="FileNotFoundException">Исключение - отсутствие файла для записи.</exception>
    private static void WriteArrayFromFile(int[] array)
    {
        try
        {
            if (!File.Exists(FilePath))
                throw new FileNotFoundException();
        }
        catch (FileNotFoundException e)
        {
            ConsoleKey selectedKey;
            do
            {
                Console.Clear();
                Console.WriteLine($"{e.Message}\nФайла для записи нет на диске. Создать новый файл? (y/n)\n");
                selectedKey = Console.ReadKey().Key;
            } while (selectedKey != ConsoleKey.Y && selectedKey != ConsoleKey.N);

            switch (selectedKey)
            {
                case ConsoleKey.Y:
                    var fileCreateStream = new FileStream(FilePath, FileMode.CreateNew);
                    fileCreateStream.Close();
                    WriteArrayFromFile(array);
                    break;
                case ConsoleKey.N:
                    break;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Другое исключение: {e.Message}");
        }

        StreamWriter writer = new StreamWriter(FilePath, false);
        foreach (var item in array) 
        {
            writer.WriteLineAsync(item.ToString());
        }
        writer.Close();
        Console.Clear();
        Console.WriteLine("Массив успешно записан в файл.");
        Thread.Sleep(2000);
    }
    
    /// <summary>
    /// Читает ранее записанный массив целых чисел из локального файла.
    /// </summary>
    /// <exception cref="FileNotFoundException">Исключение - отсутствие файла для чтения.</exception>
    private static void ReadArrayFromfile()
    {
        try
        {
            if (!File.Exists(FilePath))
                throw new FileNotFoundException();
        }
        catch (FileNotFoundException e)
        {
            Console.WriteLine($"{e.Message}\nФайла для чтения на диске нет. Сначала запишите массив в файл.\n");
            return;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Другое исключение: {e.Message}");
        }
        
        var bufferArray = new int[1000];

        var reader = new StreamReader(FilePath);
        var i = 0;
        while (!reader.EndOfStream)
        {
            if(int.TryParse(reader.ReadLine(), out var number)) 
                bufferArray[i] = number;
            i++;
        }
        reader.Close();
        
        Array.Resize(ref bufferArray, i);
        Console.WriteLine(bufferArray.Length);
        Console.WriteLine("Из файла прочитан массив чисел:");
        Print(bufferArray);
        Thread.Sleep(2000);
        Console.Clear();
    }
    
    /// <summary>
    /// Распечатывает сообщение об ошибке.
    /// </summary>
    /// <param name="errorString">Строка с ошибкой.</param>
    private static void ErrorDescription(string errorString)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(errorString);
        Console.ForegroundColor = ConsoleColor.Black;
    }
    
    /// <summary>
    /// Распечатывает содержимое массива. 
    /// </summary>
    /// <param name="array">Переданный массив целых чисел.</param>
    private static void Print(int[] array)
    {
        foreach (var item in array)
            Console.Write(item == array.Last() ? $"{item}" : $"{item} ");
        Console.WriteLine();
    }
    #endregion
}
}

