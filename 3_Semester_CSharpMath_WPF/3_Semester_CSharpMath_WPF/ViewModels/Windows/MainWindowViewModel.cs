using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Xml.Linq;
using Wpf.Ui.Controls;
using _3_Semester_CSharpMath_WPF.Views.Windows;
using _3_Semester_CSharpMath_WPF.ViewModels.Windows;
using _3_Semester_CSharpMath_WPF.Views.Pages.DichotomyMethodPage;
using _3_Semester_CSharpMath_WPF.Views.Pages.GoldenSectionSearchPage;
using _3_Semester_CSharpMath_WPF.Views.Pages.NewtonMethodPage;
using _3_Semester_CSharpMath_WPF.Views.Pages.CoordinateDescentMethodPage;
using _3_Semester_CSharpMath_WPF.Views.Pages.SortingMethodsPage;
using _3_Semester_CSharpMath_WPF.Views.Pages.EvaluatingDefiniteIntegralsPage;
using _3_Semester_CSharpMath_WPF.Views.Pages.LinearEquationsSystemMethodsPage;

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
                Icon = new SymbolIcon { Symbol = SymbolRegular.ClipboardMathFormula20 },
                TargetPageType = typeof(DichotomyMethodPageView) 
                },

                new NavigationViewItem()
                {
                    Content = "Метод золотого сечения",
                    Icon = new SymbolIcon { Symbol = SymbolRegular.ClipboardMathFormula16 },
                    TargetPageType = typeof(GoldenSectionSearchPageView) 
                },

                new NavigationViewItem()
                {
                    Content = "Метод Ньютона",
                    Icon = new SymbolIcon { Symbol = SymbolRegular.MathFormatProfessional16 },
                    TargetPageType = typeof(NewtonMethodPageView) 
                },
                new NavigationViewItem()
                {
                    Content = "Метод покоординатного спуска",
                    Icon = new SymbolIcon { Symbol = SymbolRegular.ClipboardMathFormula24 },
                    TargetPageType = typeof(CoordinateDescentMethodPageView) 
                },
                new NavigationViewItem()
                {
                    Content = "Методы сортировки",
                    Icon = new SymbolIcon { Symbol = SymbolRegular.ArrowCircleUp20 },
                    TargetPageType = typeof(SortingMethodsPageView) 
                },
                    new NavigationViewItem()
                {
                    Content = "Вычисление определённых интегралов",
                    Icon = new SymbolIcon { Symbol = SymbolRegular.Calendar20 },
                    TargetPageType = typeof(EvaluatingDefiniteIntegralsPageView) 
                },
                    new NavigationViewItem()
                {
                    Content = "Методы решения СЛАУ",
                    Icon = new SymbolIcon { Symbol = SymbolRegular.SlideGrid20 },
                    TargetPageType = typeof(LinearEquationsSystemMethodsPageView)
                },
                new NavigationViewItem()
                {
                    Content = "Метод наименьших квадратов",
                    Icon = new SymbolIcon { Symbol = SymbolRegular.LocationTargetSquare24 },
                    TargetPageType = typeof(LinearEquationsSystemMethodsPageView)
                }
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
