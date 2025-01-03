using Assignment_WpfApp.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;

namespace Assignment_WpfApp.ViewModels;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableObject _currentViewModel;

    public MainViewModel(IServiceProvider serviceProvider)
    {
        _currentViewModel = serviceProvider.GetRequiredService<UserListViewModel>();
    }
}
