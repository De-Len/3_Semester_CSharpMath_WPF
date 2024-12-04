using _3_Semester_CSharpMath_WPF.ViewModels.Pages.CoordinateDescentMethodPage.UserControls;
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

namespace _3_Semester_CSharpMath_WPF.Views.Pages.CoordinateDescentMethodPage.UserConrtol
{
    /// <summary>
    /// Interaction logic for Chart3DView.xaml
    /// </summary>
    public partial class Chart3DView : UserControl
    {
        internal Chart3DViewModel Chart3DViewModel;
        public Chart3DView()
        {
            Chart3DViewModel = new Chart3DViewModel();
            InitializeComponent();
        }
    }
}
