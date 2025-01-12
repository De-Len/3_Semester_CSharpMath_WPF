using _3_Semester_CSharpMath_WPF.ViewModels.Pages.LinearEquationsSystemMethodsPage;
using _3_Semester_CSharpMath_WPF.ViewModels.Pages.LinearEquationsSystemMethodsPage.Windows;
using System.Windows;

namespace _3_Semester_CSharpMath_WPF.Views.Pages.LinearEquationsSystemMethodsPage.Windows
{
    /// <summary>
    /// Логика взаимодействия для LinearEquationsSystemMethodsDataGeneratorWindowView.xaml
    /// </summary>
    public partial class LinearEquationsSystemMethodsDataGeneratorWindowView : Window
    {
        private LinearEquationsSystemMethodsDataGeneratorWindowViewModel _linearEquationsSystemMethodsDataGeneratorWindowViewModel;
        internal LinearEquationsSystemMethodsDataGeneratorWindowView(LinearEquationsSystemMethodsPageViewModel linearEquationsSystemMethodsPageViewModel)
        {
            _linearEquationsSystemMethodsDataGeneratorWindowViewModel = new LinearEquationsSystemMethodsDataGeneratorWindowViewModel(linearEquationsSystemMethodsPageViewModel);

            this.DataContext = _linearEquationsSystemMethodsDataGeneratorWindowViewModel;
            InitializeComponent();
        }
    }
}
