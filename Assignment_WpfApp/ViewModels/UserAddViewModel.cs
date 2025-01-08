using System;
using Business_Library.Interfaces;
using Business_Library.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Windows;

namespace Assignment_WpfApp.ViewModels;

public partial class UserAddViewModel : ObservableObject
{
    private readonly IUserService _userService;
    private readonly IServiceProvider _serviceProvider;
    private readonly IValidationService _validationService;

    public UserAddViewModel(IUserService userService, IServiceProvider serviceProvider, IValidationService validationService)
    {
        _userService = userService;
        _serviceProvider = serviceProvider;
        _validationService = validationService;
        FieldErrors = new Dictionary<string, string>();
    }

    [ObservableProperty]
    private UserBase _user = new();

    [ObservableProperty]
    private Dictionary<string, string> _fieldErrors = new();

    public string GetFieldError(string key)
    {
        return FieldErrors.ContainsKey(key) ? FieldErrors[key] : string.Empty;
    }

    [RelayCommand]
    private void Cancel()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<UserListViewModel>();
    }

    [RelayCommand]
    private void Save()
    {
        FieldErrors.Clear();

        // Validate the user object
        var errors = _validationService.ValidateUser(User);

        if (errors.Count > 0)
        {
            // Populate FieldErrors with validation results
            foreach (var error in errors)
            {
                FieldErrors[error.Key] = error.Value;
            }

            // Notify bindings of FieldErrors update
            OnPropertyChanged(nameof(FieldErrors));
            return;
        }

        // Attempt to save the user
        var result = _userService.AddUser(User);
        if (result)
        {
            // Refresh the user list and navigate back
            var userListViewModel = _serviceProvider.GetRequiredService<UserListViewModel>();
            userListViewModel.RefreshUsers();

            var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            mainViewModel.CurrentViewModel = userListViewModel;
        }
        else
        {
            MessageBox.Show("Failed to save the user. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

}
