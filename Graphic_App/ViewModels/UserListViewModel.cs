using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Business_Library.Models;
using Business_Library.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Graphic_App.ViewModels
{
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
            var mainViewModel = _serviceProvider.GetService<MainViewModel>();
            mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<UserAddViewModel>();
        }

        [RelayCommand]
        private void GoToDetailsView(UserBase selectedUser)
        {
            try
            {
                var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
                var userDetailsViewModel = _serviceProvider.GetRequiredService<UserDetailsViewModel>();

                userDetailsViewModel.User = selectedUser;

                mainViewModel.CurrentViewModel = userDetailsViewModel;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error navigating to UserDetailsViewModel: {ex.Message}");
            }
        }
    }
}
