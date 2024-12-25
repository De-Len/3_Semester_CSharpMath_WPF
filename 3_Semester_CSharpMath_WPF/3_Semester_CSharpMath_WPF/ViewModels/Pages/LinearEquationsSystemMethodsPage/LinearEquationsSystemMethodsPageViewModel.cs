using _3_Semester_CSharpMath_WPF.Models.MathMethods;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Ookii.Dialogs.Wpf;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Security.RightsManagement;
using System.Windows;
using System.Windows.Input;
using static AngouriMath.MathS;
using Clo

namespace _3_Semester_CSharpMath_WPF.ViewModels.Pages.LinearEquationsSystemMethodsPage
{
    partial class LinearEquationsSystemMethodsPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private DataView _dataView = new DataView();
        public DataTable DataTable = new DataTable("Matrix");
        public int LastColumnRow = 3;
        public DataTable MatrixDataTable = new DataTable();
        [ObservableProperty]
        private bool _isSelectedGauss = true;
        [ObservableProperty]
        private bool _isSelectedGaussJordan = true;
        [ObservableProperty]
        private bool _isSelectedCramer = true;
        [ObservableProperty]
        private string _resultGauss;
        [ObservableProperty]
        private string _resultGaussJordan;
        [ObservableProperty]
        private string _resultCramer;
        [ObservableProperty]
        private Visibility _resultGaussVisibility = Visibility.Hidden;
        [ObservableProperty]
        private Visibility _resultGaussJordanVisibility = Visibility.Hidden;
        [ObservableProperty]
        private Visibility _resultCramerVisibility = Visibility.Hidden;



        public ICommand AddToTableCommand { get; }
        public ICommand RemoveFromTableCommand { get; }
        public ICommand ClearTableCommand { get; }
        public ICommand SolveEquationsCommand { get; }
        public ICommand GetDataFromExcelCommand { get; }
        public LinearEquationsSystemMethodsPageViewModel()
        {
            InitTable();
            AddToTableCommand = new RelayCommand(AddToTable);
            RemoveFromTableCommand = new RelayCommand(RemoveFromTable);
            ClearTableCommand = new RelayCommand(ClearTable);
            SolveEquationsCommand = new RelayCommand(SolveEquations);
            GetDataFromExcelCommand = new RelayCommand(GetDataFromExcel);
        }

        private void InitTable()
        {
            DataTable.Columns.Add("x1", typeof(double));
            DataTable.Columns.Add("x2", typeof(double));
            DataTable.Columns.Add("x3", typeof(double));


            DataTable.Rows.Add();
            DataTable.Rows.Add();

            DataView = DataTable.DefaultView;
        }

