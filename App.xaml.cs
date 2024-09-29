using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using TextEditor.Interfaces;
using TextEditor.MVVM.Navigation;
using TextEditor.MVVM.ViewModels;
using TextEditor.MVVM.Views;
using TextEditor.Services;

namespace TextEditor
{
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;

        public App()
        {
            IServiceCollection services = new ServiceCollection();
            ConfigureServices(services);

            _serviceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
            base.OnStartup(e);
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<MainWindow>(_serviceProvider => new MainWindow
            {
                DataContext = _serviceProvider.GetRequiredService<MainViewModel>()
            });
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<HomeViewModel>();
            services.AddSingleton<HelpViewModel>();
            services.AddSingleton<AboutViewModel>();
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<Func<Type, ViewModelBase>>(_serviceProvider => viewModelType =>
            {
                return (ViewModelBase)_serviceProvider.GetRequiredService(viewModelType);
            });
            services.AddSingleton<IContentService, ContentService>();
            services.AddSingleton<IFileService, FileService>();
        }
    }
}
