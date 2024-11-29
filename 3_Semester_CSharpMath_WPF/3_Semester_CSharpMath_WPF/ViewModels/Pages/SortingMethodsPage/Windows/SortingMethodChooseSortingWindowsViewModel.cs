using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Controls;
using System.Windows.Input;


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
            if (_isAscendingChecked)
            {

            }
            else if (_isDescendingChecked)
            {

            }
        }
    }
}