        private void AddToTable()
        {
            try
            {
                if (LastColumnRow >= 51)
                {
                    throw new Exception("Размер матрицы: 2<=N<=50!");
                }

                DataTable dataTable = new DataTable();

                MatrixDataTable = DataView.Table;

                ++LastColumnRow;

                dataTable.Columns.Add("x1", typeof(double));
                for (int columnIndex = 2; columnIndex < LastColumnRow + 1; ++columnIndex)
                {
                    dataTable.Columns.Add("x" + columnIndex.ToString(), typeof(double));
                    dataTable.Rows.Add();
                }

                // наполнение прошлыми данными
                for (int row = 0; row < MatrixDataTable.Rows.Count; ++row)
                {
                    for (int col = 0; col < MatrixDataTable.Columns.Count; ++col)
                    {
                        dataTable.DefaultView.Table.Rows[row][col] = MatrixDataTable.Rows[row][col];
                    }
                }

                DataView = dataTable.DefaultView;
                DataTable = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RemoveFromTable()
        {
            try
            {
                if (LastColumnRow <= 3)
                {
                    throw new Exception("Размер матрицы: 2<=N<=50!");
                }

                DataTable dataTable = new DataTable();

                MatrixDataTable = DataView.Table;

                --LastColumnRow;

                dataTable.Columns.Add("x1", typeof(double));
                for (int columnIndex = 2; columnIndex < LastColumnRow + 1; ++columnIndex)
                {
                    dataTable.Columns.Add("x" + columnIndex.ToString(), typeof(double));
                    dataTable.Rows.Add();
                }

                // наполнение прошлыми данными
                for (int row = 0; row < dataTable.DefaultView.Table.Rows.Count; ++row)
                {
                    for (int col = 0; col < dataTable.DefaultView.Table.Columns.Count; ++col)
                    {
                        dataTable.DefaultView.Table.Rows[row][col] = MatrixDataTable.Rows[row][col];
                    }
                }

                DataView = dataTable.DefaultView;
                DataTable = dataTable;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void ClearTable()
        {
            // наполнение прошлыми данными
            for (int row = 0; row < DataView.Table.DefaultView.Table.Rows.Count; ++row)
            {
                for (int col = 0; col < DataView.Table.DefaultView.Table.Columns.Count; ++col)
                {
                    DataView.Table.DefaultView.Table.Rows[row][col] = DBNull.Value;
                }
            }
        }

        public void GetDataFromExcel()
        {
            DataTable dataTable = new DataTable();

            var dlg = new VistaOpenFileDialog
            {
                Title = "Выберите файл Excel",
                Filter = "Файлы Excel (*.xlsx)|*.xlsx",
                Multiselect = false // Возможность выбора нескольких файлов
            };

            // Открываем диалог и проверяем, было ли выбрано что-либо
            if (dlg.ShowDialog() == true)
            {
                string filePath = dlg.FileName;

                // Чтение данных из Excel файла
                using (var file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    workbook = new XL(file); // Открываем книгу
                    ISheet sheet = workbook.GetSheetAt(0); // Получаем первый лист

                    // Получаем количество используемых строк
                    int rowCount = sheet.LastRowNum + 1;

                    // Предполагаем, что в первом ряду находятся заголовки
                    IRow headerRow = sheet.GetRow(0);
                    int colCount = headerRow.LastCellNum;




                    // Добавляем колонки в DataTable
                    for (int col = 0; col < colCount; col++)
                    {
                        dataTable.Columns.Add(headerRow.GetCell(col).StringCellValue);
                    }

                    // Чтение данных из остальных строк
                    for (int row = 1; row < rowCount; row++)
                    {
                        DataRow dataRow = dataTable.NewRow();

                        for (int col = 0; col < colCount; col++)
                        {
                            if (currentRow.GetCell(col) != null) // Проверяем, чтобы ячейка не была пустой
                            {
                                dataRow[col] = currentRow.GetCell(col).ToString();
                            }
                        }
                        dataTable.Rows.Add(dataRow);
                    }
                }

                // Присваиваем DataView и DataTable (предполагается, что у вас есть соответствующие члены класса)
                DataView = dataTable.DefaultView;
                DataTable = dataTable;
            }
        }

        public void SolveEquations()
        {
            ResultGauss = "";
            ResultGaussJordan = "";
            ResultCramer = "";

            ResultGaussVisibility = Visibility.Hidden;
            ResultGaussJordanVisibility = Visibility.Hidden;
            ResultCramerVisibility = Visibility.Hidden;


            int rows = DataView.Table.DefaultView.Table.Rows.Count;
            int cols = DataView.Table.DefaultView.Table.Columns.Count;
            double[,] coefficients = new double[rows, cols - 1];
            double[] constants = new double[rows];

            for (int row = 0; row < rows; ++row)
            {
                for (int col = 0; col < cols - 1; ++col)
                {
                    coefficients[row, col] = DataTable.Rows[row][col] != DBNull.Value
                                        ? (double)DataTable.Rows[row][col]
                                        : 0.0; 
                }
            }

            for (int row = 0; row < rows; ++row)
            {
                constants[row] = DataTable.Rows[row][cols - 1] != DBNull.Value
                                        ? (double)DataTable.Rows[row][cols - 1]
                                        : 0.0;
            }

            try
            {
                if (IsSelectedGauss)
                {
                    double[] resultGaussDoubles = LinearEquationsSystemMethods.GaussianElimination.Solve(coefficients, constants);

                    for (int col = 0; col < cols - 1; ++col)
                    {
                        if (col == cols - 2)
                        {
                            ResultGauss += "x" + (col + 1).ToString() + " = " + resultGaussDoubles[col].ToString("F3");

                        }
                        else
                        {
                            ResultGauss += "x" + (col + 1).ToString() + " = " + resultGaussDoubles[col].ToString("F3") + ", ";

                        }
                    }
                    ResultGaussVisibility = Visibility.Visible;
                }
                if (IsSelectedGaussJordan)
                {
                    double[] resultGaussJordanDoubles = LinearEquationsSystemMethods.GaussJordanElimination.Solve(coefficients, constants);

                    for (int col = 0; col < cols - 1; ++col)
                    {
                        if (col == cols - 2)
                        {
                            ResultGaussJordan += "x" + (col + 1).ToString() + " = " + resultGaussJordanDoubles[col].ToString("F3");

                        }
                        else
                        {
                            ResultGaussJordan += "x" + (col + 1).ToString() + " = " + resultGaussJordanDoubles[col].ToString("F3") + ", ";

                        }
                    }
                    ResultGaussJordanVisibility = Visibility.Visible;

                }
                if (IsSelectedCramer)
                {
                    double[] resultCramerDoubles = LinearEquationsSystemMethods.Cramer.Solve(coefficients, constants);

                    for (int col = 0; col < cols - 1; ++col)
                    {
                        if (col == cols - 2)
                        {
                            ResultCramer += "x" + (col + 1).ToString() + " = " + resultCramerDoubles[col].ToString("F3");

                        }
                        else
                        {
                            ResultCramer += "x" + (col + 1).ToString() + " = " + resultCramerDoubles[col].ToString("F3") + ", ";

                        }
                    }
                    ResultCramerVisibility = Visibility.Visible;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}