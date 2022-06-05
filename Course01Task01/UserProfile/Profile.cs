using System;

namespace UserProfile {
    /// <summary>
    ///  Профиль пациента.
    /// </summary>
    public class Profile {
        #region Constants for Numeric Values

        public const int MinAge = 10;
        public const int MaxAge = 100;
        public const float MinHeight = 50;
        public const float MaxHeight = 250;
        public const float MinWeight = 40;
        public const float MaxWeight = 250;

        #endregion

        #region Rules for numeric values

        public static readonly string AgeEntryRule = $"целое число от {MinAge} до {MaxAge}";

        public static readonly string HeightEntryRule =
            $"целое или число, разделенное знаком ',' - от {MinHeight} до {MaxHeight}";

        public static readonly string WeightEntryRule =
            $"целое или число, разделенное знаком ',' - от {MinWeight} до {MaxWeight}";

        #endregion

        #region Private Properties

        private string FirstName { get; }
        private string LastName { get; }
        private int Age { get; }
        private float Height { get; }
        private float Weight { get; }

        #endregion
        public Profile(string firstName, string lastName, int age, float height, float weight)
        {
            FirstName = NameToUpper(firstName);
            LastName = NameToUpper(lastName);
            Age = age;
            Height = height;
            Weight = weight;

            PrintDescription(2); // Номер варианта распечатки хардкодим, он не влияет на пользователя программы.
        }

        /// <summary>
        /// Распечатывает данные анкеты.
        /// </summary>
        /// <param name="option">Номер метода распечатки - от 1 до 3</param>
        private void PrintDescription(int option)
        {
            switch (option)
            {
                case 1:
                    Console.Clear();
                    // Склеивание.
                    Console.WriteLine("Имя: " + FirstName +
                                      ", фамилия: " + LastName +
                                      ", возраст (лет): " + Age +
                                      ", рост (см): " + Height +
                                      ", вес (кг): " + Weight);
                    break;
                case 2:
                    Console.Clear();
                    // Форматированный вывод с флагами.
                    Console.WriteLine("Имя: {0}, фамилия: {1}, возраст (лет): {2}, рост (см): {3:F2}, " +
                                      "вес (кг): {4:F2}", FirstName, LastName, Age, Height, Weight);
                    break;
                case 3:
                    Console.Clear();
                    // Интерполяция с флагами.
                    Console.WriteLine($"Имя: {FirstName}, фамилия: {LastName}, возраст (лет): {Age}, " +
                                      $"рост (см): {Height:F2}, вес (кг): {Weight:F2}.");
                    break;
            }
        }

        /// <summary>
        /// Форматирует переданную строку в нижний регистр и использует верхний регистр для первой буквы.
        /// </summary>
        /// <param name="name">Исходная строка.</param>
        /// <returns>Отформатированная строка.</returns>
        private static string NameToUpper(string name)
        {
            var input = name.ToLower();
            return char.ToUpper(input[0]) + input.Substring(1);
        }
    }
}