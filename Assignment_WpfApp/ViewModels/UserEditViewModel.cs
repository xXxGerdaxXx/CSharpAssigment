using Business_Library.Interfaces;
using Business_Library.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace Assignment_WpfApp.ViewModels;

public partial class UserEditViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IUserService _userService;
    private readonly IValidationService _validationService;


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

    public UserEditViewModel(IServiceProvider serviceProvider, IUserService userService, IValidationService validationService)
    {
        _serviceProvider = serviceProvider;
        _userService = userService;
        _validationService = validationService;
        FieldErrors = new Dictionary<string, string>
        {
            { "Name", "" },
            { "Surname", "" },
            { "Email", "" },
            { "Mobile", "" }
        };
    }

    [RelayCommand]
    private void Save()
    {
        
        var errors = _validationService.ValidateUser(User);

        FieldErrors.Clear();

        if (errors.Any())
        {
            foreach (var error in errors)
            {
                FieldErrors[error.Key] = error.Value;
            }

            OnPropertyChanged(nameof(FieldErrors));
            return;
        }


        FieldErrors.Clear();
        OnPropertyChanged(nameof(FieldErrors));

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
