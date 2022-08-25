using Sims.CompositeComon;
using Sims.CompositeComon.Enums;
using Sims.Model;
using Sims.Persistance;
using Sims.Service;
using Sims.UI.Dialogs.Model;
using Sims.UI.Dialogs.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims.UI.Dialogs.ViewModel
{
    public class UsersController : BaseDialogController
    {
        private UserService service = new UserService();
        private bool dataGridEnabled;
        private List<ComboData<User>> users = new List<ComboData<User>>();
        private string filterType;
        private string userSortType;
        private string userSortBy;
        private RelayCommand filterCommand;
        private RelayCommand refreshCommand;
        private RelayCommand blockCommand;
        private RelayCommand unblockCommand;

        public UsersController(UsersView view) : base(view, typeof(User))
        {
            Init();
            LoadUsers();
            UserSortBy = "First name";
            UserSortType = "Ascending";
            FilterType = "";
        }

        public bool DataGridEnabled { get => dataGridEnabled; set => dataGridEnabled = value; }
        public List<ComboData<User>> Users { get => users; set => users = value; }
       
        public string FilterType
        {
            get { return filterType; }
            set { filterType = value;  OnPropertyChanged("FilterType"); }
        }

        public string UserSortType
        {
            get { return userSortType; }
            set { userSortType = value; OnPropertyChanged("UserSortType"); }
        }

        public string UserSortBy
        {
            get { return userSortBy; }
            set { userSortBy = value; OnPropertyChanged("UserSortBy"); }
        }

        public void LoadUsers()
        {
            foreach (User user in service.GetAll())
            {
                Users.Add(new ComboData<User> { Name = user.FirstName + " " + user.LastName , Value = user });
            }
        }

        public RelayCommand FilterCommand
        {
            get
            {
                return filterCommand ?? (filterCommand = new RelayCommand(param => this.FilterCommandExecute(), param => this.CanFilterCommandExecute()));
            }
        }

        public RelayCommand RefreshCommand
        {
            get
            {
                return refreshCommand ?? (refreshCommand = new RelayCommand(param => this.RefreshCommandExecute(), param => this.CanRefreshCommandExecute()));
            }
        }

        public RelayCommand BlockCommand
        {
            get
            {
                return blockCommand ?? (blockCommand = new RelayCommand(param => this.BlockCommandExecute(), param => this.CanBlockCommandExecute()));
            }
        }

        public RelayCommand UnblockCommand
        {
            get
            {
                return unblockCommand ?? (unblockCommand = new RelayCommand(param => this.UnblockCommandExecute(), param => this.CanUnblockCommandExecute()));
            }
        }

        protected override void Init()
        {
            Items = new ObservableCollection<Entity>(service.GetAll());
        }

        protected void FilterCommandExecute()
        {
            Items = new ObservableCollection<Entity>(service.FilterAndSortUsers(FilterType, UserSortType, UserSortBy));
            OnPropertyChanged("Users");
        }

        protected virtual bool CanFilterCommandExecute()
        {
            return !FilterType.Equals("");
        }

        protected void RefreshCommandExecute()
        {
            filterType = "";
            Items = new ObservableCollection<Entity>(service.GetAll());
            OnPropertyChanged("Users");
        }

        protected virtual bool CanRefreshCommandExecute()
        {
            return true;
        }

        protected void BlockCommandExecute()
        {
            ((User)SelectedItem).Blocked = true;
            OnPropertyChanged("Users");
            ApplicationContext.Instance.Save();
        }

        protected virtual bool CanBlockCommandExecute()
        {
            if(SelectedItem == null)
            {
                return false;
            }
            return ((User)SelectedItem).UserType != UserType.Manager && ((User)SelectedItem).Blocked == false &&
                ((User)SelectedItem) != ApplicationContext.Instance.User && DialogState == DialogState.View;
        }

        protected void UnblockCommandExecute()
        {
            ((User)SelectedItem).Blocked = false;
            OnPropertyChanged("Users");
            ApplicationContext.Instance.Save();
        }

        protected virtual bool CanUnblockCommandExecute()
        {
            if (SelectedItem == null)
            {
                return false;
            }
            return ((User)SelectedItem).UserType != UserType.Manager && ((User)SelectedItem).Blocked == true &&
                ((User)SelectedItem) != ApplicationContext.Instance.User && DialogState == DialogState.View;
        }
    }
}
