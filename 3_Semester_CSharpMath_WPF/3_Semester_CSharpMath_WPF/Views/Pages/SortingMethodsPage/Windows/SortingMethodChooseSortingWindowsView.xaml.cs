﻿using _3_Semester_CSharpMath_WPF.ViewModels.Pages.SortingMethodsPage;
using _3_Semester_CSharpMath_WPF.ViewModels.Pages.SortingMethodsPage.Windows;
using System.Windows;

namespace _3_Semester_CSharpMath_WPF.Views.Pages.SortingMethodsPage.Windows
{
    /// <summary>
    /// Interaction logic for SortingMethodChooseSortingWindowsView.xaml
    /// </summary>
    public partial class SortingMethodChooseSortingWindowsView : Window
    {
        SortingMethodChooseSortingWindowsViewModel SortingMethodChooseSortingWindowsViewModel { get; }
        internal SortingMethodChooseSortingWindowsView(SortingMethodsPageViewModel sortingMethodsPageViewModel)
        {
            SortingMethodChooseSortingWindowsViewModel = new SortingMethodChooseSortingWindowsViewModel(sortingMethodsPageViewModel);

            this.DataContext = SortingMethodChooseSortingWindowsViewModel;
            InitializeComponent();
        }
    }
}