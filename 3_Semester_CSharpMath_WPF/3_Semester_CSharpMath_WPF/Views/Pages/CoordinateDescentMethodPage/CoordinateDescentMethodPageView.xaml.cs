using _3_Semester_CSharpMath_WPF.ViewModels.Pages.CoordinateDescentMethodPage;
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

namespace _3_Semester_CSharpMath_WPF.Views.Pages.CoordinateDescentMethodPage
{
    /// <summary>
    /// Interaction logic for CoordinateDescentMethodPageView.xaml
    /// </summary>
    public partial class CoordinateDescentMethodPageView : Page
    {
        internal CoordinateDescentMethodPageViewModel CoordinateDescentMethodPageViewModel;
        public CoordinateDescentMethodPageView()
        {
            CoordinateDescentMethodPageViewModel = new CoordinateDescentMethodPageViewModel();
            this.DataContext = CoordinateDescentMethodPageViewModel;

            InitializeComponent();
        }
    }
}
