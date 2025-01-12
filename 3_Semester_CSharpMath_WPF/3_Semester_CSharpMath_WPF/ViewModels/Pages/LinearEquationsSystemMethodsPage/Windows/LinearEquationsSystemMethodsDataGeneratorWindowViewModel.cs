using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using _3_Semester_CSharpMath_WPF.Models.Pages.LeastSquareMethodPage.Windows;
using _3_Semester_CSharpMath_WPF.Models;
using _3_Semester_CSharpMath_WPF.ViewModels.Pages.LeastSquareMethodPage;
using System.Windows;
using _3_Semester_CSharpMath_WPF.Models.Pages.LinearEquationsSystemMethodsPage.Windows;
using System.Data;
using Google.Apis.Sheets.v4.Data;

namespace _3_Semester_CSharpMath_WPF.ViewModels.Pages.LinearEquationsSystemMethodsPage.Windows
{
    partial class LinearEquationsSystemMethodsDataGeneratorWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _accuracyCountDigitsAfterPoint;
        [ObservableProperty]
        private string _startLimit;
        [ObservableProperty]
        private string _endLimit;
        [ObservableProperty]
        private static int _progressBarValue;

        private LinearEquationsSystemMethodsDataGeneratorWindowModel _model;
        private LinearEquationsSystemMethodsPageViewModel _linearEquationsSystemMethodsPageViewModel;
        public ICommand GenerateNumbersCommand { get; }

        public LinearEquationsSystemMethodsDataGeneratorWindowViewModel(LinearEquationsSystemMethodsPageViewModel linearEquationsSystemMethodsPageViewModel)
        {
            _model = new LinearEquationsSystemMethodsDataGeneratorWindowModel(this);
            _linearEquationsSystemMethodsPageViewModel = linearEquationsSystemMethodsPageViewModel;
            GenerateNumbersCommand = new RelayCommand(GenerateNumbers);
        }

        private async void GenerateNumbers()
        {
            ProgressBarValue = 0;

            double startLimitDouble = double.NaN;
            double endLimitDouble = double.NaN;
            int accuracyCountDigitsAfterPointInt = 0;

            int rows = 0;
            int columns = 0;

            double[][] generatedMatrix;

            DataTable dataTable = new DataTable();
            try
            {
                // Проверка и получение параметров
                (startLimitDouble, endLimitDouble, accuracyCountDigitsAfterPointInt, int numbersCountInt) =
                    Helpers.CheckExceptionsSortingMethods(
                        startLimit: StartLimit,
                        endLimit: EndLimit,
                        accuracyCountDigitsAfterPoint: AccuracyCountDigitsAfterPoint
                    );

                rows = _linearEquationsSystemMethodsPageViewModel.DataView.Table.Rows.Count;
                columns = _linearEquationsSystemMethodsPageViewModel.DataView.Table.Columns.Count;

                await Task.Run(() => {
                    generatedMatrix = _model.GenerateRandomDoubles(rows, columns, startLimitDouble, endLimitDouble, accuracyCountDigitsAfterPointInt).Result;

                    for (int i = 0; i < columns; ++i)
                    {
                        dataTable.Columns.Add($"x{i + 1}", typeof(double));
                    }

                    foreach (var row in generatedMatrix)
                    {
                        // Создаем новую строку и добавляем в нее значения
                        DataRow dataRow = dataTable.NewRow();

                        for (int j = 0; j < columns; j++)
                        {
                            dataRow[j] = row[j]; // Заполнение данных в каждой колонке текущей строки
                        }

                        // Добавляем заполненную строку в таблицу
                        dataTable.Rows.Add(dataRow);
                    }

                    _linearEquationsSystemMethodsPageViewModel.DataView = dataTable.DefaultView;
                    _linearEquationsSystemMethodsPageViewModel.DataTable = dataTable;
                });

                

            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка аргументов", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: " + ex.Message, "Неопределённая ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
