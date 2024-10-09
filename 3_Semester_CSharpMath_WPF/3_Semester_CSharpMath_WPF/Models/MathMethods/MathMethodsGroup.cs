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
        public static string UserMathFunction = string.Empty;
        public static double SolveFunction(double x)
        {

            ExpressionContext context = new ExpressionContext();
            context.Imports.AddType(typeof(Math));
            context.Variables.Add("x", x);
            IDynamicExpression e1 = context.CompileDynamic(UserMathFunction); // Исп. свою функцию
            var result = e1.Evaluate();
            if (result is double)
            {
                return (double)result;
            }
            throw new InvalidOperationException("SolveFunction: Результат не является числом типа double."); 
        }

        public static double Bisection(double startLimit, double endLimit, double tolerance)
        {


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
        public static double GoldenSectionSeacthMinimum(double startLimit, double endLimit, double epsilon)
        {
            double phi = (1 + Math.Sqrt(5)) / 2; // Золотое сечение
            double resultPhi = (endLimit - startLimit) / phi; // длина отрезка
            double x1 = endLimit - resultPhi; // точка 1
            double x2 = startLimit + resultPhi; // точка 2

            while (Math.Abs(endLimit - startLimit) > epsilon)
            {
                if (SolveFunction(x1) < SolveFunction(x2))
                {
                    endLimit = x2; // Исключаем x2
                    x2 = x1; // Передвигаем x2
                    resultPhi = (endLimit - startLimit) / phi;
                    x1 = endLimit - resultPhi; // Находим новую x1
                }
                else
                {
                    startLimit = x1; // Исключаем x1
                    x1 = x2; // Передвигаем x1
                    resultPhi = (endLimit - startLimit) / phi;
                    x2 = startLimit + resultPhi; // Находим новую x2
                }
            }

            // Возвращаем середину отрезка как приближенное значение минимума
            return (startLimit + endLimit) / 2;
        }
        public static double GoldenSectionSeacthMaximum(double startLimit, double endLimit, double epsilon)
        {
            double phi = (1 + Math.Sqrt(5)) / 2; // Золотое сечение
            double resultPhi = (endLimit - startLimit) / phi; // длина отрезка
            double x1 = endLimit - resultPhi; // точка 1
            double x2 = startLimit + resultPhi; // точка 2

            while (Math.Abs(endLimit - startLimit) > epsilon)
            {
                if (SolveFunction(x1) > SolveFunction(x2))
                {
                    endLimit = x2; // Исключаем x2
                    x2 = x1; // Передвигаем x2
                    resultPhi = (endLimit - startLimit) / phi;
                    x1 = endLimit - resultPhi; // Находим новую x1
                }
                else
                {
                    startLimit = x1; // Исключаем x1
                    x1 = x2; // Передвигаем x1
                    resultPhi = (endLimit - startLimit) / phi;
                    x2 = startLimit + resultPhi; // Находим новую x2
                }
            }

            // Возвращаем середину отрезка как приближенное значение минимума
            return (startLimit + endLimit) / 2;
        }
    }
}