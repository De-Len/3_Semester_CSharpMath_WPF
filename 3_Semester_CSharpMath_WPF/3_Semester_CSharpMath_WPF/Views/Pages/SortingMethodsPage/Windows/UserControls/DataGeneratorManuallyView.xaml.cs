using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using _3_Semester_CSharpMath_WPF.ViewModels.Pages.SortingMethodsPage.Windows.UserControls;

namespace _3_Semester_CSharpMath_WPF.Views.Pages.SortingMethodsPage.Windows.UserControls
{
    /// <summary>
    /// Interaction logic for DataGeneratorManuallyView.xaml
    /// </summary>
    public partial class DataGeneratorManuallyView : UserControl
    {
        DataGeneratorManuallyViewModel DataGeneratorManuallyViewModel;
        public DataGeneratorManuallyView()
        {
            DataGeneratorManuallyViewModel = new DataGeneratorManuallyViewModel();
            this.DataContext = DataGeneratorManuallyViewModel;

            InitializeComponent();
        }
        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            // Установить высоту TextBox в зависимости от количества строк
            int lineCount = textBox.LineCount;
            double lineHeight = textBox.FontSize + 4; // например, добавьте немного для отступов
            double newHeight = lineHeight * lineCount;

            // Ограничить высоту 100 пикселями
            if (newHeight > 100)
                newHeight = 100;

            textBox.Height = newHeight;
        }
    }
}
