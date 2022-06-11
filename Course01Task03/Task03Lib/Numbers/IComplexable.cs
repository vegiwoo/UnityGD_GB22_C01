using System;

namespace Task03Lib {
    /// <summary>
    /// Интерфейс, представляющий комплексное число.
    /// </summary>
    /// <remarks>
    /// http://www.mathprofi.ru/kompleksnye_chisla_dlya_chainikov.html
    /// </remarks>
    public interface IComplexable {
        /// <summary>
        /// Действительная часть комплексного числа.
        /// </summary>
        float Re { get; }
        /// <summary>
        /// Мнимая часть комплексного числа.
        /// </summary>
        float Im { get; }

        /// <summary>
        /// Метод сложения комплексных чисел.
        /// </summary>
        /// <param name="c1">Первое слагаемое.</param>
        /// <param name="c2">Второе слагаемое.</param>
        /// <returns>Сумма комплексных чисел как новое число.</returns>
        IComplexable Addition(IComplexable c1, IComplexable c2);
        
        /// <summary>
        /// Метод вычитания комплексных чисел.
        /// </summary>
        /// <param name="c1">Вычитаемое.</param>
        /// <param name="c2">Уменьшаемое.</param>
        /// <returns>Разность комплексных чисел как новое число.</returns>
        IComplexable Subtraction(IComplexable c1, IComplexable c2);

        /// <summary>
        /// Метод умножения комплексных чисел.
        /// </summary>
        /// <param name="c1">Множимое</param>
        /// <param name="c2">Множитель.</param>
        /// <returns>Произведение комплексных чисел как новое число.</returns>
        IComplexable Multiplication(IComplexable c1, IComplexable c2);
    }
}