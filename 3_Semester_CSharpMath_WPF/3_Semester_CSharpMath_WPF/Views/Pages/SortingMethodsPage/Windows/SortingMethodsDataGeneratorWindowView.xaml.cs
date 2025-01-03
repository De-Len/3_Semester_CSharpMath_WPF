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
using _3_Semester_CSharpMath_WPF.ViewModels.Pages.SortingMethodsPage.Windows;

namespace _3_Semester_CSharpMath_WPF.Views.Pages.SortingMethodsPage.Windows
{
    /// <summary>
    /// Interaction logic for SortingMethodsDataGeneratorWindowView.xaml
    /// </summary>
    public partial class SortingMethodsDataGeneratorWindowView : Window
    {
        SortingMethodsDataGeneratorWindowViewModel SortingMethodsDataGeneratorWindowViewModel { get; }
        public SortingMethodsDataGeneratorWindowView()
        {
            SortingMethodsDataGeneratorWindowViewModel = new SortingMethodsDataGeneratorWindowViewModel();
            this.DataContext = SortingMethodsDataGeneratorWindowViewModel;

            InitializeComponent();
        }
    }
}
