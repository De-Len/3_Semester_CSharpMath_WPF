using AngouriMath.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace _3_Semester_CSharpMath_WPF.Models.MathMethods
{
    internal class MathMethodsGroup
    {
        public static double Function(double x)
        {
            // Пример: f(x) = x^2 - 4
            // return x * x - 4;
            return 0; //TODO READ FUNCTION
        }

        public static double Bisection(double startLimit, double endLimit, double tolerance)
        {
            int iterations = 0;
            double root = startLimit;

            while ((endLimit - startLimit) >= tolerance)
            {
                // Найдем среднюю точку
                root = (startLimit + endLimit) / 2;

                // Проверяем, является ли c корнем
                if (Math.Abs(Function(root)) < tolerance)
                {
                    break; // корень найден
                }
                // Решение лежит в левой части
                else if (Function(root) * Function(startLimit) < 0)
                {
                    endLimit = root;
                }
                // Решение лежит в правой части
                else
                {
                    startLimit = root;
                }

                ++iterations;
            }

            return root; // Возвращаем полученное значение корня
        }
    }
}