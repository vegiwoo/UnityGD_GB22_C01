#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

/// <summary>
/// Ученик средней школы.
/// </summary>
public class HighSchoolStudent {

    #region Variables and constants
    private static string scoreRegexPattern = @"^[1-5]$";
    private static Regex scoreRegex = new Regex(scoreRegexPattern);
    #endregion

    #region Properties
    /// <summary>
    /// Имя ученика
    /// </summary>
    private string FirstName { get; }
    /// <summary>
    /// Фамилия ученика
    /// </summary>
    private string LastName { get; }
    /// <summary>
    /// Оценки ученика по ЕГЭ
    /// </summary>
    private byte[] Scores { get; }
    /// <summary>
    /// Средний балл ЕГЭ
    /// </summary>
    private double GPA { get; }
    

    #endregion

    #region Initializers and deinitializer
    public HighSchoolStudent(string firstName, string lastName, byte[] scores)
    {
        FirstName = firstName;
        LastName = lastName;
        Scores = scores;
        GPA = Math.Round(scores.Average(b => (double) b), 2);
    }
    #endregion
    
    #region Methods
    public override string ToString()
    {
        return $"{LastName} {FirstName}, {Scores[0]} {Scores[1]} {Scores[2]}, средний балл: {GPA}";
    }
    public static HighSchoolStudent ParseStudent(string? inputString) 
    {
        if (inputString == null || string.IsNullOrEmpty(inputString))
            throw new Exception("Строка не может быть пустой.");

        var substring = inputString.Trim().Split(' ');
        if (substring.Count() < 5)
            throw new Exception("Данные не соответствуют шаблону ввода.");

        var lName = substring[0].ToLower();
        var fName = substring[1].ToLower();
        var scores = new List<byte>();
        
        for (var i = 2; i < substring.Length; i++)
        {
            if (byte.TryParse(substring[i], out var score) &&
                scoreRegex.IsMatch(score.ToString()))
                scores.Add(score);
            else
                throw new Exception("Оценка - целое число от 1 до 5");
        }

        var firstName = char.ToUpper(fName[0]) + fName.Substring(1);
        var lastName = char.ToUpper(lName[0]) + lName.Substring(1);

        return new HighSchoolStudent(firstName, lastName, scores.ToArray());
    }

    /// <summary>
    /// Группирует учеников по среднему баллу ЕГЭ от худшего к лучшему.
    /// </summary>
    /// <param name="students">Массив учеников.</param>
    /// <returns>Отсортированные группы.</returns>
    public static IOrderedEnumerable<IGrouping<double, HighSchoolStudent>> FindWorstStudents(HighSchoolStudent[] students)
    {
        return from student in students
            group student by student.GPA
            into g
            orderby g.Key
            select g;
    }
    #endregion 
}



