using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace Graphic_App.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly IServiceProvider _serviceProvider;

        [ObservableProperty]
        private ObservableObject? _currentViewModel;

        // Constructor to inject the service provider
        public MainViewModel(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            // Initialize with a default view
            CurrentViewModel = _serviceProvider.GetRequiredService<UserListViewModel>();
        }

        // Command to navigate to UserListViewModel
        [RelayCommand]
        private void ShowUserList()
        {
            CurrentViewModel = _serviceProvider.GetRequiredService<UserListViewModel>();
        }

        // Command to navigate to UserAddViewModel
        [RelayCommand]
        private void ShowUserAdd()
        {
            CurrentViewModel = _serviceProvider.GetRequiredService<UserAddViewModel>();
        }
    }
}
