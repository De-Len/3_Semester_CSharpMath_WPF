using CommunityToolkit.Mvvm.ComponentModel;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.Drawing;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.Painting.Effects;
using _3_Semester_CSharpMath_WPF.Models.Pages.DichotomyMethodPage;
using SkiaSharp;
using System.Diagnostics.CodeAnalysis;
using AngouriMath.Extensions;

namespace _3_Semester_CSharpMath_WPF.ViewModels.Pages.DichotomyMethodPage
{
    partial class DichotomyMethodPageViewModel : ObservableObject
    {
        private DichotomyMethodPageModel _model = new();

        public required ISeries[] Series { get; set; }
        public required Axis[] XAxes { get; set; }
        public required Axis[] YAxes { get; set; }
        public required DrawMarginFrame Frame { get; set; }

        [SetsRequiredMembers]
        public DichotomyMethodPageViewModel()
        {
            Series = _model.Series;
            XAxes = _model.XAxes;
            YAxes = _model.YAxes;
            Frame = _model.Frame;

        }

        //private string _userMathFormula = string.Empty;
        //public string UserMathFormula 
        //{
        //    get => _userMathFormula;
        //    set => SetProperty(ref _userMathFormula, value);
        //}

        [ObservableProperty]
        private string _userMathFormula = string.Empty;
    }
}
