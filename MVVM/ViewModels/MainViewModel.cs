using System.Windows;
using System.Windows.Input;
using TextEditor.MVVM.Commands;
using TextEditor.MVVM.Navigation;

namespace TextEditor.MVVM.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private INavigationService _navigationService;

        public ICommand NavigateToHomeCommand { get; }
        public ICommand NavigateToHelpCommand { get; }
        public ICommand NavigateToAboutCommand { get; }

        public MainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            NavigateToHomeCommand = new RelayCommand(_ => NavigationService.NavigateTo<HomeViewModel>(), _ => true);
            NavigateToHelpCommand = new RelayCommand(_ => NavigationService.NavigateTo<HelpViewModel>(), _ => true);
            NavigateToAboutCommand = new RelayCommand(_ => NavigationService.NavigateTo<AboutViewModel>(), _ => true);
        }

        public INavigationService NavigationService
        {
            get => _navigationService;
            set
            {
                _navigationService = value;
                OnPropertyChanged(nameof(NavigationService));
            }
        }
    }
}
