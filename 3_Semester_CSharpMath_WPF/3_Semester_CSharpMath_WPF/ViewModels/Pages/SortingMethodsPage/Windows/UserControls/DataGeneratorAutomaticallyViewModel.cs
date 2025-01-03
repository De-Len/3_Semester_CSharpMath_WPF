using _3_Semester_CSharpMath_WPF.Models;
using _3_Semester_CSharpMath_WPF.Models.Pages.SortingMethodsPage.Windows.UserControls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Ookii.Dialogs.Wpf;
using System.IO;
using System.Windows;
using System.Windows.Input;

// Singtone
namespace _3_Semester_CSharpMath_WPF.ViewModels.Pages.SortingMethodsPage.Windows.UserControls
{
    partial class DataGeneratorAutomaticallyViewModel : ObservableObject
    {
        public ICommand GenerateNumbersCommand { get; }

        [ObservableProperty]
        private string _numbersCount;
        [ObservableProperty]
        private string _startLimit;
        [ObservableProperty]
        private string _endLimit;
        [ObservableProperty]
        private string _accuracyCountDigitsAfterPoint;
        [ObservableProperty]
        private static int _progressBarValue;
        public static string FilePath {get; set;}

        // Статическое свойство для хранения единственного экземпляра класса
        private static DataGeneratorAutomaticallyViewModel _instance;

        // Статический метод для доступа к экземпляру (синглтон)
        public static DataGeneratorAutomaticallyViewModel Instance =>
            _instance ??= new DataGeneratorAutomaticallyViewModel();

        private DataGeneratorAutomaticallyViewModel()
        {
            GenerateNumbersCommand = new RelayCommand(GenerateNumbers);
            OpenFileSelectionCommand = new RelayCommand(OpenFileSelection);
            SaveFileCommand = new RelayCommand(SaveFile);
        }

        private void UpdateNumberTextBox()
        {
            NumberTextBox = string.Join(", ", SortingMethodsDataGeneratorWindowViewModel.NumbersCollection.Select(n => n.ToString()));
        }
        private async void GenerateNumbers()
        {
            try
            {
                File.WriteAllText(FilePath + "\\ДаныеДляТеста.txt", NumberTextBox);
            }
            catch
            {
                MessageBox.Show("Выберите папку для сохранения результатов!");
                return; 
            }

            // Обработчик события изменения коллекции (если это необходимо)
            NumberTextBox = "";
            SortingMethodsDataGeneratorWindowViewModel.NumbersCollection.CollectionChanged += (sender, e) => UpdateNumberTextBox();

            double startLimitDouble = double.NaN;
            double endLimitDouble = double.NaN;
            int accuracyCountDigitsAfterPointInt = 0;
            int numbersCountInt = 0;

            try
            {
                // Проверка и получение параметров
                (startLimitDouble, endLimitDouble, accuracyCountDigitsAfterPointInt, numbersCountInt) =
                    Helpers.CheckExceptionsSortingMethods(
                        startLimit: StartLimit,
                        endLimit: EndLimit,
                        accuracyCountDigitsAfterPoint: AccuracyCountDigitsAfterPoint,
                        numbersCount: NumbersCount
                    );

                // Генерация случайных чисел
                //await DataGeneratorAutomaticallyModel.GenerateRandomDoubles(numbersCountInt, startLimitDouble, endLimitDouble, accuracyCountDigitsAfterPointInt);

                // Обновление текстового поля с числами
                //NumberTextBox = string.Join(", ", SortingMethodsDataGeneratorWindowViewModel.NumbersCollection);

                await Task.Run(() => {
                    DataGeneratorAutomaticallyModel.GenerateRandomDoubles(numbersCountInt, startLimitDouble, 
                                                                          endLimitDouble, accuracyCountDigitsAfterPointInt);
                });
                // Обновление UI после завершения фоновой операции
                //NumberTextBox = string.Join(", ", SortingMethodsDataGeneratorWindowViewModel.NumbersCollection);
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка аргументов", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: " + ex.Message, "Неопределённая ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Manually

        [ObservableProperty]
        private string _txtFilePath = string.Empty;
        [ObservableProperty]
        private string _numberTextBox = string.Empty;

        //private string _numberTextBox = string.Empty;
        //public string NumberTextBox
        //{
        //    get => _numberTextBox;
        //    set => SetProperty(ref _numberTextBox, value);
        //}

        public ICommand OpenFileSelectionCommand { get; }
        public ICommand SaveFileCommand { get; }

        private void OpenFileSelection()
        {
            var dlg = new VistaOpenFileDialog
            {
                Title = "Выберите текстовый файл",
                Filter = "Текстовые файлы (*.txt)|*.txt",
                Multiselect = false // Возможность выбора нескольких файлов
            };

            // Открываем диалог и проверяем, было ли выбрано что-либо
            if (dlg.ShowDialog() == true)
            {
                string filePath = dlg.FileName;

                // Здесь вы можете работать с выбранным файлом
                // Пример: Чтение текстового файла
                string fileContent = File.ReadAllText(filePath);

                // TxtFilePath = filePath;

                NumberTextBox = fileContent;
            }
            
        }

        public void SaveFile()
        {
            // возможно, только на маке denied access, поэтому TODO
            try
            {
                File.WriteAllText(FilePath + "\\ДаныеДляТеста.txt", NumberTextBox);
            }
            catch
            {
                MessageBox.Show("Выберите папку для сохранения результатов!");
            }
        }
    }
}
