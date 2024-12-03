using _3_Semester_CSharpMath_WPF.Models.Pages.SortingMethodsPage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.IO;
using System.Windows;
using System.Windows.Input;
using _3_Semester_CSharpMath_WPF.ViewModels.Pages.SortingMethodsPage.Windows.UserControls;
using static AngouriMath.MathS;
using System.Diagnostics;

namespace _3_Semester_CSharpMath_WPF.ViewModels.Pages.SortingMethodsPage.Windows
{
    partial class SortingMethodChooseSortingWindowsViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool _isAscendingChecked = true;
        [ObservableProperty]
        private bool _isDescendingChecked = false;

        public ICommand SortCommand { get; }
        public SortingMethodChooseSortingWindowsViewModel()
        {
            SortCommand = new RelayCommand(Sort);
        }

        public void Sort()
        {
            try
            {
                string fileContent = File.ReadAllText(DataGeneratorAutomaticallyViewModel.FilePath + "\\ДаныеДляТеста.txt");
                string[] numberStrings = fileContent.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);

                // Преобразование строк в числа
                double[] numbers = Array.ConvertAll(numberStrings, double.Parse);

                if (_isAscendingChecked)
                {
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();
                    double[] SortedArrayBubbleSort = SortingMethods.BubbleSort(numbers);
                    stopwatch.Stop();
                    var time = stopwatch.ElapsedMilliseconds;


                    stopwatch.Start();
                    double[] SortedArrayInsertionSort = SortingMethods.InsertionSort(numbers);
                    stopwatch.Stop();
                    time = stopwatch.ElapsedMilliseconds;


                    stopwatch.Start();
                    double[] SortedArrayCocktailShakerSort = SortingMethods.CocktailShakerSort(numbers);
                    stopwatch.Stop();
                    time = stopwatch.ElapsedMilliseconds;

                    stopwatch.Start();
                    double[] SortedArrayQuickSort = SortingMethods.QuickSort(numbers);
                    stopwatch.Stop();
                    time = stopwatch.ElapsedMilliseconds;

                    stopwatch.Start();
                    double[] SortedArrayBogosort = SortingMethods.Bogosort(numbers);
                    stopwatch.Stop();

                    time = stopwatch.ElapsedMilliseconds;
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
