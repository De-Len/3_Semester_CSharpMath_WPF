using _3_Semester_CSharpMath_WPF.Models.MathMethods;
using _3_Semester_CSharpMath_WPF.Models.Pages.DichotomyMethodPage.UserControls;
using _3_Semester_CSharpMath_WPF.Views.Pages.DichotomyMethodPage.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace _3_Semester_CSharpMath_WPF.Models
{
    static class Helpers
    {
        public static (double, double, double, int) CheckExceptions(string userMathFunction, string startLimit = "0", string endLimit = "0", string tolerance = "0", string stepSize = "0",
            string answerCountDigitsAfterPoint = "0", string countIterations = "0", double startLimitDouble = double.NaN, double endLimitDouble = double.NaN,
            double toleranceDouble = double.NaN, int countIterationsInt = 0)
        {
            if (string.IsNullOrWhiteSpace(userMathFunction))
            {
                throw new FormatException("Функция не задана.");
            }

            // Проверка и преобразование StartLimit
            if (!double.TryParse(startLimit, out startLimitDouble))
            {
                throw new FormatException("Неверный формат для начала интервала. Пожалуйста, введите число.");
            }

            // Проверка и преобразование EndLimit
            if (!double.TryParse(endLimit, out endLimitDouble))
            {
                throw new FormatException("Неверный формат для конца интервала. Пожалуйста, введите число.");
            }

            if (!double.TryParse(answerCountDigitsAfterPoint, out double answerCountDigitsAfterPointDouble))
            {
                throw new FormatException("Неверный формат для точности ответа. Пожалуйста, введите число.");
            }
            if (answerCountDigitsAfterPointDouble < 0)
            {
                throw new FormatException("Неверный формат для точности ответа. Пожалуйста, введите положительное число.");
            }

            // Проверка и преобразование CountIterations (количества итераций)
            if (!int.TryParse(countIterations, out countIterationsInt))
            {
                throw new FormatException("Неверный формат для количества итераций. Пожалуйста, введите целое число.");
            }
            if (countIterationsInt <= 0)
            {
                throw new FormatException("Неверный формат для количества итераций. Пожалуйста, положительное число.");
            }

            // Проверка на соответствие границ
            if (startLimitDouble >= endLimitDouble)
            {
                throw new ArgumentException("Начальная граница должна быть меньше конечной границы.");
            }


            // Проверка и преобразование Tolerance (точности)
            if (!double.TryParse(tolerance, out toleranceDouble))
            {
                throw new FormatException("Неверный формат для точности вычисления. Пожалуйста, введите число.");
            }

            if (!double.TryParse(stepSize, out double stepSizeDouble))
            {
                throw new FormatException("Неверный формат для размера шага. Пожалуйста, введите число.");
            }

            return (startLimitDouble, endLimitDouble, toleranceDouble, countIterationsInt);
        }

        public static (double, double, double, int) CheckExceptionsCoordinateDescentMethod(string userMathFunction, string startLimit = "0", string endLimit = "0", string tolerance = "0", string stepSize = "0",
            string answerCountDigitsAfterPoint = "0", string countIterations = "0", double startLimitDouble = double.NaN, double endLimitDouble = double.NaN,
            double toleranceDouble = double.NaN, int countIterationsInt = 0)
        {
            if (string.IsNullOrWhiteSpace(userMathFunction))
            {
                throw new FormatException("Функция не задана.");
            }

            // Проверка и преобразование StartLimit
            if (!double.TryParse(startLimit, out startLimitDouble))
            {
                throw new FormatException("Неверный формат для начала интервала. Пожалуйста, введите число.");
            }

            // Проверка и преобразование EndLimit
            if (!double.TryParse(endLimit, out endLimitDouble))
            {
                throw new FormatException("Неверный формат для конца интервала. Пожалуйста, введите число.");
            }

            if (!double.TryParse(answerCountDigitsAfterPoint, out double answerCountDigitsAfterPointDouble))
            {
                throw new FormatException("Неверный формат для точности ответа. Пожалуйста, введите число.");
            }
            if (answerCountDigitsAfterPointDouble < 0)
            {
                throw new FormatException("Неверный формат для точности ответа. Пожалуйста, введите положительное число.");
            }

            // Проверка и преобразование CountIterations (количества итераций)
            if (!int.TryParse(countIterations, out countIterationsInt))
            {
                throw new FormatException("Неверный формат для количества итераций. Пожалуйста, введите целое число.");
            }
            if (countIterationsInt <= 0)
            {
                throw new FormatException("Неверный формат для количества итераций. Пожалуйста, положительное число.");
            }

            // Проверка и преобразование Tolerance (точности)
            if (!double.TryParse(tolerance, out toleranceDouble))
            {
                throw new FormatException("Неверный формат для точности вычисления. Пожалуйста, введите число.");
            }

            if (!double.TryParse(stepSize, out double stepSizeDouble))
            {
                throw new FormatException("Неверный формат для размера шага. Пожалуйста, введите число.");
            }

            return (startLimitDouble, endLimitDouble, stepSizeDouble, countIterationsInt);
        }
        public static (double, double, int, int) CheckExceptionsSortingMethods(string startLimit = "0", string endLimit = "0", string accuracyCountDigitsAfterPoint = "0", string numbersCount = "0",
            int numberCountInt = 0, double startLimitDouble = double.NaN, double endLimitDouble = double.NaN, int accuracyCountDigitsAfterPointInt = 0)
        {
            // Проверка и преобразование StartLimit
            if (!double.TryParse(startLimit, out startLimitDouble))
            {
                throw new FormatException("Неверный формат для начала интервала. Пожалуйста, введите число.");
            }

            // Проверка и преобразование EndLimit
            if (!double.TryParse(endLimit, out endLimitDouble))
            {
                throw new FormatException("Неверный формат для конца интервала. Пожалуйста, введите число.");
            }

            // Проверка и преобразование CountIterations (количества итераций)
            if (!int.TryParse(accuracyCountDigitsAfterPoint, out accuracyCountDigitsAfterPointInt))
            {
                throw new FormatException("Неверный формат для количества знаков после запятой.");
            }

            // Проверка и преобразование Tolerance (точности)
            if (!int.TryParse(numbersCount, out numberCountInt))
            {
                throw new FormatException("Неверный формат для количества чисел.");
            }

            return (startLimitDouble, endLimitDouble, accuracyCountDigitsAfterPointInt, numberCountInt);
        }
    }
}
