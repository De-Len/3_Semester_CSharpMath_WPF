using _3_Semester_CSharpMath_WPF.Models.Pages.SortingMethodsPage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.IO;
using System.Windows;
using System.Windows.Input;
using _3_Semester_CSharpMath_WPF.ViewModels.Pages.SortingMethodsPage.Windows.UserControls;
using static AngouriMath.MathS;
using System.Diagnostics;
using _3_Semester_CSharpMath_WPF.Models.Pages.SortingMethodsPage.Windows.UserControls;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using HelixToolkit.Wpf;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks.Dataflow;

namespace _3_Semester_CSharpMath_WPF.ViewModels.Pages.SortingMethodsPage.Windows
{
    partial class SortingMethodChooseSortingWindowsViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool _isAscendingChecked = true;
        [ObservableProperty]
        private bool _isDescendingChecked = false;
        double[] NumbersFromFile;
        List<ISortStrategy> ListSortStrategies = [new BubbleSort(), new InsertionSort(), new CocktailShakerSort(), 
                                                    new QuickSort(), new Bogosort()];
        List<string> ListSortOutputCollections = [SortingMethodsPageViewModel., SortingMethodsPageViewModel.InsertionSortOutput,
                                                                        SortingMethodsPageViewModel.CocktailShakerSortOutput,
                                                                        SortingMethodsPageViewModel.QuickSortOutput, SortingMethodsPageViewModel.BogoSortOutput];

        public ICommand SortCommand { get; }
        public SortingMethodChooseSortingWindowsViewModel()
        {
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
            try
            {
                string fileContent = File.ReadAllText(DataGeneratorAutomaticallyViewModel.FilePath + "\\ДаныеДляТеста.txt");
                string[] numberStrings = fileContent.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
                // Преобразование строк в числа
                NumbersFromFile = Array.ConvertAll(numberStrings, double.Parse);



                if (_isAscendingChecked)
                {
                    //for (int sortingIndex = 0; sortingIndex < ListSortStrategies.Count; sortingIndex++)
                    //{
                    //    Task.Run(() =>
                    //    {
                    //        SortByIndexAsync(sortingIndex);
                    //    });
                    //}

                    //Task.Run(() =>
                    //{
                    //    Stopwatch stopwatch1 = new Stopwatch();
                    //    stopwatch1.Start();
                    //    Task<double[]> SortedArrayBubbleSort = SortingMethods.BubbleSort(NumbersFromFile);
                    //    stopwatch1.Stop();
                    //    SortingMethodsPageViewModel.DataGrid[0].Timing = stopwatch1.ElapsedMilliseconds;
                    //});

                    //Task.Run(() =>
                    //{
                    //    Stopwatch stopwatch2 = new Stopwatch();
                    //    stopwatch2.Start();
                    //    Task<double[]> SortedArrayInsertionSort = SortingMethods.InsertionSort(NumbersFromFile);
                    //    stopwatch2.Stop();
                    //    SortingMethodsPageViewModel.DataGrid[1].Timing = stopwatch2.ElapsedMilliseconds;
                    //});

                    //Task.Run(() =>
                    //{
                    //    Stopwatch stopwatch3 = new Stopwatch();
                    //    stopwatch3.Start();
                    //    Task<double[]> SortedArrayCocktailShakerSort = SortingMethods.CocktailShakerSort(NumbersFromFile);
                    //    stopwatch3.Stop();
                    //    SortingMethodsPageViewModel.DataGrid[2].Timing = stopwatch3.ElapsedMilliseconds;
                    //});

                    //Task.Run(() =>
                    //{
                    //    Stopwatch stopwatch4 = new Stopwatch();
                    //    stopwatch4.Start();
                    //    Task<double[]> SortedArrayQuickSort = SortingMethods.QuickSort(NumbersFromFile);
                    //    stopwatch4.Stop();
                    //    SortingMethodsPageViewModel.DataGrid[3].Timing = stopwatch4.ElapsedMilliseconds;
                    //});

                    //Task.Run(() =>
                    //{
                    //    Stopwatch stopwatch5 = new Stopwatch();
                    //    stopwatch5.Start();
                    //    Task<double[]> SortedArrayBogosort = SortingMethods.Bogosort(NumbersFromFile);
                    //    stopwatch5.Stop();
                    //    SortingMethodsPageViewModel.DataGrid[4].Timing = stopwatch5.ElapsedMilliseconds;
                    //});
                    var tasks = new List<Task>();

                    for (int i = 0; i < ListSortStrategies.Count; i++)
                    {
                        int index = i; // Необходимо для замыкания
                        if (SortingMethodsPageViewModel.DataGrid[index].IsSelected)
                        {
                            tasks.Add(Task.Run(async () =>
                            {

                                var sorter = new Sorter();

                                Stopwatch stopwatch = Stopwatch.StartNew(); // Запуск таймера
                                sorter.SetSortStrategy(ListSortStrategies[index]);
                                var sortedArray = sorter.Sort(NumbersFromFile);
                                Thread.Sleep(10);
                                stopwatch.Stop(); // Остановка таймера

                                // Обновление временного значения в DataGrid
                                SortingMethodsPageViewModel.DataGrid[index].Timing = stopwatch.ElapsedMilliseconds - 10;
                                // Сохранение отсортированного массива

                                ListSortOutputCollections[index] = string.Join(", ", sortedArray);

                            }));
                        }
                    }

                    // Ожидание завершения всех задач
                    //await Task.WhenAll(tasks);
                }
                else if (_isDescendingChecked)
                {

                }
            }
            catch
            {
                MessageBox.Show("Ошибка при сортировке!");
            }
        }
    }
}
