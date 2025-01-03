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
using System.Windows.Navigation;
using System.Windows.Shapes;
using _3_Semester_CSharpMath_WPF.ViewModels.Pages.LeastSquareMethodPage;

namespace _3_Semester_CSharpMath_WPF.Views.Pages.LeastSquareMethodPage
{
    /// <summary>
    /// Логика взаимодействия для LeastSquareMethodPageView.xaml
    /// </summary>
    public partial class LeastSquareMethodPageView : Page
    {
        LeastSquareMethodPageViewModel LeastSquareMethodPageViewModel;
        public LeastSquareMethodPageView()
        {
            LeastSquareMethodPageViewModel = new LeastSquareMethodPageViewModel();
            this.DataContext = LeastSquareMethodPageViewModel;

            InitializeComponent();
        }
    }
}
