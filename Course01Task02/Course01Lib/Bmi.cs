using System;

namespace Course01Lib;

public class Bmi {

    #region Variables and constants
    /// <summary>
    /// Значение индекса.
    /// </summary>
    public double Value { get; }
    /// <summary>
    /// Описание индекса.
    /// </summary>
    public string Description { get; set; }
    /// <summary>
    /// Оптимальная масса тела в килограммах.
    /// </summary>
    private double OptimalBodyWeight { get;  }
    /// <summary>
    /// Необходимые изменения до нормы в кг.
    /// </summary>
    /// <remarks>
    /// Если число положительное, надо поправиться, если отрицательное - похудеть.
    /// </remarks>
    private double NecessaryСhanges { get; }
    /// <summary>
    /// Рекомендация по питанию.
    /// </summary>
    public string Recomendation { get;  }
    #endregion

    #region Initializers and Deinitializer
    /// <summary>
    /// Конструктор класса.
    /// </summary>
    /// <param name="weight">Вес человека в килограммах.</param>
    /// <param name="height">Рост человека в метрах.</param>
    public Bmi(double height, double weight)
    {
        Value = Math.Round(weight / (height * height), 2) ;
        
        FillDescription();

        // Здесь 25.0 - верхнее значение нормы 
        OptimalBodyWeight = Math.Round(25.0 *  Math.Pow(height, 2), 2);
        
        NecessaryСhanges = OptimalBodyWeight - weight;

        Recomendation = SetRecomendation();
    }
    #endregion

    #region Methods
    /// <summary>
    /// Заполняет описание индекса на основе значения.
    /// </summary>
    private void FillDescription()
    {
        switch (Value)
        {
            case > 0 and <= 16.0:
                Description = "Выраженный дефицит массы тела";
                break;
            case > 16.0 and <= 18.5:
                Description = "Недостаточная (дефицит) масса тела";
                break;
            case > 18.5 and <= 25.0:
                Description = "Норма";
                break;
            case > 25.0 and <= 30.0:
                Description = "Избыточная масса тела (предожирение)";
                break;
            case > 30.0 and <= 35.0:
                Description = "Ожирение первой степени";
                break;
            case > 35.0 and <= 40.0:
                Description = "Ожирение второй степени";
                break;
            case > 40.0:
                Description = "Ожирение третьей степени (морбидное)";
                break;
        }
    }

    /// <summary>
    /// Определяет рекомендации по питанию.
    /// </summary>
    /// <returns></returns>
    private string SetRecomendation()
    {
        if (Value is > 18.5 and <= 25.0)
        {
            return "Отличный вес, так держать!";
        }
        else
        {
            double necessaryValue = Math.Abs(Math.Round(NecessaryСhanges,2));
            string action = Math.Sign((int) NecessaryСhanges) == 1 ? "поправиться" : "похудеть";

            return $"Оптимальный вес: {OptimalBodyWeight}, рекомендуется {action} на {necessaryValue} кг.";
        }
    }
    #endregion
}