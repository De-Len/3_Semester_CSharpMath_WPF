using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Semestr_3_Lab_0_WPF.ViewModels
{
    class FisrtControlViewModel : ObservableObject
    {
        public FisrtControlViewModel()
        {

        }

        private double _cubeEdge;
        public double CubeEdge
        {
            get { return _cubeEdge; }
            set
            {
                if (SetProperty(ref _cubeEdge, value))
                {
                    OnPropertyChanged(nameof(CubeWeight));
                }
            }
        }

        private double _cubeDefect;
        public double CubeDefect
        {
            get { return _cubeDefect; }
            set
            {
                if (SetProperty(ref _cubeDefect, value))
                {
                    OnPropertyChanged(nameof(CubeWeight));
                }
            }

        }

        private double _cubeDensity;
        public double CubeDensity
        {
            get { return _cubeDensity; }
            set
            {
                if (SetProperty(ref _cubeDensity, value))
                {
                    OnPropertyChanged(nameof(CubeWeight));
                }
            }
        }

        public double CubeWeight => CalculateWeight();

        private double CalculateWeight()
        {
            // Формула: M = ρ * (a³ - πd³ / 6)
            return CubeDensity * (Math.Pow(CubeEdge, 3) - (Math.PI * Math.Pow(CubeDefect, 3) / 6));
        }
    }
}
