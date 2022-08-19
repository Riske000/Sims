using Sims.CompositeComon;
using Sims.CompositeComon.Enums;
using Sims.Model;
using Sims.Persistance;
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
    public class UsersViewModel : BaseDialogViewModel
    {
        private UserRepository repository = new UserRepository();
        private bool dataGridEnabled;
        private List<ComboData<User>> users = new List<ComboData<User>>();
        private UserType filterType;
        private RelayCommand filterCommand;

        public UsersViewModel(UsersView view) : base(view, typeof(Medicine))
        {
            Init();
            LoadUsers();
        }

        public UserRepository Repository { get => repository; set => repository = value; }
        public bool DataGridEnabled { get => dataGridEnabled; set => dataGridEnabled = value; }
        public List<ComboData<User>> Users { get => users; set => users = value; }
       
        public UserType FilterType
        {
            get { return filterType; }
            set { filterType = value;  OnPropertyChanged("FilterType"); }
        }

        public void LoadUsers()
        {
            foreach (User user in Repository.GetAll())
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


        protected override void Init()
        {
            Items = new ObservableCollection<Entity>(repository.GetAll());
        }

        protected void FilterCommandExecute()
        {
            repository.Filter(FilterType);
        }

        protected virtual bool CanFilterCommandExecute()
        {
            return FilterType != null;
        }
    }
}
