using _3_Semester_CSharpMath_WPF.ViewModels.Pages.DichotomyMethodPage;
using AngouriMath.Extensions;
using Flee.PublicTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using static SkiaSharp.HarfBuzz.SKShaper;

namespace _3_Semester_CSharpMath_WPF.Models.MathMethods
{
    internal class MathMethodsGroup
    {

        public static double SolveFunction(double x)
        {

            ExpressionContext context = new ExpressionContext();
            context.Imports.AddType(typeof(Math));
            context.Variables.Add("x", x);
            IDynamicExpression e1 = context.CompileDynamic(DichotomyMethodPageViewModel.UserMathFunction); // Исп. свою функцию
            var result = e1.Evaluate();
            if (result is double)
            {
                return (double)result;
            }
            throw new InvalidOperationException("SolveFunction: Результат не является числом типа double."); 
        }

        public static double Bisection(double startLimit, double endLimit, double tolerance)
        {
            if (SolveFunction(startLimit) * SolveFunction(endLimit) >= 0)
            {
                throw new ArgumentException("Функция имеет одинаковый знак на концах. Корень не существует в этом интервале.");
            }

            double root = startLimit;

            while ((endLimit - startLimit) >= tolerance)
            {
                root = (startLimit + endLimit) / 2;

                // Если найденный корень достаточно близок к нулю, можно завершить
                if (Math.Abs(SolveFunction(root)) < tolerance)
                {
                    return root; // нашел корень
                }

                // Ищем в какую сторону двигаться
                if (SolveFunction(root) * SolveFunction(startLimit) < 0)
                {
                    endLimit = root; // корень слева
                }
                else
                {
                    startLimit = root; // корень справа
                }
            }

            return root; // Возвращаем корень после окончания итераций
        }
    }
}