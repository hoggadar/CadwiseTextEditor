using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using Microsoft.Win32;
using TextEditor.MVVM.Commands;
using TextEditor.MVVM.Views;

namespace TextEditor.MVVM.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private object _currentView;
        
        public ICommand NavigateToHome { get; }
        public ICommand NavigateToAbout { get; }
        

        public MainViewModel()
        {
            _currentView = new HomePage();
            OnPropertyChanged(nameof(CurrentView));
            NavigateToHome = new RelayCommand(_ => CurrentView = new HomePage(), _ => true);
            NavigateToAbout = new RelayCommand(_ => CurrentView = new AboutPage(), _ => true);
        }

        public object CurrentView
        {
            get => _currentView;
            set
            {
                if (_currentView == value) return;
                _currentView = value;
                OnPropertyChanged();
            }
        }
    }
}
