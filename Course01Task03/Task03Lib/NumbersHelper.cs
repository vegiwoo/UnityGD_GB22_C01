using System; 

namespace Task03Lib; 

public class NumbersHelper {
    private static readonly NumbersHelper Instance = new NumbersHelper();

    public static NumbersHelper GetInstance()
    {
        return Instance;
    }
    
    /// <summary>
    /// Определяет количество знаков для целой и дробной частей в числе с плавающей точкой.
    /// </summary>
    /// <param name="number">Исходное число с плавающей точкой.</param>
    /// <returns>Кортеж с количеством знаков для целой и дробной части.</returns>
    public (int intPartCount, int fracPartCount) GetNumberOfDecimalPlaces(decimal number)
    {
        // Отделяем целую часть
        var intPart = (int)Math.Truncate(number);
        
        // Количество знаков целой части получаем по десятичному логарифму
        int intPartNumbersCount = (int)Math.Log10(Math.Abs(intPart)) + 1; 
        
        // Отделяем дробную часть
        var fracPart = number - intPart;
        
        Console.WriteLine($"Целая часть {intPart} дробная часть {fracPart}");
        
        // Число знаков в дробной части получаем с помощью BitConverter
        var fracPartNumbersCount = BitConverter.GetBytes(decimal.GetBits(fracPart)[3])[2];
        
        return (intPartNumbersCount, fracPartNumbersCount);
    }
}