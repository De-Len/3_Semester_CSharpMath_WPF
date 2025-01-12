using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using _3_Semester_CSharpMath_WPF.ViewModels.Pages.LinearEquationsSystemMethodsPage;
using _3_Semester_CSharpMath_WPF.ViewModels.Pages.LinearEquationsSystemMethodsPage.Windows;

namespace _3_Semester_CSharpMath_WPF.Views.Pages.LinearEquationsSystemMethodsPage.Windows
{
    /// <summary>
    /// Логика взаимодействия для LinearEquationsSystemMethodsGoogleSheetWindowView.xaml
    /// </summary>
    public partial class LinearEquationsSystemMethodsGoogleSheetWindowView : Window
    {
        LinearEquationsSystemMethodsGoogleSheetWindowViewModel LinearEquationsSystemMethodsGoogleSheetWindowViewModel;
        internal LinearEquationsSystemMethodsGoogleSheetWindowView(LinearEquationsSystemMethodsPageViewModel linearEquationsSystemMethodsPageViewModel)
        {
            LinearEquationsSystemMethodsGoogleSheetWindowViewModel = new LinearEquationsSystemMethodsGoogleSheetWindowViewModel(linearEquationsSystemMethodsPageViewModel);  

            this.DataContext = LinearEquationsSystemMethodsGoogleSheetWindowViewModel;
            InitializeComponent();
        }
    }
}
