using System.Windows.Controls;
using Graphic_App.ViewModels;

namespace Graphic_App.Views
{
    /// <summary>
    /// Interaction logic for UserListView.xaml
    /// </summary>
    public partial class UserListView : UserControl
    {
        public UserListView()
        {
        }

        public UserListView(UserListViewModel userListViewModel)
        {
            InitializeComponent();
            DataContext = userListViewModel;
        }
    }
}
