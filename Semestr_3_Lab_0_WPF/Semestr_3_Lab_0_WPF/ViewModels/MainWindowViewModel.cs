using CommunityToolkit.Mvvm.ComponentModel;
using Semestr_3_Lab_0_WPF.Views;
using System.Windows.Input;

namespace Semestr_3_Lab_0_WPF.ViewModels
{
    class MainWindowViewModel : ObservableObject
    {
        public ICommand OpenSecondControlCommand { get; set; }
        public ICommand OpenFirstControlCommand { get; set; }

        private object? _currentView;
        private SecondControl _secondControl;
        private FirstControl _firstControl;

        public object CurrentView
        {
            get => _currentView;
            set
            {
                SetProperty(ref _currentView, value);
                (OpenFirstControlCommand as MyDelegateCommand)?.InvokeCanExecuteChanged();
                (OpenSecondControlCommand as MyDelegateCommand)?.InvokeCanExecuteChanged();
            }
        }

        public MainWindowViewModel()
        {
            _firstControl = new FirstControl();
            _secondControl = new SecondControl();

            CurrentView = _firstControl;

            OpenSecondControlCommand = new MyDelegateCommand(OpenSecondControl, CanOpenSecondControl);
            OpenFirstControlCommand = new MyDelegateCommand(OpenFirstControl, CanOpenFirstControl);

        }

        private void OpenSecondControl(object parameter)
        {
            CurrentView = _secondControl;
        }

        private bool CanOpenSecondControl(object parameter)
        {
            return CurrentView != _secondControl;

        }
        private void OpenFirstControl(object parameter)
        {
            CurrentView = _firstControl;
        }

        private bool CanOpenFirstControl(object parameter)
        {
            return CurrentView != _firstControl;
        }
    }
}
