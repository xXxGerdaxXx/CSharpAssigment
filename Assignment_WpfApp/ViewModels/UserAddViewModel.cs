using System;
using Business_Library.Interfaces;
using Business_Library.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace Assignment_WpfApp.ViewModels;

public partial class UserAddViewModel(IUserService userService, IServiceProvider serviceProvider) : ObservableObject
{
    // Dependencies injected via constructor
    private readonly IUserService _userService = userService;
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    // Property bound to the user input
    [ObservableProperty]
    private UserBase user = new();

    // Command to cancel and navigate back to the user list
    [RelayCommand]
    private void Cancel()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<UserListViewModel>();
    }

    // Command to save the user
[RelayCommand]
private void Save()
{
    if (string.IsNullOrWhiteSpace(User.Name) || string.IsNullOrWhiteSpace(User.Email))
    {
        MessageBox.Show("Please fill out all required fields.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
        return;
    }

    var result = _userService.AddUser(User); // Save the user
    if (result)
    {
        // Refresh UserListViewModel
        var userListViewModel = _serviceProvider.GetRequiredService<UserListViewModel>();
        userListViewModel.RefreshUsers();

        // Navigate back to UserListView
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = userListViewModel;
    }
    else
    {
        MessageBox.Show("Failed to save the user. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
    }
}


}
