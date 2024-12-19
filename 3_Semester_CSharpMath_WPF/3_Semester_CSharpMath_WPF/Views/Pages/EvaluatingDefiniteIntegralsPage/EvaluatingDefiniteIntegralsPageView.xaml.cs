using System.Windows.Controls;
using _3_Semester_CSharpMath_WPF.ViewModels.Pages.EvaluatingDefiniteIntegralsPage;

namespace _3_Semester_CSharpMath_WPF.Views.Pages.EvaluatingDefiniteIntegralsPage
{
    /// <summary>
    /// Interaction logic for EvaluatingDefiniteIntegralsPageView.xaml
    /// </summary>
    public partial class EvaluatingDefiniteIntegralsPageView : Page
    {
        internal EvaluatingDefiniteIntegralsPageViewModel EvaluatingDefiniteIntegralsPageViewModel;
        public EvaluatingDefiniteIntegralsPageView()
        {
            EvaluatingDefiniteIntegralsPageViewModel = new EvaluatingDefiniteIntegralsPageViewModel();
            this.DataContext = EvaluatingDefiniteIntegralsPageViewModel;    

            InitializeComponent();
        }
    }
}
