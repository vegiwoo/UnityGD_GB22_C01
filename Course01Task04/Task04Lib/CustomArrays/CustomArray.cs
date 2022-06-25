#nullable enable
using System;
using System.IO;

namespace Task04Lib.CustomArrays {
    public abstract class CustomArray {
    #region Variables and constants
    protected readonly Random randomizer = new Random();
    protected abstract string FileName { get; set; }
    protected static string BasePath => AppDomain.CurrentDomain.BaseDirectory;
    #endregion

    #region Properties
    /// <summary>
    /// Длина массива.
    /// </summary>
    public abstract int Length { get; }

    /// <summary>
    /// Сумма всех элементов массива.
    /// </summary>
    public int Sum => GetSum();
    
    /// <summary>
    /// Минимальное значение в массиве и количество вхождений.
    /// </summary>
    public (int value, int count) Min => MinCount();
    /// <summary>
    /// Максимальное значение в массиве и количество вхождений.
    /// </summary>
    public (int value, int count, (int row, int column)[]? indexes) Max => MaxCount();
    #endregion

    #region Methods
    /// <summary>
    /// Распечатывает одномерный массив.
    /// </summary>
    /// <param name="array1Dem">Одномерный массив для вывода на печать.</param>
    protected void Print(int[] array1Dem)
    {
        for (var i = 0; i < Length; i++)
            Console.Write(array1Dem[i] + " ");
        Console.WriteLine();
    }
    /// <summary>
    /// Распечатывает двумерный массив.
    /// </summary>
    /// <param name="array2Dem">Двумерный массив для вывода на печать.</param>
    protected static void Print(int[,] array2Dem)
    {
        for (var r = 0; r < array2Dem.GetLength(0); r++)
        {
            for (var c = 0; c < array2Dem.GetLength(1); c++) 
            {
                Console.Write(array2Dem[r,c] + "\t");
            }
            Console.Write("\n");
        }
        Console.WriteLine();
    }
    /// <summary>
    /// Распечатывает сводку по массиву.
    /// </summary>
    public abstract void ToSummary();
    /// <summary>
    /// Возвращает сумму всех элементов массива. 
    /// </summary>
    /// <returns></returns>
    protected abstract int GetSum();
    /// <summary>
    /// Возвращает сумму чисел больше заданного.
    /// </summary>
    /// <param name="givenNumber">Заданное число.</param>
    /// <returns></returns>
    protected abstract int GetSum(int givenNumber);
    /// <summary>
    /// Возвращает минимальный элемент массива.
    /// </summary>
    /// <returns>Минимальный элемент и количество вхождений.</returns>
    protected abstract (int value, int count) MinCount();
    /// <summary>
    /// Возвращает максимальный элемент массива.
    /// </summary>
    /// <returns>Максимальный элемент, количество вхождений и индексы вхождения (реализовано только для двумерных массивов).</returns>
    protected abstract (int value, int count, (int row, int column)[]? indexes) MaxCount();
    /// <summary>
    /// Возвращает случайный элемент массива.
    /// </summary>
    /// <returns>Найденный элемент.</returns>
    protected abstract int GetRandomElement();
    /// <summary>
    /// Распечатывает сообщение об ошибке.
    /// </summary>
    /// <param name="errorString">Строка с ошибкой.</param>
    protected static void ErrorDescription(string errorString)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(errorString);
        Console.ForegroundColor = ConsoleColor.Black;
    }

    /// <summary>
    /// Проверяет наличие локального файла для записи или чтения массива.
    /// </summary>
    /// <param name="filePath">Путь к файлу.</param>
    /// <returns></returns>
    /// <exception cref="FileNotFoundException">Исключение: Файла нет на диске.</exception>
    protected static bool CheckFileWithArray(string filePath)
    {
        try
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException();
        }
        catch (FileNotFoundException e)
        {
            ConsoleKey selectedKey;
            do
            {
                ErrorDescription($"{e.Message}\nФайла для работы с массивом не найдено. Создать новый файл? (y/n)\n");
                selectedKey = Console.ReadKey().Key;
            } while (selectedKey != ConsoleKey.Y && selectedKey != ConsoleKey.N);

            switch (selectedKey)
            {
                case ConsoleKey.Y:
                    var fileCreateStream = new FileStream(filePath, FileMode.CreateNew);
                    fileCreateStream.Close();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Файл для работы с массивом успешно создан");
                    Console.ForegroundColor = ConsoleColor.Black;
                    return true;
                case ConsoleKey.N:
                    break;
            }
        }
        catch (Exception e)
        {
            ErrorDescription($"Возникла ошибка: {e.Message}");
        }
        return true;
    }
    #endregion
}
    
}