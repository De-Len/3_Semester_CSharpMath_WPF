using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using Ookii.Dialogs.Wpf;
using System.IO;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using _3_Semester_CSharpMath_WPF.Models.Pages.LinearEquationsSystemMethodsPage.Windows;
using ClosedXML.Excel;
using System.Data;
using _3_Semester_CSharpMath_WPF.ViewModels.Pages.LinearEquationsSystemMethodsPage;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Spreadsheet;

namespace _3_Semester_CSharpMath_WPF.ViewModels.Pages.LinearEquationsSystemMethodsPage.Windows
{
    partial class LinearEquationsSystemMethodsGoogleSheetWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _pathToCredentialsJson;
        [ObservableProperty]
        private string _spreadsheetId = "1906DW5tRcBs_wrtWzlGwe2bQ9BaHGzdd87kfcrAuKic";
        public ICommand ChoosePathToCredentialsJsonCommand { get; }
        public ICommand SaveDataFromGoogleSheetCommand {  get; }

        private LinearEquationsSystemMethodsGoogleSheetWindowModel _model = new LinearEquationsSystemMethodsGoogleSheetWindowModel();
        private LinearEquationsSystemMethodsPageViewModel _linearEquationsSystemMethodsPageViewModel;
        public LinearEquationsSystemMethodsGoogleSheetWindowViewModel(LinearEquationsSystemMethodsPageViewModel linearEquationsSystemMethodsPageViewModel)
        {
            _linearEquationsSystemMethodsPageViewModel = linearEquationsSystemMethodsPageViewModel; 

            ChoosePathToCredentialsJsonCommand = new RelayCommand(ChoosePathToCredentialsJson);
            SaveDataFromGoogleSheetCommand = new RelayCommand(SaveDataFromGoogleSheet);
        }

        public void ChoosePathToCredentialsJson()
        {
            var dlg = new VistaOpenFileDialog
            {
                Title = "Выберите JSON-файл",
                Filter = "JSON файлы (*.json)|*.json|Все файлы (*.*)|*.*",
                Multiselect = false // Возможность выбора нескольких файлов
            };

            // Открываем диалог и проверяем, было ли выбрано что-либо
            if (dlg.ShowDialog() == true)
            {
                string filePath = dlg.FileName;

                PathToCredentialsJson = filePath;
            }
        }

        public void SaveDataFromGoogleSheet()
        {
            string sheetName = "Лист1";

            // Получаем данные из Google Sheets
            List<List<double>> listDataFromGoogleSheet = _model.GetDataFromGoogleSheet(SpreadsheetId, sheetName, PathToCredentialsJson);

            // Создаем новый DataTable для хранения данных
            DataTable dataTable = new DataTable();

            // Определяем количество столбцов и строк
            int columnCount = listDataFromGoogleSheet.Count > 0 ? listDataFromGoogleSheet[0].Count : 0; // Количество столбцов в первой строке
            int rowCount = listDataFromGoogleSheet.Count; // Количество строк

            // Сохраняем количество столбцов
            _linearEquationsSystemMethodsPageViewModel.LastColumnRow = columnCount;

            // Инициализируем столбцы в DataTable
            for (int i = 0; i < columnCount; ++i)
            {
                dataTable.Columns.Add($"x{i + 1}", typeof(double));
            }

            // Заполняем строки данными
            for (int row = 0; row < rowCount; ++row)
            {
                var drA = dataTable.NewRow();
                for (int col = 0; col < columnCount; ++col)
                {
                    // Пытаемся присвоить значение из исходного списка в DataRow
                    try
                    {
                        drA[col] = listDataFromGoogleSheet[row][col];
                    }
                    catch (IndexOutOfRangeException)
                    {
                        // Если индекс выходит за пределы, устанавливаем DBNull
                        drA[col] = DBNull.Value;
                    }
                    catch (InvalidCastException)
                    {
                        // Если преобразование не удалось, также устанавливаем DBNull
                        drA[col] = DBNull.Value;
                    }
                }
                dataTable.Rows.Add(drA);
            }

            // Присваиваем DataView и DataTable (предполагается, что у вас есть соответствующие члены класса)
            _linearEquationsSystemMethodsPageViewModel.DataView = dataTable.DefaultView;
            _linearEquationsSystemMethodsPageViewModel.DataTable = dataTable;
        }
    }
}
