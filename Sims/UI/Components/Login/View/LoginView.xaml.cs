using Sims.Model;
using Sims.UI.Components.Login.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Sims.UI.Components.Login.View
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();

            DataContext = new LoginController(this, passwordBox, null);

            ApplicationContext.Instance.SaveMedicines();
            ApplicationContext.Instance.RemoveScheudledAddition();
        }

        private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ((LoginController)DataContext).Password = passwordBox.Password;
        }
    }
}
