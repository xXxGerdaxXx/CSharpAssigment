using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Windows;
using Graphic_App.Views;
using Graphic_App.ViewModels;
using Business_Library.Services;
using Business_Library.Interfaces;
using Business_Library.Repositories;

namespace Graphic_App
{
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    ConfigureServices(services);
                })
                .Build();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            // Register IFileService and UserRepository
            services.AddSingleton<IFileService>(new FileService(AppDomain.CurrentDomain.BaseDirectory, "users.json"));
            services.AddSingleton<IUserRepository, UserRepository>();

            // Register IUserService implementation
            services.AddTransient<IUserService, UserService>();

            // Register ViewModels
            services.AddTransient<UserListViewModel>();
            services.AddTransient<UserAddViewModel>();
            services.AddTransient<UserDetailsViewModel>();
            services.AddTransient<UserEditViewModel>();

            // Register Views
            services.AddTransient<UserListView>();
            services.AddTransient<UserAddView>();
            services.AddTransient<UserDetailsView>();
            services.AddTransient<UserEditView>();

            // Register MainViewModel and MainWindow
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<MainWindow>();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            try
            {
                await _host.StartAsync();

                var mainViewModel = _host.Services.GetRequiredService<MainViewModel>();
                mainViewModel.CurrentViewModel = _host.Services.GetRequiredService<UserListViewModel>();

                var mainWindow = _host.Services.GetRequiredService<MainWindow>();
                mainWindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Application failed to start: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Shutdown();
            }
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await _host.StopAsync();
            _host.Dispose();
            base.OnExit(e);
        }
    }
}
