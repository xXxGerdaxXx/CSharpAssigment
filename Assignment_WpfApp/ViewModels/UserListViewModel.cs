using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Business_Library.Interfaces;
using System.Collections.ObjectModel;
using Business_Library.Models;
using Business_Library.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Assignment_WpfApp.ViewModels;

public partial class UserListViewModel : ObservableObject
{
    private readonly IUserService _userService;
    private readonly IServiceProvider _serviceProvider;

    [ObservableProperty]
    private ObservableCollection<UserBase> _users = [];

    public UserListViewModel(IUserService userService, IServiceProvider serviceProvider)
    {
        _userService = userService;
        _serviceProvider = serviceProvider;

        _users = new ObservableCollection<UserBase>(_userService.GetAllUsers());
    }

    [RelayCommand]

    private void GoToAddView()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<UserAddViewModel>();
    }
    [RelayCommand]

    private void GoToDetailsView(UserBase user)
    {
        var userDetailsViewModel = _serviceProvider.GetRequiredService<UserDetailsViewModel>();
        userDetailsViewModel.User = user;

        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = userDetailsViewModel;
    }



    public void RefreshUsers()
    {
        Users = new ObservableCollection<UserBase>(_userService.GetAllUsers());
    }

    [RelayCommand]
    private void DeleteItem(UserBase user)
    {
        if (user == null)
        {
            Console.WriteLine("No user selected for deletion.");
            return;
        }

        try
        {
            _userService.RemoveUser(user.Id); // Call to IUserService to delete user
            Console.WriteLine($"User with ID {user.Id} successfully removed.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error while deleting user: {ex.Message}");
        }

        RefreshUsers(); // Refresh the list after deletion
    }


}


