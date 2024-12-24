using _3_Semester_CSharpMath_WPF.ViewModels.Pages.LinearEquationsSystemMethodsPage;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Wpf.Ui.Controls;


namespace _3_Semester_CSharpMath_WPF.Views.Pages.LinearEquationsSystemMethodsPage
{
    /// <summary>
    /// Interaction logic for LinearEquationsSystemMethodsPageView.xaml
    /// </summary>
    public partial class LinearEquationsSystemMethodsPageView : Page
    {
        LinearEquationsSystemMethodsPageViewModel LinearEquationsSystemMethodsPageViewModel;
        public LinearEquationsSystemMethodsPageView()
        {
            LinearEquationsSystemMethodsPageViewModel = new LinearEquationsSystemMethodsPageViewModel();
            this.DataContext = LinearEquationsSystemMethodsPageViewModel;

            InitializeComponent();
        }
    }
}
