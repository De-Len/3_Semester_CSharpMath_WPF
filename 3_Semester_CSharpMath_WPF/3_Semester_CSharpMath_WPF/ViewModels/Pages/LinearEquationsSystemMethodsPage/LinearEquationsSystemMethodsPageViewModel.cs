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
using ClosedXML;
using ClosedXML.Excel;

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
                using (var workbook = new XLWorkbook(filePath)) // Открываем книгу
                {
                    DataTable dtA = new DataTable();
                    DataTable dtB = new DataTable();
                    var workbookxls = new XLWorkbook(filePath);
                    var worksheet = workbookxls.Worksheet(1);
                    var range = worksheet.RangeUsed();
                    LastColumnRow = range.FirstColumn().CellCount() + 1;

                    for (int i = 0; i < range.FirstRow().CellCount(); ++i)
                    {
                        dtA.Columns.Add($"x{i + 1}");
                    }


                    foreach (var row in range.RowsUsed())
                    {
                        var drA = dtA.NewRow();

                        for (int i = 0; i < row.Cells().Count(); ++i)
                        {
                            drA[i] = row.Cell(i + 1).Value;
                        }

                        dtA.Rows.Add(drA);
                    }
                    // Присваиваем DataView и DataTable (предполагается, что у вас есть соответствующие члены класса)
                    DataView = dtA.DefaultView;
                    DataTable = dtA;
                }

                
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