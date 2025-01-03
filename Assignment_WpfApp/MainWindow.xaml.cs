using Assignment_WpfApp.ViewModels;
using System.Windows;


namespace Assignment_WpfApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow(MainViewModel viewmodel)
    {
        InitializeComponent();
        DataContext = viewmodel;
    }

    private void CloseApp_Click(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }

    private void TopBar_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        
        if (e.ChangedButton == System.Windows.Input.MouseButton.Left)
        {
            this.DragMove();
        }
    }
}