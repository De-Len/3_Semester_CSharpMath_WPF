using _3_Semester_CSharpMath_WPF.ViewModels.Pages.SortingMethodsPage;

namespace _3_Semester_CSharpMath_WPF.Models.Pages.SortingMethodsPage
{
    public class Sorter
    {
        private ISortStrategy _sortStrategy;

        public void SetSortStrategy(ISortStrategy sortStrategy)
        {
            _sortStrategy = sortStrategy;
        }

        public double[] Sort(double[] list)
        {
            return _sortStrategy.Sort(list);
        }
    }

    public interface ISortStrategy
    {
        double[] Sort(double[] list);
    }

    public class BubbleSort : ISortStrategy
    {
        public double[] Sort(double[] array)
        {
            if (SortingMethods.IsSorted(array))
            {
                return array;
            }

            int arrayLength = array.Length;
            bool swapped;
            for (int i = 0; i < arrayLength - 1; ++i)
            {
                // Увеличиваем итераторы
                SortingMethodsPageViewModel.AddIteration(0);
                swapped = false;
                for (int j = 0; j < arrayLength - i - 1; ++j)
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
    }

    public class InsertionSort : ISortStrategy
    {
        public double[] Sort(double[] array)
        {
            //if (SortingMethods.IsSorted(array))
            //{
            //    return array;
            //}

            int arrayLength = array.Length;
            for (int i = 1; i < arrayLength; ++i)
            {
                SortingMethodsPageViewModel.AddIteration(1);
                double key = array[i];
                int j = i - 1;

                // Сдвигаем элементы, которые больше ключа, на одну позицию вперед
                while (j >= 0)
                {
                    
                    if (array[j] > key)
                    {
                        array[j + 1] = array[j];
                        --j;
                    }
                    else break; // добавлено для оптимизации
                }
                array[j + 1] = key;
            }
            return array;
        }
    }

    public class CocktailShakerSort : ISortStrategy
    {
        //метод обмена элементов
        static void Swap(ref double e1, ref double e2)
        {
            var temp = e1;
            e1 = e2;
            e2 = temp;
        }

        //сортировка перемешиванием
        public double[] Sort(double[] array)
        {
            for (var i = 0; i < array.Length / 2; i++)
            {
                SortingMethodsPageViewModel.AddIteration(2);
                var swapFlag = false;
                //проход слева направо
                for (var j = i; j < array.Length - i - 1; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        Swap(ref array[j], ref array[j + 1]);
                        swapFlag = true;
                    }
                }

                //проход справа налево
                for (var j = array.Length - 2 - i; j > i; j--)
                {
                    if (array[j - 1] > array[j])
                    {
                        Swap(ref array[j - 1], ref array[j]);
                        swapFlag = true;
                    }
                }

                //если обменов не было выходим
                if (!swapFlag)
                {
                    break;
                }
            }

            return array;
        }

    }

    public class QuickSort : ISortStrategy
    {
        public double[] Sort(double[] array)
        {
            //if (SortingMethods.IsSorted(array))
            //{
            //    return array;
            //}

            // Создаем копию исходного массива, чтобы не изменять оригинал
            double[] result = (double[])array.Clone();
            QuickSortHelper(result, 0, result.Length - 1);
            return result; // Возвращаем отсортированный массив
        }

        private void QuickSortHelper(double[] array, int low, int high)
        {
            if (low < high)
            {
                SortingMethodsPageViewModel.AddIteration(3);
                int pivotIndex = Partition(array, low, high); // Разделяем массив и получаем индекс опорного элемента
                QuickSortHelper(array, low, pivotIndex - 1); // Рекурсивно сортируем левую часть
                QuickSortHelper(array, pivotIndex + 1, high); // Рекурсивно сортируем правую часть
            }
        }

        private int Partition(double[] array, int low, int high)
        {
            double pivot = array[high]; // Опорный элемент
            int i = low - 1; // Индекс меньшего элемента

            for (int j = low; j < high; j++)
            {
                if (array[j] <= pivot) // Если текущий элемент меньше или равен опорному
                {
                    ++i; // Увеличиваем индекс меньшего элемента
                    Swap(array, i, j); // Меняем местами элементы
                }
            }
            Swap(array, i + 1, high); // Перемещаем опорный элемент в правильное место
            return i + 1; // Возвращаем индекс опорного элемента
        }

