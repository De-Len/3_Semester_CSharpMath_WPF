//using CommunityToolkit.Mvvm.ComponentModel;
//using CommunityToolkit.Mvvm.Input;
//using Ookii.Dialogs.Wpf;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Input;
//using System.IO;
//using System.Configuration;
//using CommunityToolkit.Mvvm.ComponentModel;


//namespace _3_Semester_CSharpMath_WPF.ViewModels.Pages.SortingMethodsPage.Windows.UserControls
//{
//    partial class DataGeneratorManuallyViewModel : ObservableObject
//    {
//        [ObservableProperty]
//        private static string _txtFilePath = string.Empty;
//        [ObservableProperty]
//        private static string _numberTextBox = string.Empty;
//        public ICommand OpenFileSelectionCommand { get; }
//        public ICommand SaveFileCommand { get; }


//        public DataGeneratorManuallyViewModel()
//        {
//            OpenFileSelectionCommand = new RelayCommand(OpenFileSelection);
//            SaveFileCommand = new RelayCommand(SaveFile);
//        }

//        private void OpenFileSelection()
//        {
//            var dlg = new VistaOpenFileDialog
//            {
//                Title = "Выберите текстовый файл",
//                Filter = "Текстовые файлы (*.txt)|*.txt",
//                Multiselect = false // Возможность выбора нескольких файлов
//            };

//            // Открываем диалог и проверяем, было ли выбрано что-либо
//            if (dlg.ShowDialog() == true)
//            {
//                string filePath = dlg.FileName;

//                // Здесь вы можете работать с выбранным файлом
//                // Пример: Чтение текстового файла
//                string fileContent = File.ReadAllText(filePath);

//                TxtFilePath = filePath;
//                // Далее можно сделать что-то с содержимым файла
//                Console.WriteLine(fileContent);
//            }
//        }

//        public void SaveFile()
//        {

//        }
//    }
//}
