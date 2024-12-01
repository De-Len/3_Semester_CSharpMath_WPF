using CommunityToolkit.Mvvm.ComponentModel;
using Semestr_3_Lab_0_WPF.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Semestr_3_Lab_0_WPF.ViewModels
{
    class SecondControlViewModel : ObservableObject
    {
        public SecondControlViewModel()
        {
            SmallestFlowerName = "Не задано";

            OpenFolderDialogCommand = new MyDelegateCommand(OpenFolderDialog, (object parameter) => true);
            OpenFileDialogCommand = new MyDelegateCommand(OpenFileDialog, (object parameter) => true);
            GenerateDataCommand = new MyDelegateCommand(GenerateData, (object parameter) => true);
            SaveDataCommand = new MyDelegateCommand(SaveData, (object parameter) => true);
            UploadDataCommand = new MyDelegateCommand(UploadData, (object parameter) => true);
        }

        public ICommand OpenFolderDialogCommand { get; }
        public ICommand OpenFileDialogCommand { get; }
        public ICommand GenerateDataCommand { get; }
        public ICommand SaveDataCommand {  get; }
        public ICommand UploadDataCommand { get; }




        private ObservableCollection<Flower> _flowers = new ObservableCollection<Flower>();
        public ObservableCollection<Flower> Flowers
        {
            get => _flowers;
            set => SetProperty(ref _flowers, value);
        }
        private string _smallestFlowerName;

        public string SmallestFlowerName
        {
            get => _smallestFlowerName;
            set => SetProperty(ref _smallestFlowerName, value);
        }

        private string _filePathToJson;

        public string FilePathToJson
        {
            get => _filePathToJson;
            set => SetProperty(ref _filePathToJson, value);
        }

        private string _filePathToFolder;

        public string FilePathToFolder
        {
            get => _filePathToFolder;
            set => SetProperty(ref _filePathToFolder, value);
        }

        public ObservableCollection<Flower> LoadCollectionData()
        {
            return Flowers;
        }

        public void OpenFolderDialog(object parameter)
        {
            Microsoft.Win32.OpenFolderDialog dlg = new Microsoft.Win32.OpenFolderDialog();

            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string folderName = dlg.FolderName;
                FilePathToFolder = folderName;
            }

            FilePathToFolder = FilePathToFolder;

        }
        public void OpenFileDialog(object parameter)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                FilePathToJson = filename;
            }
        }

        public void GenerateData(object parameter) 
        {
            Flowers = new ObservableCollection<Flower>(SecondControlModel.GenerateRandomFlowers(3));
            SetSmallestFlower();
        }
        public void SaveData(object parameter) 
        {
            DataSerializer<ObservableCollection<Flower>>.Serialize(Flowers, FilePathToFolder + "\\FlowersData.json");
        }
        public void UploadData(object parameter) 
        {
            Flowers = DataDeserializer<ObservableCollection<Flower>>.Deserialize(FilePathToJson);
            SetSmallestFlower();
        }

        private void SetSmallestFlower()
        {
            SmallestFlowerName = Flowers.OrderBy(flower => flower.FlowerSize).FirstOrDefault().FlowerName;
        }
    }
}
