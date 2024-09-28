using _3_Semester_CSharpMath_WPF.ViewModels.Pages.DichotomyMethodPage;
using AngouriMath.Extensions;
using Flee.PublicTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Windows;
using static SkiaSharp.HarfBuzz.SKShaper;

namespace _3_Semester_CSharpMath_WPF.Models.MathMethods
{
    internal class MathMethodsGroup
    {

        public static double SolveFunction(double x)
        {
            var factory = new ExpressionContext();
            factory.Variables["x"] = x; // Устанавливаем значение переменной как double

            return factory.CompileGeneric<double>(DichotomyMethodPageViewModel.UserMathFunction).Evaluate(); // Компилируем выражение как double и вычисляем
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
                if (Math.Abs(SolveFunction(root)) < tolerance)
                {
                    break; // корень найден
                }
                // Решение лежит в левой части
                else if (SolveFunction(root) * SolveFunction(startLimit) < 0)
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