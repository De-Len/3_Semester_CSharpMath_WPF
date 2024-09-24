using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using CommunityToolkit.Mvvm.ComponentModel;

namespace _3_Semester_CSharpMath_WPF.ViewModels.Pages.DichotomyMethodPage
{
    class DichotomyMethodPageViewModel : ObservableObject
    {
        public ISeries[] Series { get; set; }
            = new ISeries[]
            {
                new LineSeries<double>
                {
                    Values = new double[] { -22, 1, 3, 5, 3, 4, 60 },
                    Fill = null
                }
            };

        public DichotomyMethodPageViewModel()
        {

        }
    }
}
