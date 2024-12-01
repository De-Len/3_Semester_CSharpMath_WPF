using _3_Semester_CSharpMath_WPF.Views.Pages.SortingMethodsPage.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Ookii.Dialogs.Wpf;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace _3_Semester_CSharpMath_WPF.ViewModels.Pages.SortingMethodsPage
{
    public class SortingMethodsPageViewModel : ObservableObject
    {
        public ICommand OpenSortingWindowCommand { get; }
        public ICommand OpenFolderSelectionCommand { get; }
        public ICommand OpenDataGeneratorWindowCommand { get; }

        public ObservableCollection<Person> DataGrid { get; set; }
        private SortingMethodsDataGeneratorWindowView _sortingMethodsDataGeneratorWindowView;
        public SortingMethodsPageViewModel()
        {
            DataGrid = new ObservableCollection<Person>
            {
                new Person { IsSelected = true, SortMethodName = "Пузырьковая", Timing = 0},
                new Person { IsSelected = true, SortMethodName = "Вставками", Timing = 0 },
                new Person { IsSelected = true, SortMethodName = "Быстрая", Timing = 0 },
                new Person { IsSelected = true, SortMethodName = "Шейкерная", Timing = 0 },
                new Person { IsSelected = true, SortMethodName = "Быстрая", Timing = 0 }
            };

            OpenSortingWindowCommand = new RelayCommand(OpenSortingWindow);
            OpenFolderSelectionCommand = new RelayCommand(OpenFolderSelection);
            OpenDataGeneratorWindowCommand = new RelayCommand(OpenDataGeneratorWindow);
        }

        public void OpenSortingWindow()
        {
            SortingMethodChooseSortingWindowsView sortingMethodChooseSortingWindowsView = new SortingMethodChooseSortingWindowsView();
            sortingMethodChooseSortingWindowsView.Show();
        }
        public void OpenFolderSelection()
        {
            var dialog = new VistaFolderBrowserDialog();
            if (dialog.ShowDialog() == true)
            {
                string folderPath = dialog.SelectedPath;
            }
        }
        public void OpenDataGeneratorWindow()
        {
            _sortingMethodsDataGeneratorWindowView = new SortingMethodsDataGeneratorWindowView();
            _sortingMethodsDataGeneratorWindowView.Show();
        }
    }

    public class Person
    {
        public string SortMethodName { get; set; }
        public int Timing { get; set; }
        public bool IsSelected { get; set; }
    }
}
