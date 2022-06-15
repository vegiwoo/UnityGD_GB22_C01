#nullable enable
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace Task04Lib.CustomArrays
{
    public class TwoDimIntArray: CustomArray {
        #region Variables and constants
        protected sealed override string FileName { get; set; } = "twoDimIntArray.txt";
        /// <summary>
        /// Внутренний двумерный массив.
        /// </summary>
        private readonly int[,] internalArray;
        #endregion
        
        #region Properties
        public int this[int row, int column]
        {
            get => internalArray[row, column];
            set => internalArray[row, column] = value;
        }
        /// <summary>
        /// Длина массива.
        /// </summary>
        public override int Length => internalArray.Length;
        /// <summary>
        /// Сглаженная версия двумерного массива;
        /// </summary>
        private int[] Flatten => FlattenTwoDimArray();
        #endregion
        
        #region Initializers and deinitializer
        /// <summary>
        /// Конструктор для генерации массива случайных чисел заданного объёма (строки и столбцы) и в заданном диапазоне.
        /// </summary>
        /// <param name="volume">Объём двумерного массива (строки и столбцы).</param>
        /// <param name="limits">Диапазон с указанием нижней и верхней границы</param>
        public TwoDimIntArray((int rows, int columns) volume, (int lowerBound, int upperBound) limits)
        {
            internalArray = new int[volume.rows, volume.columns];
            for (var i = 0; i < internalArray.GetLength(0); i++)
                for (var j = 0; j < internalArray.GetLength(1); j++)
                    internalArray[i,j] = randomizer.Next(limits.lowerBound, limits.upperBound + 1);
        }
        
        /// <summary>
        /// Конструктор по-умолчанию используется для загрузки двумерного массива из файла.
        /// </summary>
        public TwoDimIntArray()
        {
            var path = BasePath + FileName;
            var readArray  = ReadArrayFromfile();
            if (readArray != null) 
                internalArray = readArray;
            else
            {
                throw new Exception($"Не удалось инициализировать экземпляр типа TwoDimIntArray!");
            }
        }
        #endregion
        
        #region Methods
        public override void ToSummary()
        {
            Console.WriteLine("==========================");
            Print(internalArray);
            Console.WriteLine($"Сумма всех элементов: {Sum}");
            // Находим рандомный элемент, чтобы получить сумму чисел больше него:
            var randomGivenNumber = GetRandomElement();
            Console.WriteLine($"Сумма элементов больше {randomGivenNumber}: {GetSum(randomGivenNumber)}");
            Console.WriteLine($"Минимальное значение: {Min.value}, встречается раз: {Min.count}");
            Console.WriteLine($"Максимальное значение: {Max.value}, встречается раз: {Max.count}");
            if (Max.indexes != null)
            {
                Console.WriteLine("Индексы максимального значения (отсчет от 0!):");
                for (int i = 0; i < Max.indexes.Length; i++)
                {
                    var print = $"[{Max.indexes[i].row},{Max.indexes[i].column}]";
                    if (i == Max.indexes.Length - 1)
                        print += "\n";
                    else
                    {
                        print += ", ";
                    }
                    Console.Write(print);
                }
            }

            Console.WriteLine("Записываю массив в локальный файл...");
            WriteArrayToFile(internalArray);
        }
        /// <summary>
        /// Сглаживает двумерный массив до одномерного.
        /// </summary>
        /// <returns>Одномерный массив.</returns>
        private int[] FlattenTwoDimArray()
        {
            var flatten = from int item in internalArray
                select item;
            return flatten.ToArray();
        }
        protected override int GetSum()
        {
            return Flatten.Sum();
        }
        protected override int GetSum(int givenNumber)
        {
            return Flatten.Where(el => el > givenNumber).Sum();
        }
        protected override (int value, int count) MinCount()
        {
            var min = Flatten.Min();
            var count= Flatten.Count(el => el == min);
            return (min, count);
        }
        protected override (int value, int count, (int row, int column)[]? indexes) MaxCount()
        {
            var max = Flatten.Max();
            var count= Flatten.Count(el => el == max);
            // Получаем индексы макс.элемента
            var indexes = FindIndexesOfValue(max, in internalArray);
            return (max, count, indexes);
        }
        /// <summary>
        /// Ищет все индексы вхождения элемента двумерного массива. 
        /// </summary>
        /// <param name="value">Элемент.</param>
        /// <param name="array">Исходный массив как ссылочный параметр.</param>
        /// <returns></returns>
        private static (int row, int column)[] FindIndexesOfValue(int value, in int [,] array)
        {
            var indexes = new List<(int, int)>();
            for (int r = 0; r < array.GetLength(0); r++)
            {
                for (int c = 0; c < array.GetLength(1); c++)
                {
                    if (array[r,c] == value)
                        indexes.Add((r,c));
                }
            }
            return indexes.ToArray();
        }
        protected override int GetRandomElement()
        {
            return Flatten[randomizer.Next(0, Length)];
        }
        /// <summary>
        /// Записывает двумерный массив в файл.
        /// </summary>
        /// <param name="array"></param>
        /// <exception cref="FileNotFoundException"></exception>
        private  void WriteArrayToFile(int[,] array)
        {
            var filePath = BasePath + FileName;
            if (CheckFileWithArray(filePath))
            {
                using (var writer = new StreamWriter(filePath, false))
                {
                    var rowLength = internalArray.GetLength(0);
                    var columnLength = internalArray.GetLength(1);
                
                    for (int r = 0; r < rowLength; r++)
                    {
                        for (int c = 0; c < columnLength; c++)
                        {
                            var item = internalArray[r, c].ToString();
                            item += c + 1 == columnLength ? "\n" : " ";
                            writer.Write(item);
                        }
                    }
                }
                Console.WriteLine("Двумерный массив успешно записан в файл.");
            }
            Thread.Sleep(2000);
        }
        /// <summary>
        /// Читает двумерный массив из файла.
        /// </summary>
        /// <returns>Полученный двумерный массив (опционально).</returns>
        private int[,]? ReadArrayFromfile()
        {
            var filePath = BasePath + FileName;
            if (!CheckFileWithArray(filePath)) return null;
            
            var newArray = new int[100,100];
            int rows = default;
            int cols = default;
            
            using (var reader = new StreamReader(filePath))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (string.IsNullOrEmpty(line)) continue;
                        
                    var substrings = line.TrimEnd().Split(' ');
                    if (cols == 0) cols = substrings.Length;
                    for (var i = 0; i < substrings.Length; i++)
                        if(int.TryParse(substrings[i], out var number))
                            newArray[rows, i] = number;
                    rows++;
                }
            }

            ResizeArray(ref newArray, rows, cols);
            return newArray;
        }
        /// <summary>
        /// Изменяет размер двумерного массива.
        /// </summary>
        /// <param name="original">Оригинальный двумерный массив.</param>
        /// <param name="newRowsNumber">Новое количество строк.</param>
        /// <param name="newColumsNumber">Новое количество колонок.</param>
        /// <typeparam name="T">Тип элемента массива.</typeparam>
        private static void ResizeArray<T>(ref T[,] original, int newRowsNumber, int newColumsNumber)
        {
            var result = new T[newRowsNumber,newColumsNumber];
            for (var row = 0; row < newRowsNumber; row++)    
                for (int col = 0; col < newColumsNumber; col++)
                    result[row, col] = original[row, col];
            original = result;
        }
        #endregion
    }
}