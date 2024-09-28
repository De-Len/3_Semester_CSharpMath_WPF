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
using _3_Semester_CSharpMath_WPF.ViewModels.Pages.DichotomyMethodPage.UserControls;

namespace _3_Semester_CSharpMath_WPF.Views.Pages.DichotomyMethodPage.UserControls
{
    /// <summary>
    /// Interaction logic for ChartView.xaml
    /// </summary>
    public partial class ChartView : UserControl
    {
        internal ChartViewModel ViewModel { get; set; }
        public ChartView()
        {
            ViewModel = new ChartViewModel();
            this.DataContext = ViewModel;

            InitializeComponent();
        }
    }
}
