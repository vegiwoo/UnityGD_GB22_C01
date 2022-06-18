using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Task05Lib {
    public enum RegexKey {
        Login,
        OneCharisRus,
        WordRus
    }

    public static class Message {
        #region Variables and constants

        private const string LoginPattern = @"^(?=[a-zA-Z]{1,})+(?:\w{3,10})$";
        private const string OneCharIsRusPattern = @"^[а-яА-ЯеЁ]{1}$";
        private const string WordRusPattern = @"^[а-яА-Я]{1,}$";

        public static readonly Dictionary<RegexKey, Regex> Regexes = new Dictionary<RegexKey, Regex>
        {
            {
                RegexKey.Login, new Regex(LoginPattern)
            },
            {
                RegexKey.OneCharisRus, new Regex(OneCharIsRusPattern)
            },
            {
                RegexKey.WordRus, new Regex(WordRusPattern)
            }
        };

        /// <summary>
        /// Разделители для анализа текста.
        /// </summary>
        private static readonly char[] TextSeparators =
        {
            '!', '?', ':', ';', '.', ',', ' ', '-', '—', '\n', '\t'
        };

        #endregion

        #region Methods for solving tasks

        /// <summary>
        /// Выводит массив слов из сообщения не больше заданной длины.
        /// </summary>
        /// <param name="text">Исходный текст сообщения.</param>
        /// <param name="length">Заданная длина слов для поиска.</param>
        /// <returns></returns>
        public static IEnumerable<string> FindSpecificWords(in string text, int length)
        {
            return PreparingForAnalysis(text)
                .Where(w => w.Length <= length)
                .ToArray();
        }

        /// <summary>
        /// Удаляет из сообщения слова, заканчивающиеся на определённый символ.
        /// </summary>
        /// <param name="text">Исходный текст сообщения.</param>
        /// <param name="checkChar">Символ для поиска.</param>
        /// <returns></returns>
        public static IEnumerable<string> DeleteSpecificWords(in string text, char checkChar)
        {
            return PreparingForAnalysis(text)
                .Where(w => w.Last() != checkChar)
                .ToArray();
        }

        /// <summary>
        /// Находит самые длинные слова в тексте.
        /// </summary>
        /// <param name="text">Исходный текст сообщения.</param>
        /// <returns>Коллекция групп слов.</returns>
        public static IGrouping<int, string> FindMaxLengthWords(string text)
        {
            // Берем Last() тк по условию работы GetFrequencyOccurrenceWords в нем
            // содержится группа самых длинных элементов.
            return GetFrequencyOccurrenceWords(text).Last();
        }

        /// <summary>
        /// Находит самые длинные слова в тексте и возвращает строку из них.
        /// </summary>
        /// <param name="text">Исходный текст сообщения.</param>
        /// <returns>Полученная строка из найденных слов.</returns>
        /// <remarks>Для формирования строки применяется StringBuilder.</remarks>>
        public static string FindMaxLengthWordsAnaMakeString(string text)
        {
            var maxCountWords = FindMaxLengthWords(text);
            var builder = new StringBuilder(100);
            foreach (var word in maxCountWords)
                builder.Append($"{word} ");
            return builder.ToString();
        }

        /// <summary>
        /// Выполняет частотный анализ текста (число вхождений заданного слова в искомых текст).
        /// </summary>
        /// <param name="text">Исходный текст сообщения.</param>
        /// <param name="searchWords">Слова для поиска</param>
        public static IOrderedEnumerable<IGrouping<int, string>> GetFrequencyAnalysisOfText(string text,
            IEnumerable<string> searchWords)
        {
            var wArray = PreparingForAnalysis(text);
            return from searchWord in searchWords
                group searchWord by wArray.Count(w => w == searchWord)
                into g
                orderby g.Key
                select g;
        }

        #endregion

        #region Helper methods

        /// <summary>
        /// Преобразует строку в нижний регистр, обрезает пробелы и разбивает по разделителю.
        /// </summary>
        /// <param name="message">Исходная строка.</param>
        /// <returns>Коллекция строк.</returns>
        private static IEnumerable<string> PreparingForAnalysis(string message)
        {
            return message
                .ToLower()
                .Trim()
                .Split(TextSeparators, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// Проверяет один введённый символ на кириллицу.
        /// </summary>
        /// <param name="c">Символ для проверки.</param>
        /// <returns>Флаг проверки.</returns>
        public static bool isRus(char c)
        {
            return Regexes[RegexKey.OneCharisRus].IsMatch(c.ToString());
        }

        /// <summary>
        /// Находит вхождения слов в текст.
        /// </summary>
        /// <param name="text">Исходный текст</param>
        /// <returns>Перечисление групп найденных слов.</returns>
        private static IEnumerable<IGrouping<int, string>> GetFrequencyOccurrenceWords(string text)
        {
            var wArray = PreparingForAnalysis(text).ToArray();
            return from word in wArray
                group word by word.Length
                into g
                orderby g.Key
                select g;
        }

        #endregion
    }
}
