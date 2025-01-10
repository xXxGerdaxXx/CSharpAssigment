using Business_Library.Interfaces;
using Business_Library.Models;
using Business_Library.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace Assignment_WpfApp.ViewModels;

public partial class UserEditViewModel(IServiceProvider serviceProvider, IUserService userService, IValidationService validationService) : ObservableObject
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private readonly IUserService _userService = userService;
    private readonly IValidationService _validationService = validationService;

    [ObservableProperty]
    private UserBase _user = new()
    {
        Name = string.Empty,
        Surname = string.Empty,
        Email = string.Empty,
        Mobile = string.Empty,
        Address = string.Empty,
        PostNumber = string.Empty,
        City = string.Empty
    };

    [ObservableProperty]
    private Dictionary<string, string> _fieldErrors = [];

    // Properties for individual field errors
    public string NameError => FieldErrors.TryGetValue("Name", out string? value) ? value : string.Empty;
    public string SurnameError => FieldErrors.TryGetValue("Surname", out string? value) ? value : string.Empty;
    public string EmailError => FieldErrors.TryGetValue("Email", out string? value) ? value : string.Empty;
    public string MobileError => FieldErrors.TryGetValue("Mobile", out string? value) ? value : string.Empty;

    private bool Validate()
    {
        var errors = _validationService.ValidateUser(User);

        FieldErrors.Clear();

        foreach (var error in errors)
        {
            FieldErrors[error.Key] = error.Value;
        }

        // Notify UI about changes to error properties
        OnPropertyChanged(nameof(NameError));
        OnPropertyChanged(nameof(SurnameError));
        OnPropertyChanged(nameof(EmailError));
        OnPropertyChanged(nameof(MobileError));

        return FieldErrors.Count == 0; // Return true if there are no errors
    }

    [RelayCommand]
    private void Save()
    {
        if (!Validate()) return; // Stop execution if there are validation errors

        var result = _userService.UpdateUser(User);
        if (result)
        {
            var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<UserListViewModel>();
        }
    }

    [RelayCommand]
    private void Cancel()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<UserListViewModel>();
    }
}
