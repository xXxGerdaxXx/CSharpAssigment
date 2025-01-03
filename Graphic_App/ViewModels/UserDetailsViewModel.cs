using Business_Library.Interfaces;
using Business_Library.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;

public class UserDetailsViewModel : INotifyPropertyChanged
{
    private readonly IUserService _userService;
    private UserBase _user;

    public UserBase User
    {
        get => _user;
        set
        {
            _user = value;
            OnPropertyChanged(nameof(User));
        }
    }

    // Constructor with Dependency Injection
    public UserDetailsViewModel(IUserService userService, UserBase user)
    {
        _userService = userService;
        _user = user;
    }

    // Parameterless Constructor
    public UserDetailsViewModel()
    {
        _user = new UserBase
        {
            Id = Guid.NewGuid().ToString(),
            Name = string.Empty,
            Surname = string.Empty,
            Email = string.Empty,
            Mobile = string.Empty,
            Address = string.Empty,
            PostNumber = string.Empty,
            City = string.Empty
        };
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public static implicit operator ObservableObject(UserDetailsViewModel v)
    {
        throw new NotImplementedException();
    }
}
