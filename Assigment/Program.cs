using Assigment.Dialogs;
using Business_Library.Interfaces;
using Business_Library.Services;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = new ServiceCollection()
         .AddSingleton<IFileService>(new FileService("Data", "applicationUserList.json"))
         .AddSingleton<IUserService, UserService>()
         .AddTransient<MenuDialog>()
         .BuildServiceProvider();


var menuDialog = serviceProvider.GetRequiredService<MenuDialog>();
menuDialog.MainMenu();