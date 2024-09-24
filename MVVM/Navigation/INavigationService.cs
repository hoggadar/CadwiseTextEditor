using TextEditor.MVVM.ViewModels;

namespace TextEditor.MVVM.Navigation
{
    public interface INavigationService
    {
        ViewModelBase CurrentView { get; }
        void NavigateTo<TViewModel>() where TViewModel : ViewModelBase;
    }
}
