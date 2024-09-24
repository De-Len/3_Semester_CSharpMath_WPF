using _3_Semester_CSharpMath_WPF.ViewModels.Pages.DichotomyMethodPage;
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

namespace _3_Semester_CSharpMath_WPF.Views.Pages.DichotomyMethodPage
{
    /// <summary>
    /// Interaction logic for DichotomyMethodPageView.xaml
    /// </summary>
    public partial class DichotomyMethodPageView : Page
    {
        internal DichotomyMethodPageViewModel ViewModel {  get; set; }
        public DichotomyMethodPageView()
        {
            ViewModel = new DichotomyMethodPageViewModel();
            this.DataContext = ViewModel;

            InitializeComponent();
        }
    }
}