        private void Swap(double[] array, int index1, int index2)
        {
            double temp = array[index1];
            array[index1] = array[index2];
            array[index2] = temp; // Меняем их местами
        }
    }

    public class Bogosort : ISortStrategy
    {
        public double[] Sort(double[] array)
        {
            if (SortingMethods.IsSorted(array))
            {
                return array;
            }
            Random rand = new Random();
            while (!IsSorted(array))
            {
                SortingMethodsPageViewModel.AddIteration(4);
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
                SortingMethodsPageViewModel.AddIteration(4);
                if (array[i - 1] > array[i])
                    return false;
            }
            return true;
        }
    }


    static class SortingMethods
    {
        // Пузырьковая сортировка
        public async static Task<double[]> BubbleSort(double[] array)
        {
            Thread.Sleep(1);
            int n = array.Length;
            bool swapped;
            for (int i = 0; i < n - 1; i++)
            {
                swapped = false;
                for (int j = 0; j < n - i - 1; j++)
                {
                    SortingMethodsPageViewModel.AddIteration(0); // Добавлен счетчик
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
        public async static Task<double[]> InsertionSort(double[] array)
        {
            Thread.Sleep(1);
            int n = array.Length;
            for (int i = 1; i < n; i++)
            {
                double key = array[i];
                int j = i - 1;

                // Сдвигаем элементы, которые больше ключа, на одну позицию вперед
                while (j >= 0)
                {
                    SortingMethodsPageViewModel.AddIteration(1); // добавлено
                    if (array[j] > key)
                    {
                        array[j + 1] = array[j];
                        j--;
                    }
                    else break; // добавлено для предотвращения лишних итераций
                }
                array[j + 1] = key;
            }
            return array;
        }

        // Шейкерная сортировка
        public async static Task<double[]> CocktailShakerSort(double[] array)
        {
            Thread.Sleep(1);
            int n = array.Length;
            bool swapped;
            do
            {
                swapped = false;

                // Проход в прямом направлении
                for (int i = 0; i < n - 1; i++)
                {
                    SortingMethodsPageViewModel.AddIteration(2); // добавлено
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
                    SortingMethodsPageViewModel.AddIteration(2); // добавлено
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
        public async static Task<double[]> QuickSort(double[] array)
        {
            // Проверяем базовый случай: если массив пустой или содержит один элемент, он уже отсортирован
            if (array.Length <= 1)
            {
                return array;
            }

            // Выбор опорного элемента - здесь выбираем последний элемент массива
            double pivot = array[array.Length - 1];

            // Создаем списки для хранения элементов меньше и больше опорного
            var less = new List<double>();
            var greater = new List<double>();

            // Разделяем массив на два подмассива
            for (int i = 0; i < array.Length - 1; i++)
            {
                SortingMethodsPageViewModel.AddIteration(3); // добавлено
                if (array[i] <= pivot)
                {
                    less.Add(array[i]);
                }
                else
                {
                    greater.Add(array[i]);
                }
            }

            // Параллельно сортируем подмассивы и объединяем: меньше + опорный элемент + больше
            var sortedLessTask = QuickSort(less.ToArray());
            var sortedGreaterTask = QuickSort(greater.ToArray());

            // Дожидаемся завершения асинхронных задач и объединяем результаты
            var sortedLess = await sortedLessTask;
            var sortedGreater = await sortedGreaterTask;

            return sortedLess
                .Concat(new double[] { pivot })
                .Concat(sortedGreater)
                .ToArray();
        }

        // Сортировка Бого
        public async static Task<double[]> Bogosort(double[] array)
        {
            Thread.Sleep(1);
            Random rand = new Random();
            while (!IsSorted(array))
            {
                SortingMethodsPageViewModel.AddIteration(4); // добавлено
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

        public static bool IsSorted(double[] array)
        {
            for (int i = 1; i < array.Length; i++)
            {
                SortingMethodsPageViewModel.AddIteration(4); // добавлено
                if (array[i - 1] > array[i])
                    return false;
            }
            return true;
        }
    }
}