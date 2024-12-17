using LiveChartsCore.Defaults;
using LiveChartsCore.Drawing;
using LiveChartsCore.SkiaSharpView.Painting.Effects;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _3_Semester_CSharpMath_WPF.Models.MathMethods;
using System.Threading.Tasks;

namespace _3_Semester_CSharpMath_WPF.Models.Pages.DichotomyMethodPage.UserControls
{
    class ChartModel
    {
        private static readonly SKColor s_dark = new(00, 00, 00);
        private static readonly SKColor s_gray1 = new(100, 100, 100);
        private static readonly SKColor s_gray2 = new(200, 200, 200);
        private static readonly SKColor s_white = new(255, 255, 255);

        public static float StartLimit { get; set; }    
        public static float EndLimit {  get; set; }

        //private static readonly SKColor s_gray = new(195, 195, 195);
        //private static readonly SKColor s_gray1 = new(160, 160, 160);
        //private static readonly SKColor s_gray2 = new(90, 90, 90);
        //private static readonly SKColor s_dark3 = new(60, 60, 60);

        public ISeries[] Series { get; set; } =
        {
        new LineSeries<ObservablePoint>
        {
            Values = Fetch(),
            Stroke = new SolidColorPaint(new SKColor(33, 150, 243), 4),
            Fill = null,
            GeometrySize = 0
        }
        };

        public Axis[] XAxes { get; set; } =
        {
        new Axis
        {
            Name = "X ось",
            NamePaint = new SolidColorPaint(s_gray1),
            TextSize = 18,
            Padding = new Padding(5, 15, 5, 5),
            LabelsPaint = new SolidColorPaint(s_dark),
            SeparatorsPaint = new SolidColorPaint
            {
                Color = s_dark,
                StrokeThickness = 1,
                PathEffect = new DashEffect(new float[] { 3, 3 })
            },
            SubseparatorsPaint = new SolidColorPaint
            {
                Color = s_gray2,
                StrokeThickness = 0.5f
            },
            SubseparatorsCount = 9,
            ZeroPaint = new SolidColorPaint
            {
                Color = s_gray1,
                StrokeThickness = 2
            },
            TicksPaint = new SolidColorPaint
            {
                Color = s_dark,
                StrokeThickness = 1.5f
            },
            SubticksPaint = new SolidColorPaint
            {
                Color = s_dark,
                StrokeThickness = 1
            }
        }
        };

        public Axis[] YAxes { get; set; } =
        {
        new Axis
        {
            Name = "Y ось",
            NamePaint = new SolidColorPaint(s_gray1),
            TextSize = 18,
            Padding = new Padding(5, 0, 15, 0),
            LabelsPaint = new SolidColorPaint(s_dark),
            SeparatorsPaint = new SolidColorPaint
            {
                Color = s_dark,
                StrokeThickness = 1,
                PathEffect = new DashEffect(new float[] { 3, 3 })
            },
            SubseparatorsPaint = new SolidColorPaint
            {
                Color = s_gray2,
                StrokeThickness = 0.5f
            },
            SubseparatorsCount = 9,
            ZeroPaint = new SolidColorPaint
            {
                Color = s_gray1,
                StrokeThickness = 2
            },
            TicksPaint = new SolidColorPaint
            {
                Color = s_dark,
                StrokeThickness = 1.5f
            },
            SubticksPaint = new SolidColorPaint
            {
                Color = s_dark,
                StrokeThickness = 1
            }
        }
        };

        public DrawMarginFrame Frame { get; set; } =
        new()
        {
            Fill = new SolidColorPaint(s_white),
            Stroke = new SolidColorPaint
            {
                Color = s_dark,
                StrokeThickness = 1
            }
        };
        private static List<ObservablePoint> Fetch()
        {
            var list = new List<ObservablePoint>();

            for (var x = StartLimit; x < EndLimit; x += 0.01f)
            {
                var y = MathMethodsGroup.SolveFunctionSingleArgument(x);
                list.Add(new ObservablePoint(x, y));
            }

            return list;

        }
    }
}
