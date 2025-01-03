using _3_Semester_CSharpMath_WPF.ViewModels.Pages.LeastSquareMethodPage;
using _3_Semester_CSharpMath_WPF.ViewModels.Pages.LeastSquareMethodPage.Windows;
using _3_Semester_CSharpMath_WPF.ViewModels.Pages.SortingMethodsPage.Windows.UserControls;
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

namespace _3_Semester_CSharpMath_WPF.Views.Pages.LeastSquareMethodPage.Windows
{
    /// <summary>
    /// Interaction logic for LeastSquareMethodDataGeneratorWindowView.xaml
    /// </summary>
    public partial class LeastSquareMethodDataGeneratorWindowView : Window
    {
        internal LeastSquareMethodDataGeneratorWindowView(LeastSquareMethodPageViewModel LeastSquareMethodPageViewModel)
        {
            this.DataContext = LeastSquareMethodDataGeneratorWindowViewModel.GetInstance(LeastSquareMethodPageViewModel);

            InitializeComponent();
        }
    }
}
