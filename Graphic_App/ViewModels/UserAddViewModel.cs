using System;
using Business_Library.Interfaces;
using Business_Library.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace Graphic_App.ViewModels;

public partial class UserAddViewModel(IUserService userService, IServiceProvider serviceProvider) : ObservableObject
{
    private readonly IUserService _userService = userService;
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    [ObservableProperty]
    private UserBase user = new();

    [RelayCommand]
    private void Save()
    {
        if (user != null)
        {
            _userService.AddUser(user);          
            NavigateToUserList();

        }
    }

    [RelayCommand]
    private void Cancel()
    {
        NavigateToUserList();
    }

    [RelayCommand]
    private void RemoveUser()
    {
        try
        {
            if (user == null || string.IsNullOrEmpty(user.Id))
            {
                ShowMessage("No user selected for removal.");
                return;
            }

            _userService.RemoveUser(user.Id);
            ShowMessage($"User with ID {user.Id} has been removed successfully.");

            // Navigate back to the UserListViewModel after removal
            NavigateToUserList();
        }
        catch (Exception ex)
        {
            ShowMessage($"Error removing user: {ex.Message}");
        }
    }

    // Private method for navigation to UserListViewModel
    private void NavigateToUserList()
    {
        try
        {
            var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<UserListViewModel>();
        }
        catch (Exception ex)
        {
            ShowMessage($"Error navigating to UserListViewModel: {ex.Message}");
        }
    }

    // Private method for displaying messages
    private void ShowMessage(string message)
    {
        MessageBox.Show(message, "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
    }
}
