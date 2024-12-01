using _3_Semester_CSharpMath_WPF.ViewModels.Pages.SortingMethodsPage.Windows.UserControls;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3_Semester_CSharpMath_WPF.Views.Pages.SortingMethodsPage.Windows.UserControls;
using System.Windows;
using System.Collections.ObjectModel;

namespace _3_Semester_CSharpMath_WPF.ViewModels.Pages.SortingMethodsPage.Windows
{
    partial class SortingMethodsDataGeneratorWindowViewModel : ObservableObject
    {
        public static ObservableCollection<double> NumbersCollection { get; set; }
        private bool _isToggleButtonOn; 
        public bool IsToggleButtonOn
        {
            get => _isToggleButtonOn;
            set 
            {
                if (SetProperty(ref _isToggleButtonOn, value))
                {
                    UpdateDataGeneratorUserControlVisibility();
                }
            } 
        }

        [ObservableProperty]
        private DataGeneratorManuallyView _dataGeneratorManually;
        [ObservableProperty]
        private DataGeneratorAutomaticallyView _dataGeneratorAutomatically;

        [ObservableProperty]
        private Visibility _dataGeneratorManuallyVisibility = Visibility.Hidden;
        [ObservableProperty]
        private Visibility _dataGeneratorAutomaticallyVisibility = Visibility.Hidden;

        public SortingMethodsDataGeneratorWindowViewModel()
        {
            _dataGeneratorManually = new DataGeneratorManuallyView();
            _dataGeneratorAutomatically = new DataGeneratorAutomaticallyView();

            UpdateDataGeneratorUserControlVisibility();
        }

        private void UpdateDataGeneratorUserControlVisibility()
        {
            if (IsToggleButtonOn)
            {
                DataGeneratorManuallyVisibility = Visibility.Hidden;
                DataGeneratorAutomaticallyVisibility = Visibility.Visible;
            }
            else
            {
                DataGeneratorManuallyVisibility = Visibility.Visible;
                DataGeneratorAutomaticallyVisibility = Visibility.Hidden;
            }
        }
    }
}
