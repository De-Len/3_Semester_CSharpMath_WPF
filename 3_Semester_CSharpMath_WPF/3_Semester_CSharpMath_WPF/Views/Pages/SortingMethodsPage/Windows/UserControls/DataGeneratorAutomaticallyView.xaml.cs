using _3_Semester_CSharpMath_WPF.ViewModels.Pages.SortingMethodsPage.Windows.UserControls;
using System.Windows.Controls;

namespace _3_Semester_CSharpMath_WPF.Views.Pages.SortingMethodsPage.Windows.UserControls
{
    /// <summary>
    /// Interaction logic for DataGeneratorAutomaticallyView.xaml
    /// </summary>
    /// 
    public partial class DataGeneratorAutomaticallyView : UserControl
    {
        //DataGeneratorAutomaticallyViewModel DataGeneratorAutomaticallyViewModel;
        public DataGeneratorAutomaticallyView()
        {
            //DataGeneratorAutomaticallyViewModel = new DataGeneratorAutomaticallyViewModel();

            this.DataContext = DataGeneratorAutomaticallyViewModel.Instance;
            InitializeComponent();
        }
    }
}
