using _3_Semester_CSharpMath_WPF.Models.MathMethods;
using _3_Semester_CSharpMath_WPF.Models.Pages.DichotomyMethodPage.UserControls;
using _3_Semester_CSharpMath_WPF.Views.Pages.DichotomyMethodPage.UserControls;
using ClosedXML.Excel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Ookii.Dialogs.Wpf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace _3_Semester_CSharpMath_WPF.ViewModels.Pages.LeastSquareMethodPage
{
    partial class LeastSquareMethodPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private DataView _dataView = new DataView();
        public DataTable DataTable = new DataTable("Data");
        public DataTable CurrentDataTable = new DataTable();
        public int LastColumnRow = 3;
        public ICommand SolveCommand { get; }
        public ICommand AddToTableCommand { get; }
        public ICommand RemoveFromTableCommand { get; }
        public ICommand ClearTableCommand { get; }
        public ICommand GetDataFromExcelCommand { get; }
        [ObservableProperty]
        private bool _isLinearRegression;
        [ObservableProperty]
        private bool _isQuadraticRegression;
        [ObservableProperty]
        private string _linearRegressionFunction;
        [ObservableProperty]
        private string _quadraticRegressionFunction;
        [ObservableProperty]
        private string _answerCountDigitsAfterPoint;
        [ObservableProperty]
        private Visibility _linearRegressionVisibility = Visibility.Hidden;
        [ObservableProperty]
        private Visibility _quadraticRegressionVisibility = Visibility.Hidden;
        [ObservableProperty]
        private Visibility _chartVisibility = Visibility.Hidden;
        [ObservableProperty]
        private ChartView _chartView;

        public LeastSquareMethodPageViewModel()
        {
            InitTable();
            TESTInitTable();
            SolveCommand = new RelayCommand(Solve);
            AddToTableCommand = new RelayCommand(AddToTable);
            RemoveFromTableCommand = new RelayCommand(RemoveFromTable);
            ClearTableCommand = new RelayCommand(ClearTable);
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

        private void TESTInitTable()
        {
            DataTable dataTable = new DataTable();
            double[,] data = new double[2 , 7]
            { 
                { 7, 31, 61, 99, 129, 178, 209},
                { 13, 10, 9, 10, 12, 20, 26 }
            };

            dataTable.Columns.Add("x1", typeof(double));
            dataTable.Columns.Add("x2", typeof(double));
            dataTable.Columns.Add("x3", typeof(double));
            dataTable.Columns.Add("x4", typeof(double));
            dataTable.Columns.Add("x5", typeof(double));
            dataTable.Columns.Add("x6", typeof(double));
            dataTable.Columns.Add("x7", typeof(double));

            dataTable.Rows.Add();
            dataTable.Rows.Add();

            for (int row = 0; row < dataTable.DefaultView.Table.Rows.Count; ++row)
            {
                for (int col = 0; col < dataTable.DefaultView.Table.Columns.Count; ++col)
                {
                    dataTable.DefaultView.Table.Rows[row][col] = data[row, col];
                }
            }

            DataView = dataTable.DefaultView;
            DataTable = dataTable;
        }

        public void Solve()
        {
            LinearRegressionVisibility = Visibility.Collapsed;
            QuadraticRegressionVisibility = Visibility.Collapsed;
            ChartVisibility = Visibility.Collapsed;

            int rows = DataView.Table.DefaultView.Table.Rows.Count;
            int cols = DataView.Table.DefaultView.Table.Columns.Count;
            double[] independentVariable = new double[cols];

            double[] dependentVariable = new double[cols];

            for (int row = 0; row < rows; ++row)
            {
                for (int col = 0; col < cols; ++col)
                {
                    if (row == 0)
                    {
                        independentVariable[col] = DataTable.Rows[row][col] != DBNull.Value
                                                                ? (double)DataTable.Rows[row][col]
                                                                : 0.0;
                    }
                    else
                    {
                        dependentVariable[col] = DataTable.Rows[row][col] != DBNull.Value
                                        ? (double)DataTable.Rows[row][col]
                                        : 0.0;
                    }
                    

                }
            }
            try
            {
                if (!double.TryParse(AnswerCountDigitsAfterPoint, out double answerCountDigitsAfterPointDouble))
                {
                    throw new FormatException("Неверный формат для точности коэффициентов. Пожалуйста, введите число.");
                }
                if (!IsLinearRegression && !IsQuadraticRegression)
                {
                    throw new ArgumentException("Выберите функцию!");
                }

                if (IsLinearRegression)
                {
                    ChartModel.IsLinearRegression = true;
                    LinearRegressionVisibility = Visibility.Visible;
                    LeastSquareMethod.LinearRegression.Fit(independentVariable, dependentVariable);
                    LinearRegressionFunction = String.Concat("y = ", LeastSquareMethod.LinearRegression.Slope.ToString("F" + AnswerCountDigitsAfterPoint), 
                                                            "x + ", LeastSquareMethod.LinearRegression.Intercept.ToString("F" + AnswerCountDigitsAfterPoint));
                }
                if (IsQuadraticRegression)
                {
                    ChartModel.IsQuadraticRegression = true;
                    QuadraticRegressionVisibility = Visibility.Visible;
                    LeastSquareMethod.QuadraticRegression.Fit(independentVariable, dependentVariable);
                    QuadraticRegressionFunction = String.Concat("y = ", LeastSquareMethod.QuadraticRegression.QuadraticCoefficient.ToString("F" + AnswerCountDigitsAfterPoint),
                                                            "x^2 + ", LeastSquareMethod.QuadraticRegression.LinearCoefficient.ToString("F" + AnswerCountDigitsAfterPoint), "x + ",
                                                            LeastSquareMethod.QuadraticRegression.ConstantTerm.ToString("F" + AnswerCountDigitsAfterPoint));
                }

                ChartVisibility = Visibility.Visible;
                ChartView = new ChartView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddToTable()
        {
            try
            {
                //if (LastColumnRow >= 51)
                //{
                //    throw new Exception("Размер матрицы: 2<=N<=50!");
                //}

                DataTable dataTable = new DataTable();

                CurrentDataTable = DataView.Table;

                ++LastColumnRow;

                dataTable.Columns.Add("x1", typeof(double));
                for (int columnIndex = 2; columnIndex < LastColumnRow + 1; ++columnIndex)
                {
                    dataTable.Columns.Add("x" + columnIndex.ToString(), typeof(double));
                }

                dataTable.Rows.Add();
                dataTable.Rows.Add();

                // наполнение прошлыми данными
                for (int row = 0; row < CurrentDataTable.Rows.Count; ++row)
                {
                    for (int col = 0; col < CurrentDataTable.Columns.Count; ++col)
                    {
                        dataTable.DefaultView.Table.Rows[row][col] = CurrentDataTable.Rows[row][col];
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
                if (LastColumnRow <= 2)
                {
                    throw new Exception("Размер таблицы должен быть больше 1!");
                }

                DataTable dataTable = new DataTable();

                CurrentDataTable = DataView.Table;

                --LastColumnRow;

                dataTable.Columns.Add("x1", typeof(double));
                for (int columnIndex = 2; columnIndex < LastColumnRow + 1; ++columnIndex)
                {
                    dataTable.Columns.Add("x" + columnIndex.ToString(), typeof(double));
                }

                dataTable.Rows.Add();
                dataTable.Rows.Add();

                // наполнение прошлыми данными
                for (int row = 0; row < dataTable.DefaultView.Table.Rows.Count; ++row)
                {
                    for (int col = 0; col < dataTable.DefaultView.Table.Columns.Count; ++col)
                    {
                        dataTable.DefaultView.Table.Rows[row][col] = CurrentDataTable.Rows[row][col];
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
    }
}
