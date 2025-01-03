using System.Windows;
using System.Windows.Navigation;
using Graphic_App.ViewModels;
using Graphic_App.Views;

namespace Graphic_App
{
    public partial class MainWindow : Window
    {
        public MainWindow(MainViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

    }
}
