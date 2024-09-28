using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Xml.Linq;
using Wpf.Ui.Controls;
using _3_Semester_CSharpMath_WPF.Views.Windows;
using _3_Semester_CSharpMath_WPF.ViewModels.Windows;
using _3_Semester_CSharpMath_WPF.Views.Pages.DichotomyMethodPage;

namespace _3_Semester_CSharpMath_WPF.ViewModels.Windows
{
    public partial class MainWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _applicationTitle = string.Empty;
        [ObservableProperty]
        private ObservableCollection<object> _navigationFooter = [];
        [ObservableProperty]
        private ObservableCollection<object> _navigationItems = [];

        public MainWindowViewModel()
        {
            ApplicationTitle = "Prod by Gordei";

            NavigationItems =
            [
                new NavigationViewItem()
            {
                Content = "Метод Дихотомии",
                Icon = new SymbolIcon { Symbol = SymbolRegular.MathFormatLinear24 },
                TargetPageType = typeof(DichotomyMethodPageView) },

                //new NavigationViewItem()
                //{
                //    Content = "Player",
                //    Icon = new SymbolIcon { Symbol = SymbolRegular.PlayCircle24 },
                //    TargetPageType = typeof(MainWindowView) },
            ];
            //    },
            //    new NavigationViewItem()
            //    {
            //        Content = "Data",
            //        Icon = new SymbolIcon { Symbol = SymbolRegular.DataHistogram24 },
            //        TargetPageType = typeof(Views.Pages.DataPage)
            //    },
            //];

            //    NavigationFooter =
            //    [
            //        new NavigationViewItem()
            //    {
            //        Content = "Settings",
            //        Icon = new SymbolIcon { Symbol = SymbolRegular.Settings24 },
            //        TargetPageType = typeof(Views.Pages.SettingsPage)
            //    },
            //];

            //TrayMenuItems = [new() { Header = "Home", Tag = "tray_home" }];

            //SaveActionsCommand = new MyDelegateCommand(SaveActions, CanSaveActions);

            //_isInitialized = true;
        }
    }
}
