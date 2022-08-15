using Sims.CompositeComon;
using Sims.Model;
using Sims.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Sims.UI.Components.Login.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        private string email;
        private string password;
        private RelayCommand loginCommand;
        private RelayCommand cancelCommand;
        private Window dialog;
        private PasswordBox passwordBox;
        private MainWindowViewModel mainViewModel;
        private UserRepository userRepository = new UserRepository();


        public LoginViewModel(Window dialog, PasswordBox passwordBox, MainWindowViewModel mainViewModel)
        {
            this.dialog = dialog;
            this.PasswordBox = passwordBox;
            this.mainViewModel = mainViewModel;
        }

        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged(nameof(email));
            }
        }

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged(nameof(password));
            }
        }

        #region Command Properties

        public RelayCommand LoginCommand
        {
            get
            {
                return loginCommand ?? (loginCommand = new RelayCommand(param => LoginCommandExecute(), param => CanLoginCommandExecute()));
            }
        }

        public RelayCommand CancelCommand
        {
            get
            {
                return cancelCommand ?? (cancelCommand = new RelayCommand(param => CancelCommandExecute(), param => CanCancelCommandExecute()));
            }
        }

        public PasswordBox PasswordBox { get => passwordBox; set => passwordBox = value; }


        #endregion

        private void LoginCommandExecute()
        {
            User user = userRepository.getUserWithUsernameAndPassword(email, password);

            if (user != null)
            {

                ApplicationContext.Instance.User = user;
                dialog.Close();
            }
            else
            {
                MessageBox.Show("Wrong username or password");
            }
        }

        private bool CanLoginCommandExecute()
        {
            return !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password);
        }

        private void CancelCommandExecute()
        {
            System.Windows.Application.Current.Shutdown();
        }

        private bool CanCancelCommandExecute()
        {
            return true;
        }
    }
}
