using Assignment_WpfApp.ViewModels;
using Assignment_WpfApp.Views;
using Business_Library.Interfaces;
using Business_Library.Repositories;
using Business_Library.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Windows;

namespace Assignment_WpfApp;

public partial class App : Application
{
    private readonly IHost _host; 

    public App()
    {
        _host = Host.CreateDefaultBuilder() 
            .ConfigureServices(services =>
            {

                services.AddSingleton<IFileService>(new FileService(AppDomain.CurrentDomain.BaseDirectory, "applicationUserList.json"));
                
                services.AddSingleton<IUserRepository, UserRepository>();
                services.AddTransient<IUserService, UserService>();
                services.AddTransient<IValidationService, Business_Library.Services.ValidationService>();

                services.AddTransient<UserListViewModel>();
                services.AddTransient<UserAddViewModel>();
                services.AddTransient<UserDetailsViewModel>();
                services.AddTransient<UserEditViewModel>();

                services.AddTransient<UserListView>();
                services.AddTransient<UserAddView>();
                services.AddTransient<UserDetailsView>();
                services.AddTransient<UserEditView>();

                services.AddSingleton<MainViewModel>();
                services.AddSingleton<MainWindow>();
            })
            .Build();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        var mainViewModel = _host.Services.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _host.Services.GetRequiredService<UserListViewModel>();

        var mainWindow = _host.Services.GetRequiredService<MainWindow>();
        mainWindow.DataContext = mainViewModel; 
        mainWindow.Show();
    }

}
