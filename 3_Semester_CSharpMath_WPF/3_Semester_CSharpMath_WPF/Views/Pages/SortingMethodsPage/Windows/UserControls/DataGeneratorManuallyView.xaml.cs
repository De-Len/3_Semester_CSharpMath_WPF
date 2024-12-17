using _3_Semester_CSharpMath_WPF.ViewModels.Pages.SortingMethodsPage.Windows.UserControls;
using System.Windows.Controls;

namespace _3_Semester_CSharpMath_WPF.Views.Pages.SortingMethodsPage.Windows.UserControls
{
    /// <summary>
    /// Interaction logic for DataGeneratorManuallyView.xaml
    /// </summary>
    public partial class DataGeneratorManuallyView : UserControl
    {
        //DataGeneratorAutomaticallyViewModel DataGeneratorAutomaticallyViewModel;
        public DataGeneratorManuallyView()
        {
            //DataGeneratorAutomaticallyViewModel = new DataGeneratorAutomaticallyViewModel();
            this.DataContext = DataGeneratorAutomaticallyViewModel.Instance;

            InitializeComponent();
        }
    }
}
