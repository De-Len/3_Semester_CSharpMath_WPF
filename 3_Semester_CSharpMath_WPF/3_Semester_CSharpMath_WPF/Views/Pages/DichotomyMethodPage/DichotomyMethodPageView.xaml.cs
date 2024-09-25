using _3_Semester_CSharpMath_WPF.ViewModels.Pages.DichotomyMethodPage;
using System.Windows.Controls;

namespace _3_Semester_CSharpMath_WPF.Views.Pages.DichotomyMethodPage
{
    /// <summary>
    /// Interaction logic for DichotomyMethodPageView.xaml
    /// </summary>
    public partial class DichotomyMethodPageView : Page
    {
        internal DichotomyMethodPageViewModel ViewModel { get; set; }
        public DichotomyMethodPageView()
        {
            ViewModel = new DichotomyMethodPageViewModel();
            this.DataContext = ViewModel;

            InitializeComponent();
        }
    }
}
