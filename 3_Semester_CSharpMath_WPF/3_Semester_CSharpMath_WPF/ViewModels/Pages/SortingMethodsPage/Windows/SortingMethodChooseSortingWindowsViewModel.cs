using _3_Semester_CSharpMath_WPF.Models.Pages.SortingMethodsPage;
using _3_Semester_CSharpMath_WPF.ViewModels.Pages.SortingMethodsPage.Windows.UserControls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace _3_Semester_CSharpMath_WPF.ViewModels.Pages.SortingMethodsPage.Windows
{
    partial class SortingMethodChooseSortingWindowsViewModel : ObservableObject
    {
        private SortingMethodsPageViewModel _sortingMethodsPageViewModel;
        [ObservableProperty]
        private bool _isAscendingChecked = true;
        [ObservableProperty]
        private bool _isDescendingChecked = false;
        double[] NumbersFromFile;
        List<ISortStrategy> ListSortStrategies = [new BubbleSort(), new InsertionSort(), new CocktailShakerSort(),
                                                    new QuickSort(), new Bogosort()];
        List<string> ListSortOutputCollections;

        public ICommand SortCommand { get; }
        public SortingMethodChooseSortingWindowsViewModel(SortingMethodsPageViewModel sortingMethodsPageViewModel)
        {
            _sortingMethodsPageViewModel = sortingMethodsPageViewModel;
            ListSortOutputCollections = new List<string>
            {
                sortingMethodsPageViewModel.BubbleSortOutput,
                sortingMethodsPageViewModel.InsertionSortOutput,
                sortingMethodsPageViewModel.CocktailShakerSortOutput,
                sortingMethodsPageViewModel.QuickSortOutput,
                sortingMethodsPageViewModel.BogoSortOutput
            };

            SortCommand = new RelayCommand(Sort);
        }

        public async Task SortByIndexAsync(int sortingIndex)
        {
            Stopwatch stopwatch = new Stopwatch();
            var sorter = new Sorter();


            sorter.SetSortStrategy(ListSortStrategies[sortingIndex]);

            stopwatch.Start();
            Thread.Sleep(1);
            var result = sorter.Sort(NumbersFromFile);
            stopwatch.Stop();

            //SortingMethodsPageViewModel.DataGrid[sortingIndex].Timing = (double)stopwatch.ElapsedMilliseconds;
            SortingMethodsPageViewModel.DataGrid[sortingIndex].Timing = stopwatch.ElapsedMilliseconds - 1;
        }


        public async void Sort()
        {
            double[] _sortedArray0 = [];
            double[] _sortedArray1 = [];
            double[] _sortedArray2 = [];
            double[] _sortedArray3 = [];
            double[] _sortedArray4 = [];
            try
            {
                SortingMethodsPageViewModel.CleanIterations();

                string fileContent = File.ReadAllText(DataGeneratorAutomaticallyViewModel.FilePath + "\\ДаныеДляТеста.txt");
                string[] numberStrings = fileContent.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
                // Преобразование строк в числа
                NumbersFromFile = Array.ConvertAll(numberStrings, double.Parse);

                double[] NumbersFromFile0 = Array.ConvertAll(numberStrings, double.Parse);
                double[] NumbersFromFile1 = Array.ConvertAll(numberStrings, double.Parse);
                double[] NumbersFromFile2 = Array.ConvertAll(numberStrings, double.Parse);
                double[] NumbersFromFile3 = Array.ConvertAll(numberStrings, double.Parse);
                double[] NumbersFromFile4 = Array.ConvertAll(numberStrings, double.Parse);
                


                if (_isAscendingChecked)
                {
                    var tasks = new List<Task>();

                    for (int index = 0; index < ListSortStrategies.Count; index++)
                    {
                        int sortIndex = index;
                        if (SortingMethodsPageViewModel.DataGrid[sortIndex].IsSelected)
                        {
                            tasks.Add(Task.Run(async () =>
                            {

                                var sorter = new Sorter();

                                Stopwatch stopwatch = Stopwatch.StartNew(); // Запуск таймера
                                sorter.SetSortStrategy(ListSortStrategies[sortIndex]);

                                switch (sortIndex)
                                {
                                    case 0:
                                        _sortedArray0 = sorter.Sort(NumbersFromFile0);
                                        break;
                                    case 1:
                                        _sortedArray1 = sorter.Sort(NumbersFromFile1);
                                        break;
                                    case 2:
                                        _sortedArray2 = sorter.Sort(NumbersFromFile2);
                                        break;
                                    case 3:
                                        _sortedArray3 = sorter.Sort(NumbersFromFile3);
                                        break;
                                    case 4:
                                        _sortedArray4 = sorter.Sort(NumbersFromFile4);
                                        break;
                                }

                                Thread.Sleep(10);
                                stopwatch.Stop(); // Остановка таймера

                                // Обновление временного значения в DataGrid
                                SortingMethodsPageViewModel.DataGrid[sortIndex].Timing = stopwatch.ElapsedMilliseconds - 10;
                                // Сохранение отсортированного массива

                                switch (sortIndex)
                                {
                                    case 0:
                                        _sortingMethodsPageViewModel.BubbleSortOutput = string.Join(", ", _sortedArray0);
                                        break;
                                    case 1:
                                        _sortingMethodsPageViewModel.InsertionSortOutput = string.Join(", ", _sortedArray1);
                                        break;
                                    case 2:
                                        _sortingMethodsPageViewModel.CocktailShakerSortOutput = string.Join(", ", _sortedArray2);
                                        break;
                                    case 3:
                                        _sortingMethodsPageViewModel.QuickSortOutput = string.Join(", ", _sortedArray3);
                                        break;
                                    case 4:
                                        _sortingMethodsPageViewModel.BogoSortOutput = string.Join(", ", _sortedArray4);
                                        break;
                                }
                            }));
                        }
                    }
                }
                else if (_isDescendingChecked)
                {
                    var tasks = new List<Task>();

                    for (int index = 0; index < ListSortStrategies.Count; index++)
                    {
                        int sortIndex = index; // Необходимо для замыкания
                        if (SortingMethodsPageViewModel.DataGrid[index].IsSelected)
                        {
                            tasks.Add(Task.Run(async () =>
                            {

                                var sorter = new Sorter();

                                Stopwatch stopwatch = Stopwatch.StartNew(); // Запуск таймера
                                sorter.SetSortStrategy(ListSortStrategies[sortIndex]);
                                switch (sortIndex)
                                {
                                    case 0:
                                        _sortedArray0 = sorter.Sort(NumbersFromFile0).Reverse().ToArray();
                                        break;
                                    case 1:
                                        _sortedArray1 = sorter.Sort(NumbersFromFile1).Reverse().ToArray();
                                        break;
                                    case 2:
                                        _sortedArray2 = sorter.Sort(NumbersFromFile2).Reverse().ToArray();
                                        break;
                                    case 3:
                                        _sortedArray3 = sorter.Sort(NumbersFromFile3).Reverse().ToArray();
                                        break;
                                    case 4:
                                        _sortedArray4 = sorter.Sort(NumbersFromFile4).Reverse().ToArray();
                                        break;
                                }
                                Thread.Sleep(10);
                                stopwatch.Stop(); // Остановка таймера

                                // Обновление временного значения в DataGrid
                                SortingMethodsPageViewModel.DataGrid[sortIndex].Timing = stopwatch.ElapsedMilliseconds - 10;
                                // Сохранение отсортированного массива

                                switch (sortIndex)
                                {
                                    case 0:
                                        _sortingMethodsPageViewModel.BubbleSortOutput = string.Join(", ", _sortedArray0);
                                        break;
                                    case 1:
                                        _sortingMethodsPageViewModel.InsertionSortOutput = string.Join(", ", _sortedArray1);
                                        break;
                                    case 2:
                                        _sortingMethodsPageViewModel.CocktailShakerSortOutput = string.Join(", ", _sortedArray2);
                                        break;
                                    case 3:
                                        _sortingMethodsPageViewModel.QuickSortOutput = string.Join(", ", _sortedArray3);
                                        break;
                                    case 4:
                                        _sortingMethodsPageViewModel.BogoSortOutput = string.Join(", ", _sortedArray4);
                                        break;
                                }
                            }));
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Ошибка при сортировке!");
            }
        }
    }
}
