using _3_Semester_CSharpMath_WPF.Models.MathMethods;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Data;
using System.Security.RightsManagement;
using System.Windows;
using System.Windows.Input;
using static AngouriMath.MathS;

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




        public ICommand AddToTableCommand { get; }
        public ICommand RemoveFromTableCommand { get; }
        public ICommand ClearTableCommand { get; }
        public ICommand SolveEquationsCommand { get; }

        public LinearEquationsSystemMethodsPageViewModel()
        {
            InitTable();
            AddToTableCommand = new RelayCommand(AddToTable);
            RemoveFromTableCommand = new RelayCommand(RemoveFromTable);
            ClearTableCommand = new RelayCommand(ClearTable);
            SolveEquationsCommand = new RelayCommand(SolveEquations);
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

        public void SolveEquations()
        {
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

            if (IsSelectedGauss)
            {
                double[] resultGaussDoubles = LinearEquationsSystemMethods.GaussianElimination.Solve(coefficients, constants);

                for (int col = 0; col < cols - 1; ++col)
                {
                    ResultGauss += "x" + (col + 1).ToString() + " = " + resultGaussDoubles[col].ToString() + ", ";
                }
            }
            if (IsSelectedGaussJordan)
            {
                ResultGaussJordan = string.Join(", ", LinearEquationsSystemMethods.GaussJordanElimination.Solve(coefficients, constants));
            }
            if (IsSelectedCramer)
            {
                ResultCramer = string.Join(", ", LinearEquationsSystemMethods.Cramer.Solve(coefficients, constants));
            }
        }
    }
}