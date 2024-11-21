using _3_Semester_CSharpMath_WPF.ViewModels.Pages.SortingMethodsPage;
using System.Windows.Controls;

namespace _3_Semester_CSharpMath_WPF.Views.Pages.SortingMethodsPage
{
    /// <summary>
    /// Interaction logic for SortingMethodsPageView.xaml
    /// </summary>
    public partial class SortingMethodsPageView : Page
    {
        internal SortingMethodsPageViewModel SortingMethodsPageViewModel;
        public SortingMethodsPageView()
        {
            SortingMethodsPageViewModel = new SortingMethodsPageViewModel();
            this.DataContext = SortingMethodsPageViewModel;
            
            InitializeComponent();
        }
    }
}
