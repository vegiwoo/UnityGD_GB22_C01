#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Task04Lib.CustomArrays {
    public class OneDimIntArray : CustomArray {
        
        #region Variables and constants
        protected override string FileName { get; set; } = "oneDimIntArray.txt";
        
        /// <summary>
        /// Внутренний одномерный массив.
        /// </summary>
        private readonly int[] internalArray;
        #endregion

        #region Properties
        /// <summary>
        /// Элемент массива по индексу.
        /// </summary>
        /// <param name="index">Индекс элемента.</param>
        public int this[int index]
        {
            get => internalArray[index];
            set => internalArray[index] = value;
        }
        public override int Length => internalArray.Length;
        #endregion

        #region Initializers and deinitializer 
        /// <summary>
        /// Конструктор, принимающий существующий массив элементов.
        /// </summary>
        /// <param name="existingArray">Переданный массив.</param>
        public OneDimIntArray(int[] existingArray)
        {
            internalArray = existingArray;
        }
        /// <summary>
        /// Конструктор для генерации массива случайных чисел заданной длины и в диапазоне.
        /// </summary>
        /// <param name="length">Длина создаваемого массива.</param>
        /// <param name="limits">Нижняя и верхняя границы чисел для генерации массива.</param>
        public OneDimIntArray(int length, (int lowerLimit, int upperLimit) limits )
        {
            internalArray = new int[length];
            for (int i = 0; i < length; i++)
            {
                internalArray[i] = randomizer.Next(limits.lowerLimit, limits.upperLimit + 1);
            }
        }
        /// <summary>
        /// Конструктор для генерации массива чисел заданной длины, с начальным значением и шагом.
        /// </summary>
        /// <param name="length">Длина массива.</param>
        /// <param name="step">Шаг при заполнении массива.</param>
        /// <param name="initalValue">Начальное значение массива по индексу 0</param>
        public OneDimIntArray(int length, int step, int initalValue)
        {
            this.internalArray = new int[length];
            var value = initalValue;
           
            for (int i = 0; i < length; i++)
            {
                internalArray[i] = value;
                value += step;
            }
        }
        /// <summary>
        /// Конструктор для загрузки массива из файла.
        /// </summary>
        /// <param name="fileName">Имя файла с расширением для загрузки массива.</param>
        public OneDimIntArray(string fileName)
        {
            var path = BasePath + fileName;
            internalArray =  ReadArrayFromfile(path);
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            var result = string.Empty;
            var last = internalArray.Last();
            for (int i = 0; i < Length; i++)
            {
                result += $"{internalArray[i]}";
                if(internalArray[i] == last) 
                    result += ", ";
            }
            return result;
       }
        
        public override void ToSummary()
        {
            Console.WriteLine("==========================");
            Print(internalArray);
            Console.WriteLine($"Элементов в массиве: {Length}");
            Console.WriteLine($"Сумма всех элементов: {Sum}");
            // Находим рандомный элемент, чтобы получить сумму чисел больше него:
            var randomGivenNumber = GetRandomElement();
            Console.WriteLine($"Сумма элементов больше {randomGivenNumber}: {GetSum(randomGivenNumber)}");
            var minCount = MinCount();
            Console.WriteLine($"Минимальное значение: {minCount.value}, встречается раз: {minCount.count}");
            var maxCount = MaxCount();
            Console.WriteLine($"Максимальное значение: {maxCount.value}, встречается раз: {maxCount.count}");
            Console.WriteLine($"Массив с инверсией знака:");
            Print(Inverse());
            Console.WriteLine($"Массив со значениями х 2:");
            Print(Multi(2));
            var frequency = FrequencyOccurrenceElements();
            Console.WriteLine($"Вхождение элементов:");
            foreach (var item in frequency)
            {
                Console.WriteLine($"Элемент: {item.Key}, вхождений: {item.Value}");
            }
            Console.WriteLine("==========================");
        }

        /// <summary>
        /// Читает ранее записанный массив целых чисел из локального файла.
        /// </summary>
        /// <param name="filePath">Путь к файлу.</param>
        /// <exception cref="FileNotFoundException">Исключение - отсутствие файла для чтения.</exception>
        /// <returns>Загруженный массив целых чисел.</returns>>
        private int[] ReadArrayFromfile(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                    throw new FileNotFoundException();
            }
            catch (FileNotFoundException e)
            {
                ErrorDescription($"{e.Message}\nФайла для чтения на диске нет. Сначала запишите массив в файл.\n");
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message}");
                throw;
            }
        
            var bufferArray = new int[1000];

            var reader = new StreamReader(filePath);
            var i = 0;
            while (!reader.EndOfStream)
            {
                if(int.TryParse(reader.ReadLine(), out var number)) 
                    bufferArray[i] = number;
                i++;
            }
            reader.Close();
        
            Array.Resize(ref bufferArray, i);
            return bufferArray;
        }

        protected override int GetSum() => internalArray.Sum();
        protected override int GetSum(int givenNumber)
        {
            return internalArray
                .Where(el => el > givenNumber)
                .Sum();
        }

        /// <summary>
        /// Меняет знак каждого числа в массиве и возвращает как новый массив.
        /// </summary>
        /// <returns>Новый массив с инвертированным знаком.</returns>
        private int[] Inverse()
        {
            return internalArray
                .Select(el => -el)
                .ToArray();
        }
        
        /// <summary>
        /// Умножает заданный элемент массива на указанную величину.
        /// </summary>
        /// <param name="multiplier">Множитель.</param>
        /// <returns></returns>
        private int[] Multi(int multiplier)
        {
            return internalArray
                .Select(el => el * multiplier)
                .ToArray();
        }

        protected override (int value, int count) MinCount()
        {
            var min = internalArray.Min();
            var count= internalArray
                .Count(el => el == min);
            return (min, count);
        }

        protected override (int value, int count, (int row, int column)[]? indexes) MaxCount()
        {
            var max = internalArray.Max();
            var count= internalArray
                .Count(el => el == max);
            return (max, count, null);
        }
        /// <summary>
        /// Определяет число вхождений элементов в массив.
        /// </summary>
        /// <returns>Словарь, где ключ-элемент массива, значение</returns>
        private Dictionary<int, int> FrequencyOccurrenceElements()
        {
            var result = new Dictionary<int, int>();
            var keys = internalArray.Distinct();
            foreach (var item in keys)
                result[item] = internalArray.Count(el => el == item);
            return result
                .OrderByDescending(dict => dict.Value)
                .ToDictionary(dict => dict.Key, dict => dict.Value);
        }
        protected override int GetRandomElement()
        {
            return internalArray[randomizer.Next(0, Length)];
        }
        #endregion
    }
}
