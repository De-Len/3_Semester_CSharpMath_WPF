using _3_Semester_CSharpMath_WPF.ViewModels.Pages.NewtonMethodPage;
using System.Windows.Controls;

namespace _3_Semester_CSharpMath_WPF.Views.Pages.NewtonMethodPage
{
    /// <summary>
    /// Interaction logic for NewtonMethodPageView.xaml
    /// </summary>
    public partial class NewtonMethodPageView : Page
    {
        internal NewtonMethodPageViewModel NewtonMethodPageViewModel;
        public NewtonMethodPageView()
        {
            NewtonMethodPageViewModel = new NewtonMethodPageViewModel();
            this.DataContext = NewtonMethodPageViewModel;

            InitializeComponent();
        }
    }
}
