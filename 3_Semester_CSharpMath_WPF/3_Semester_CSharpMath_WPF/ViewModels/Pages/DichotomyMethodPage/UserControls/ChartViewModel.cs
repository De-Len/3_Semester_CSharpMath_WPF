using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3_Semester_CSharpMath_WPF.Models.Pages.DichotomyMethodPage;
using _3_Semester_CSharpMath_WPF.Models.Pages.DichotomyMethodPage.UserControls;
using System.Diagnostics.CodeAnalysis;

namespace _3_Semester_CSharpMath_WPF.ViewModels.Pages.DichotomyMethodPage.UserControls
{
    class ChartViewModel
    {
        private ChartModel _model;
        public required ISeries[] Series { get; set; }
        public required Axis[] XAxes { get; set; }
        public required Axis[] YAxes { get; set; }
        public required DrawMarginFrame Frame { get; set; }


        [SetsRequiredMembers]
        public ChartViewModel()
        {
            _model = new ChartModel();
            Series = _model.Series;
            XAxes = _model.XAxes;
            YAxes = _model.YAxes;
            Frame = _model.Frame;
        }
    }
}
