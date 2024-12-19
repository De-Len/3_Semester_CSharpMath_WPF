using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3_Semester_CSharpMath_WPF.Models.MathMethods
{
    static class EvaluatingDifiniteIntegralsModel
    {
        public static double CalculateUsingRectangleMethod(double lowerBound, double upperBound, int subintervalsNumber)
        {
            double subintervalWidth = (upperBound - lowerBound) / subintervalsNumber; // Ширина подынтервалов
            double areaSum = 0.0;

            for (int index = 0; index < subintervalsNumber; ++index)
            {
                double currentX = lowerBound + index * subintervalWidth; // Текущая координата x
                areaSum += MathMethodsGroup.SolveFunctionSingleArgument(currentX) * subintervalWidth; // Значение функции умноженное на ширину подынтервала
            }

            return areaSum;
        }

        // Метод трапеций
        public static double CalculateUsingTrapezoidalMethod(double lowerBound, double upperBound, int subintervalsNumber)
        {
            double subintervalWidth = (upperBound - lowerBound) / subintervalsNumber; // Ширина подынтервалов
            double areaSum = 0.5 * (MathMethodsGroup.SolveFunctionSingleArgument(lowerBound) + MathMethodsGroup.SolveFunctionSingleArgument(upperBound)); // Учёт крайних точек

            for (int index = 1; index < subintervalsNumber; ++index)
            {
                double currentX = lowerBound + index * subintervalWidth;
                areaSum += MathMethodsGroup.SolveFunctionSingleArgument(currentX); // Суммируем значения функции в подынтервалах
            }

            return areaSum * subintervalWidth; // Умножаем на ширину подынтервалов
        }

        // Метод Симпсона
        public static double CalculateUsingSimpsonMethod(double lowerBound, double upperBound, int subintervalsNumber)
        {
            if (subintervalsNumber % 2 != 0)
            {
                ++subintervalsNumber; // Делим количество подынтервалов на 2, если оно нечетное
            }

            double subintervalWidth = (upperBound - lowerBound) / subintervalsNumber;
            double areaSum = MathMethodsGroup.SolveFunctionSingleArgument(lowerBound) + MathMethodsGroup.SolveFunctionSingleArgument(upperBound);

            for (int index = 1; index < subintervalsNumber; ++index)
            {
                double currentX = lowerBound + index * subintervalWidth;
                areaSum += (index % 2 == 0 ? 2 * MathMethodsGroup.SolveFunctionSingleArgument(currentX) : 4 * MathMethodsGroup.SolveFunctionSingleArgument(currentX)); // Alternating weight
            }

            return areaSum * (subintervalWidth / 3); // Умножаем на вес
        }
    }
}
