using Business_Library.Models;
using Business_Library.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace Graphic_App.ViewModels
{
    public partial class UserEditViewModel(IServiceProvider serviceProvider, IUserService userService) : ObservableObject
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider;
        private readonly IUserService _userService = userService;

        [ObservableProperty]
        private UserBase _user = new UserBase
        {
            Name = string.Empty,
            Surname = string.Empty,
            Email = string.Empty,
            Mobile = string.Empty,
            Address = string.Empty,
            PostNumber = string.Empty,
            City = string.Empty
        };

        // RelayCommand to save the user
        [RelayCommand]
        private void Save()
        {
            var result = _userService.UpdateUser(User);
            if (result)
            {
                var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
                mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<UserListViewModel>();
            }
        }

        // RelayCommand to cancel and navigate back to the UserListViewModel
        [RelayCommand]
        private void Cancel()
        {
            var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<UserListViewModel>();
        }
    }
}
