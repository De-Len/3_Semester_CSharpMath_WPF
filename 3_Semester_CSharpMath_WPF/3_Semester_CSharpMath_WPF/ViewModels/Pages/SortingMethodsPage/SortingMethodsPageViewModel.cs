using _3_Semester_CSharpMath_WPF.ViewModels.Pages.SortingMethodsPage.Windows.UserControls;
using _3_Semester_CSharpMath_WPF.Views.Pages.SortingMethodsPage.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Ookii.Dialogs.Wpf;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace _3_Semester_CSharpMath_WPF.ViewModels.Pages.SortingMethodsPage
{
    partial class SortingMethodsPageViewModel : ObservableObject
    {
        public ICommand OpenSortingWindowCommand { get; }
        public ICommand OpenFolderSelectionCommand { get; }
        public ICommand OpenDataGeneratorWindowCommand { get; }

        public static ObservableCollection<DataGridSortColumn> DataGrid { get; set; }

        private SortingMethodsDataGeneratorWindowView _sortingMethodsDataGeneratorWindowView;

        [ObservableProperty]
        private string _bubbleSortOutput;

        [ObservableProperty]
        private string _insertionSortOutput;

        [ObservableProperty]
        private string _cocktailShakerSortOutput;

        [ObservableProperty]
        private string _quickSortOutput;

        [ObservableProperty]
        private string _bogoSortOutput;


        public SortingMethodsPageViewModel()
        {
            DataGrid = new ObservableCollection<DataGridSortColumn>
            {
                new DataGridSortColumn { IsSelected = true, SortMethodName = "Пузырьковая", Timing = 0, Iterations = 0},
                new DataGridSortColumn { IsSelected = true, SortMethodName = "Вставками", Timing = 0, Iterations = 0 },
                new DataGridSortColumn { IsSelected = true, SortMethodName = "Шейкерная", Timing = 0, Iterations = 0 },
                new DataGridSortColumn { IsSelected = true, SortMethodName = "Быстрая", Timing = 0, Iterations = 0 },
                new DataGridSortColumn { IsSelected = true, SortMethodName = "BOGO", Timing = 0, Iterations = 0 }
            };

            OpenSortingWindowCommand = new RelayCommand(OpenSortingWindow);
            OpenFolderSelectionCommand = new RelayCommand(OpenFolderSelection);
            OpenDataGeneratorWindowCommand = new RelayCommand(OpenDataGeneratorWindow);
        }

        public void OpenSortingWindow()
        {
            SortingMethodChooseSortingWindowsView sortingMethodChooseSortingWindowsView = new SortingMethodChooseSortingWindowsView(this);
            sortingMethodChooseSortingWindowsView.Show();
        }
        public void OpenFolderSelection()
        {
            var dialog = new VistaFolderBrowserDialog();
            if (dialog.ShowDialog() == true)
            {
                string folderPath = dialog.SelectedPath;
                DataGeneratorAutomaticallyViewModel.FilePath = folderPath;
            }
        }
        public void OpenDataGeneratorWindow()
        {
            _sortingMethodsDataGeneratorWindowView = new SortingMethodsDataGeneratorWindowView();
            _sortingMethodsDataGeneratorWindowView.Show();
        }

        public static void AddIteration(int sortingIndex)
        {
            ++DataGrid[sortingIndex].Iterations;
        }
        public static void CleanIterations()
        {
            DataGrid[0].Iterations = 0;
            DataGrid[1].Iterations = 0;
            DataGrid[2].Iterations = 0;
            DataGrid[3].Iterations = 0;
            DataGrid[4].Iterations = 0;
        }
    }

    public class DataGridSortColumn : ObservableObject
    {
        private string _sortMethodName;
        private long _timing;
        private int _iterations;
        private bool _isSelected;

        public string SortMethodName
        {
            get => _sortMethodName;
            set => SetProperty(ref _sortMethodName, value);
        }

        public long Timing
        {
            get => _timing;
            set => SetProperty(ref _timing, value);
        }

        public int Iterations
        {
            get => _iterations;
            set => SetProperty(ref _iterations, value);
        }

        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }
    }
}
