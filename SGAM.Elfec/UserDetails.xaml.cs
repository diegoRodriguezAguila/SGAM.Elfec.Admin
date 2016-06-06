using SGAM.Elfec.Model;
using SGAM.Elfec.Presenters;
using SGAM.Elfec.Presenters.Views;
using System.Windows.Controls;

namespace SGAM.Elfec
{
    /// <summary>
    /// Interaction logic for UserDetails.xaml
    /// </summary>
    public partial class UserDetails : UserControl, IUserDetailsView
    {
        public UserDetails(User user)
        {
            InitializeComponent();
            DataContext = new UserDetailsPresenter(this, user);
        }
    }
}
