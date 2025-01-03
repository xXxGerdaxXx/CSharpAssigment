using Business_Library.Interfaces;
using Business_Library.Models;
using Business_Library.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace Assignment_WpfApp.ViewModels;

public partial class UserDetailsViewModel(IServiceProvider serviceProvider, IUserService userService) : ObservableObject

{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private readonly IUserService _userService = userService;

    [ObservableProperty]
    private UserBase _user = new();


    [RelayCommand]

    private void GoToEditView()
    {
        var userEditViewModel = _serviceProvider.GetService<UserEditViewModel>();
        userEditViewModel.User = User;

        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = userEditViewModel;
    }
    [RelayCommand]

    private void Cancel()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<UserListViewModel>();
    }
}
