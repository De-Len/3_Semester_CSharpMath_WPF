using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3_Semester_CSharpMath_WPF.ViewModels.Pages.SortingMethodsPage
{
    public class SortingMethodsPageViewModel : ObservableObject
    {
        public ObservableCollection<Person> DataGrid { get; set; }
        public SortingMethodsPageViewModel()
        {
            DataGrid = new ObservableCollection<Person>
            {
                new Person { IsSelected = true, SortMethodName = "Пузырьковая", Timing = 0},
                new Person { IsSelected = true, SortMethodName = "Вставками", Timing = 0 },
                new Person { IsSelected = true, SortMethodName = "Быстрая", Timing = 0 },
                new Person { IsSelected = true, SortMethodName = "Шейкерная", Timing = 0 },
                new Person { IsSelected = true, SortMethodName = "Быстрая", Timing = 0 }
            };
        }
    }

    public class Person
    {
        public string SortMethodName { get; set; }
        public int Timing { get; set; }
        public bool IsSelected {  get; set; }
    }
}
