using System;

namespace _3_Semester_CSharpMath_WPF.Models.Pages.SortingMethodsPage
{
    static class SortingMethods
    {
        // Пузырьковая сортировка
        public static double[] BubbleSort(double[] array)
        {
            int n = array.Length;
            bool swapped;
            for (int i = 0; i < n - 1; i++)
            {
                swapped = false;
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        // Обмен значениями
                        double temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                        swapped = true;
                    }
                }
                // Если за проход не было замен, массив отсортирован
                if (!swapped)
                    break;
            }
            return array;
        }

        // Сортировка вставками
        public static double[] InsertionSort(double[] array)
        {
            int n = array.Length;
            for (int i = 1; i < n; i++)
            {
                double key = array[i];
                int j = i - 1;

                // Сдвигаем элементы, которые больше ключа, на одну позицию вперед
                while (j >= 0 && array[j] > key)
                {
                    array[j + 1] = array[j];
                    j--;
                }
                array[j + 1] = key;
            }
            return array;
        }

        // Шейкерная сортировка
        public static double[] CocktailShakerSort(double[] array)
        {
            int n = array.Length;
            bool swapped;
            do
            {
                swapped = false;

                // Проход в прямом направлении
                for (int i = 0; i < n - 1; i++)
                {
                    if (array[i] > array[i + 1])
                    {
                        // Обмен значениями
                        double temp = array[i];
                        array[i] = array[i + 1];
                        array[i + 1] = temp;
                        swapped = true;
                    }
                }

                // Если по прямому проходу не было замен, массив отсортирован
                if (!swapped)
                    break;

                swapped = false;

                // Проход в обратном направлении
                for (int i = n - 2; i >= 0; i--)
                {
                    if (array[i] > array[i + 1])
                    {
                        // Обмен значениями
                        double temp = array[i];
                        array[i] = array[i + 1];
                        array[i + 1] = temp;
                        swapped = true;
                    }
                }
            } while (swapped);
            return array;
        }

        // Быстрая сортировка
        public static double[] QuickSort(double[] array)
        {
            // Проверяем базовый случай: если массив пустой или содержит один элемент, он уже отсортирован
            if (array.Length <= 1)
            {
                return array;
            }

            // Выбор опорного элемента - здесь выбираем последний элемент массива
            double pivot = array[array.Length - 1];
            // Создаем массивы для элементов меньше и больше опорного
            var less = new List<double>();
            var greater = new List<double>();

            // Разделяем массив на два подмассива
            for (int i = 0; i < array.Length - 1; i++)
            {
                if (array[i] <= pivot)
                {
                    less.Add(array[i]);
                }
                else
                {
                    greater.Add(array[i]);
                }
            }

            // Рекурсивно сортируем подмассивы и объединяем: меньше + опорный элемент + больше
            return QuickSort(less.ToArray())
                .Concat(new double[] { pivot })
                .Concat(QuickSort(greater.ToArray()))
                .ToArray();
        }

    private static int Partition(double[] array, int low, int high)
        {
            double pivot = array[high]; // Опорный элемент
            int i = (low - 1); // Индекс меньшего элемента

            for (int j = low; j < high; j++)
            {
                if (array[j] < pivot)
                {
                    i++;

                    // Обмен значениями
                    double temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;
                }
            }

            // Обмен опорного элемента с элементом по индексу из левой части
            double temp1 = array[i + 1];
            array[i + 1] = array[high];
            array[high] = temp1;

            return i + 1;
        }

        // Сортировка Бого
        public static double[] Bogosort(double[] array)
        {
            Random rand = new Random();
            while (!IsSorted(array))
            {
                // Перемешиваем массив
                for (int i = 0; i < array.Length; i++)
                {
                    int j = rand.Next(array.Length);
                    // Обмен значениями
                    double temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;
                }
            }
            return array;
        }

        private static bool IsSorted(double[] array)
        {
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i - 1] > array[i])
                    return false;
            }
            return true;
        }
    }
}