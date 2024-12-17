using Flee.PublicTypes;
using System.Text.RegularExpressions;
using static AngouriMath.Entity;

namespace _3_Semester_CSharpMath_WPF.Models.MathMethods
{
    internal class MathMethodsGroup 
    {
        public static string UserMathFunction = string.Empty;

        public static double SolveFunctionSingleArgument(double x)
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

        public static double SolveFunctionTwoArguments(double x, double y)
        {
            ExpressionContext context = new ExpressionContext();
            context.Imports.AddType(typeof(Math));
            context.Variables.Add("x", x);
            context.Variables.Add("y", y);
            IDynamicExpression e1 = context.CompileDynamic(UserMathFunction); // Исп. свою функцию
            var result = e1.Evaluate();
            if (result is double)
            {
                return (double)result;
            }
            throw new InvalidOperationException("SolveFunction: Результат не является числом типа double.");
        }

        public static double FindDerivative(double x)
        {
            Func<double, double> function = SolveFunctionSingleArgument;

            // Точка, в которой вы хотите найти производную

            // Шаг для вычисления производной
            double h = 1e-5;

            // Численное приближение производной
            double derivative = (function(x + h) - function(x - h)) / (2 * h);

            return derivative;
        }
        public static double FindSecondDerivative(double x)
        {
            return FindDerivative(FindDerivative(x));
        }

