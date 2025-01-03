using System.Windows;
using System.Windows.Controls;
using Graphic_App.ViewModels;

namespace Graphic_App.Views
{
    namespace Graphic_App.Views
    {
        /// <summary>
        /// Interaction logic for UserDetailsView.xaml
        /// </summary>
        public partial class UserDetailsView : Page
        {
            public UserDetailsView()
            {
                InitializeComponent();
                DataContext = new UserDetailsViewModel();
            }

            private void InitializeComponent()
            {
                throw new NotImplementedException();
            }
        }
    }
}