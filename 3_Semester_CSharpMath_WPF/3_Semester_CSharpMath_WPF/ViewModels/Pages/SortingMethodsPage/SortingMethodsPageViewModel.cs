using _3_Semester_CSharpMath_WPF.Views.Pages.SortingMethodsPage.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;
using System.Windows.Forms;

namespace _3_Semester_CSharpMath_WPF.ViewModels.Pages.SortingMethodsPage
{
    public class SortingMethodsPageViewModel : ObservableObject
    {
        public ICommand OpenSortingWindowCommand { get; }
        public ICommand OpenFolderSelectionCommand { get; }
        public ObservableCollection<Person> DataGrid { get; set; }
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
        }

        public void OpenSortingWindow()
        {
            SortingMethodChooseSortingWindowsView sortingMethodChooseSortingWindowsView = new SortingMethodChooseSortingWindowsView();
            sortingMethodChooseSortingWindowsView.Show();
        }
        public void OpenFolderSelection()
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();

            DialogResult result = folderBrowser.ShowDialog();

            if (!string.IsNullOrWhiteSpace(folderBrowser.SelectedPath))
            {
                string[] files = Directory.GetFiles(folderBrowser.SelectedPath);
            }
        }
    }

    public class Person
    {
        public string SortMethodName { get; set; }
        public int Timing { get; set; }
        public bool IsSelected { get; set; }
    }
}
