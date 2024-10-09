using Flee.PublicTypes;
using System.Text.RegularExpressions;
using static AngouriMath.Entity;

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
            if (SolveFunction(startLimit) * SolveFunction(endLimit) > 0)
            {
                throw new ArgumentException("Функция имеет одинаковые знаки на границах");
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

        public static double NewtonMethod(double startLimit, double endLimit, int maxIterations, double tolerance)
        {
            double initialGuess = (startLimit + endLimit) / 2; // Пример: или любое другое значение внутри границ
            if (initialGuess <= startLimit || initialGuess >= endLimit)
            {
                throw new ArgumentOutOfRangeException("Начальное приближение должно находиться в пределах указанных границ.");
            }

            double root = initialGuess;

            for (int index = 0; index < maxIterations; ++index)
            {
                double fx = SolveFunction(root);
                double deravativeFx = FindDerivative(root);

                if (Math.Abs(fx) < tolerance) // Проверка на корень
                {
                    return root;
                }

                if (Math.Abs(deravativeFx) < tolerance) // Если производная близка к нулю
                {
                    throw new InvalidOperationException("Производная слишком мала, метод Ньютона не может продолжаться.");
                }

                root = root - fx / deravativeFx; // Обновляем приближение

                // Проверка на границы
                if (root < startLimit || root > endLimit)
                {
                    throw new InvalidOperationException("Корень найден за пределами указанного интервала.");
                }
            }

            throw new InvalidOperationException("Корень не найден за указанное количество итераций.");
        }

        public static double FindDerivative(double x)
        {
            Func<double, double> function = SolveFunction;

            // Точка, в которой вы хотите найти производную

            // Шаг для вычисления производной
            double h = 1e-5;

            // Численное приближение производной
            double derivative = (function(x + h) - function(x - h)) / (2 * h);

            return derivative;
        }
    }
}