        // МЕТОД ПОЛОВИННОГО ДЕЛЕНИЯ
        public static double Bisection(double startLimit, double endLimit, double tolerance)
        {
            if (SolveFunctionSingleArgument(startLimit) * SolveFunctionSingleArgument(endLimit) > 0)
            {
                throw new ArgumentException("Функция имеет одинаковые знаки на границах");
            }

            double root = startLimit;

            while ((endLimit - startLimit) >= tolerance)
            {
                root = (startLimit + endLimit) / 2;

                // Если найденный корень достаточно близок к нулю, можно завершить
                if (Math.Abs(SolveFunctionSingleArgument(root)) < tolerance)
                {
                    return root; // нашел корень
                }

                // Ищем в какую сторону двигаться
                if (SolveFunctionSingleArgument(root) * SolveFunctionSingleArgument(startLimit) < 0)
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

        // МЕТОД ЗОЛОТОГО СЕЧЕНИЯ
        public static double GoldenSectionSearch(double startLimit, double endLimit, double epsilon, OptimizationType optimizationType)
        {
            double phi = (1 + Math.Sqrt(5)) / 2; // Золотое сечение
            double resultPhi = (endLimit - startLimit) / phi; // длина отрезка
            double x1 = endLimit - resultPhi; // точка 1
            double x2 = startLimit + resultPhi; // точка 2

            while (Math.Abs(endLimit - startLimit) > epsilon)
            {
                // Сравниваем значения функции в точках x1 и x2
                bool condition = (optimizationType == OptimizationType.Minimize) ?
                                  (SolveFunctionSingleArgument(x1) < SolveFunctionSingleArgument(x2)) :
                                  (SolveFunctionSingleArgument(x1) > SolveFunctionSingleArgument(x2));

                if (condition) // Для минимума
                {
                    endLimit = x2; // Исключаем x2
                    x2 = x1; // Передвигаем x2
                    resultPhi = (endLimit - startLimit) / phi;
                    x1 = endLimit - resultPhi; // Находим новую x1
                }
                else // Для максимума
                {
                    startLimit = x1; // Исключаем x1
                    x1 = x2; // Передвигаем x1
                    resultPhi = (endLimit - startLimit) / phi;
                    x2 = startLimit + resultPhi; // Находим новую x2
                }
            }

            // Возвращаем середину отрезка как приближенное значение минимума или максимума
            return (startLimit + endLimit) / 2;
        }
    


    // МЕТОД НЬЮТОНА - КЛАССИКА
    public static double NewtonMethod(double startLimit, double endLimit, int iterationsCount, double tolerance)
        {
            if (iterationsCount < 0)
            {
                throw new ArgumentException("Неверное количество итераций!");
            }
            double initialGuess = (startLimit + endLimit) / 2; // Пример: или любое другое значение внутри границ
            if (initialGuess <= startLimit || initialGuess >= endLimit)
            {
                throw new ArgumentOutOfRangeException("Начальное приближение должно находиться в пределах указанных границ.");
            }

            double root = initialGuess;

            for (int index = 0; index < iterationsCount; ++index)
            {
                double fx = SolveFunctionSingleArgument(root);
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

        // МЕТОД НЬЮТОНА - ЭКСТРЕМУМ
        public static double NewtonRaphson(double startLimit, double endLimit, double iterations, double tolerance, OptimizationType optimizationType)
        {
            // Инициализируем начальное приближение
            double x0 = (startLimit + endLimit) / 2;
            for (int index = 0; index < iterations; ++index)
            {
                double fPrime = FindDerivative(x0);
                double fDoublePrime = FindSecondDerivative(x0);
                // Проверка на ноль производной (возможная точка локального экстремума или седловая точка)
                if (Math.Abs(fPrime) < tolerance)
                {
                    Console.WriteLine($"Найден эквивалентный корень в x = {x0}");
                    return x0;
                }
                // В зависимости от типа оптимизации, выбираем значение для обновления
                double step = (optimizationType == OptimizationType.Minimize) ?
                              -fPrime / fDoublePrime :
                              fPrime / fDoublePrime;
                // Вычисление нового приближения
                double x1 = x0 + step;
                // Проверка выхода за границы
                if (x1 < startLimit || x1 > endLimit)
                {
                    Console.WriteLine($"Выход за границы: найденное значение x = {x1} находится вне диапазона.");
                    return x1;
                }
                x0 = x1;
                // Проверка на сходимость: если изменение значения очень маленькое, то мы можем остановиться
                if (Math.Abs(x1 - x0) < tolerance)
                {
                    Console.WriteLine($"Оптимальное значение найдено в x = {x1}");
                    return x1;
                }
            }
            throw new InvalidOperationException("Не удалось найти оптимальное значение в заданном количестве итераций.");
        }


        // МЕТОД ПОКООРДИНАТНОГО СПУСКА
        // Метод для одномерной оптимизации (градиентный спуск) по одной координате
        public static double OneDimensionalMinimization(Func<double, double> func, double initial, double stepSize, int iterations, OptimizationType optimizationType)
        {
            double x = initial;

            for (int iter = 0; iter < iterations; ++iter)
            {
                double x1 = x - stepSize; // Влево
                double x2 = x + stepSize; // Вправо

                double fCurrent = func(x);
                double fLeft = func(x1);
                double fRight = func(x2);

                // Проверка и обновление x в зависимости от типа оптимизации
                if ((optimizationType == OptimizationType.Maximize && fLeft > fCurrent) || (optimizationType == OptimizationType.Minimize && fLeft < fCurrent))
                {
                    x = x1; // Двигаемся влево
                }
                else if ((optimizationType == OptimizationType.Maximize && fRight > fCurrent) || (optimizationType == OptimizationType.Minimize && fRight < fCurrent))
                {
                    x = x2; // Двигаемся вправо
                }
                else
                {
                    break; // Минимум или максимум достигнут
                }
            }

            return x;
        }

        // Основной метод для координатного спуска
        public static double CoordinateDescentMethod(double initialX, double initialY, double stepSize, int iterations, OptimizationType optimizationType)
        {
            double x = initialX;
            double y = initialY;

            for (int iter = 0; iter < iterations; ++iter)
            {
                // Минимизируем или максимизируем по x, оставляя y фиксированным
                x = OneDimensionalMinimization(xi => SolveFunctionTwoArguments(xi, y), x, stepSize, iterations, optimizationType);

                // Минимизируем или максимизируем по y, оставляя x фиксированным
                y = OneDimensionalMinimization(yi => SolveFunctionTwoArguments(x, yi), y, stepSize, iterations, optimizationType);

                // Вывод текущих значений
                Console.WriteLine($"Iteration {iter + 1}: x = {x}, y = {y}, f(x, y) = {SolveFunctionTwoArguments(x, y)}");
            }

            return SolveFunctionTwoArguments(x, y);
        }
    }
}