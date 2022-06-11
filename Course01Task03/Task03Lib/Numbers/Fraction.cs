using System;
using System.Data.Common;

namespace Task03Lib; 

public class Fraction {
    /// <summary>
    /// Числитель (верхнее число в дроби).
    /// </summary>
    public int Num { get; } // Сделал публичным так как так понял условие реализации в задании, по-хорошему оно private  и вообще поле, потому что извне оно нам не нужно

    /// <summary>
    /// Знаменатель (верхнее число в дроби).
    /// </summary>
    public int Denom { get; } // Сделал публичным так как так понял условие реализации в задании, по-хорошему оно private и вообще поле, потому что извне оно нам не нужно

    /// <summary>
    /// Десятичная дробь.
    /// </summary>
    public double DecFraction => GetDecFraction();

    public Fraction(int num, int denom)
    {
        Num = num;
        Denom = denom;
    }

    public override string ToString()
    {
        return $"{Num}/{Denom}";
    }

    /// <summary>
    /// Метод сложения дробей.
    /// </summary>
    /// <param name="f1">Первая дробь.</param>
    /// <param name="f2">Вторая дробь.</param>
    /// <returns>Новая дробь как результат сложения.</returns>
    public static Fraction Addition(Fraction f1, Fraction f2)
    {
        var fraction = new Fraction((f1.Num * f2.Denom + f2.Num * f1.Denom), f1.Denom * f2.Denom);
        return ReturnResult(fraction);
    }

    /// <summary>
    /// Метод вычитания дробей.
    /// </summary>
    /// <param name="f1">Первая дробь.</param>
    /// <param name="f2">Вторая дробь.</param>
    /// <returns>Новая дробь как результат вычитания.</returns>
    public static Fraction Subtraction(Fraction f1, Fraction f2)
    {
        var fraction = new Fraction((f1.Num * f2.Denom - f2.Num * f1.Denom), 
            f1.Denom * f2.Denom);
        return ReturnResult(fraction);
    }
    
    /// <summary>
    /// Метод умножения дробей.
    /// </summary>
    /// <param name="f1">Первая дробь.</param>
    /// <param name="f2">Вторая дробь.</param>
    /// <returns>Новая дробь как результат умножения.</returns>
    public static Fraction Multiplication(Fraction f1, Fraction f2)
    {
        var fraction = new Fraction(f1.Num * f2.Num, f1.Denom * f2.Denom);
        return ReturnResult(fraction);
    }

    /// <summary>
    /// Метод деления дробей.
    /// </summary>
    /// <param name="f1">Первая дробь.</param>
    /// <param name="f2">Вторая дробь.</param>
    /// <returns>Новая дробь как результат деления.</returns>
    public static Fraction Division(Fraction f1, Fraction f2)
    {
        var fraction = new Fraction(f1.Num * f2.Denom, f1.Denom * f2.Num);
        return ReturnResult(fraction);
    }
    
    /// <summary>
    /// Возвращает полученный результат, пытаясь упросить (сократить) дробь.
    /// </summary>
    /// <param name="fraction">Полученная дробь.</param>
    /// <returns>Упрощённая (сокращённая) или полученная дробь.</returns>
    private static Fraction ReturnResult(Fraction fraction)
    {
        var commonDivisor = GetCommonDivisor(fraction.Num, fraction.Denom);
        return commonDivisor > 0 ? 
            new Fraction(fraction.Num/commonDivisor,fraction.Denom/commonDivisor) : 
            fraction;
    }
    
    /// <summary>
    /// Находит общий делитель.
    /// </summary>
    /// <param name="i">Первое число.</param>
    /// <param name="j">Второе число.</param>
    /// <returns>Общий делитель.</returns>
    private static int GetCommonDivisor(int i, int j)
    {
        while (i != j)
        {
            if (i > j)  
                i -= j;
            else        
                j -= i;
        }
        return i;
    }

    /// <summary>
    /// Формирует десятичную дробь из обычной.
    /// </summary>
    /// <returns></returns>
    private double GetDecFraction()
    {
        
        var decFraction = (decimal)Num / Denom;
        
        NumbersHelper nh = NumbersHelper.GetInstance();
        var numberDigits = nh.GetNumberOfDecimalPlaces(decFraction);
        
        // Предполагаем, что десятичная дробь бесконечная.
        if (numberDigits.fracPartCount > 4)
        {
            decFraction = Math.Round(decFraction, 4);
        }

        return (double)decFraction;
    }
}