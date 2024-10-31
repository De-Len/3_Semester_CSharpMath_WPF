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
        public static void TryAndCatchExceptions(string UserMathFunction, string StartLimit, string EndLimit, string Tolerance,
            string AnswerCountDigitsAfterPoint, string CountIterations, out double startLimitDouble, out double endLimitDouble,
            out double toleranceDouble, out int countIterationsInt)
        {

            try
            {
                if (UserMathFunction == "")
                {
                    throw new FormatException("Функция не задана.");
                }
                // Проверка и преобразование StartLimit
                if (!double.TryParse(StartLimit, out startLimitDouble))
                {
                    throw new FormatException("Неверный формат для начала интервала. Пожалуйста, введите число.");
                }

                // Проверка и преобразование EndLimit
                if (!double.TryParse(EndLimit, out endLimitDouble))
                {
                    throw new FormatException("Неверный формат для конца интервала. Пожалуйста, введите число.");
                }

                // Проверка и преобразование Tolerance (точности)
                if (!double.TryParse(Tolerance, out toleranceDouble))
                {
                    throw new FormatException("Неверный формат для точности вычисления. Пожалуйста, введите число.");
                }
                if (!double.TryParse(AnswerCountDigitsAfterPoint, out double answerCountDigitsAfterPoint))
                {
                    throw new FormatException("Неверный формат для точности ответа. Пожалуйста, введите число.");
                }
                if (answerCountDigitsAfterPoint < 0)
                {
                    throw new FormatException("Неверный формат для точности ответа. Пожалуйста, введите положительное число.");
                }

                // Проверка и преобразование CountIterations (количества итераций)
                if (!int.TryParse(CountIterations, out countIterationsInt))
                {
                    throw new FormatException("Неверный формат для количества итераций. Пожалуйста, введите целое число.");
                }
                if (countIterationsInt < 0)
                {
                    throw new FormatException("Неверный формат для количества итераций. Пожалуйста, положительное число.");
                }

                // Проверка на соответствие границ
                if (startLimitDouble >= endLimitDouble)
                {
                    throw new ArgumentException("Начальная граница должна быть меньше конечной границы.");
                }

            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка аргументов", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: " + ex.Message, "Неопределённая ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
