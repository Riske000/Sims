using Sims.CompositeComon;
using Sims.CompositeComon.Enums;
using Sims.Model;
using Sims.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Sims.UI.Dialogs.ViewModel
{
    public abstract class BaseDialogController : ViewModelBase
    {
        #region Fields

        private Type type;
        private string search = string.Empty;

        private RelayCommand addCommand;
        private RelayCommand findCommand;
        private RelayCommand deleteCommand;
        private RelayCommand editCommand;
        private RelayCommand okCommand;
        private RelayCommand cancelCommand;
        private RelayCommand exitCommand;
        private DialogState dialogState;
        protected Entity selectedItem;
        protected Entity oldItem;
        protected Window dialog;

        private UserService userService = new UserService();

        private ObservableCollection<Entity> items;

        #endregion Fields

        #region Constructors

        public BaseDialogController(Window dialog, Type type)
        {
            this.dialog = dialog;
            this.type = type;
            dialogState = DialogState.View;
            items = new ObservableCollection<Entity>();
        }

        #endregion Constructors

        #region Command Properties

        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ?? (addCommand = new RelayCommand(param => this.AddCommandExecute(), param => this.CanAddCommandExecute()));
            }
        }

        public RelayCommand FindCommand
        {
            get
            {
                return findCommand ?? (findCommand = new RelayCommand(param => this.FindCommandExecute(), param => this.CanFindCommandExecute()));
            }
        }

        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ?? (deleteCommand = new RelayCommand(param => this.DeleteCommandExecute(), param => this.CanDeleteCommandExecute()));
            }
        }

        public RelayCommand EditCommand
        {
            get
            {
                return editCommand ?? (editCommand = new RelayCommand(param => this.EditCommandExecute(), param => this.CanEditCommandExecute()));
            }
        }

        public RelayCommand OkCommand
        {
            get
            {
                return okCommand ?? (okCommand = new RelayCommand(param => this.OkCommandExecute(), param => this.CanOkCommandExecute()));
            }
        }

        public RelayCommand CancelCommand
        {
            get
            {
                return cancelCommand ?? (cancelCommand = new RelayCommand(param => this.CancelCommandExecute(), param => this.CanCancelCommandExecute()));
            }
        }

        #endregion CommandProperties

        #region Properties


        public string Search
        {
            get { return search; }
            set
            {
                bool doSearch = true;

                if (search.Length == 0 && value.Length == 0)
                {
                    doSearch = false;
                }

                search = value;
                OnPropertyChanged(nameof(Search));

                if (!doSearch)
                {
                    return;
                }

                DoSearch();
            }
        }

        public ObservableCollection<Entity> Items
        {
            get { return items; }
            set
            {
                items = value;
                OnPropertyChanged(nameof(Items));
            }
        }

        public virtual Entity SelectedItem
        {
            get { return selectedItem; }
            set
            {
                if (selectedItem != value)
                {
                    selectedItem = value;
                    OnPropertyChanged(nameof(SelectedItem));
                }
            }
        }

        public virtual Entity OldItem
        {
            get { return oldItem; }
            set
            {
                if (oldItem != value)
                {
                    oldItem = value;
                    OnPropertyChanged(nameof(OldItem));
                }
            }
        }

        public DialogState DialogState
        {
            get { return dialogState; }
            set
            {
                if (dialogState == value)
                {
                    return;
                }

                dialogState = value;

                OnPropertyChanged(nameof(DialogState));
            }
        }

        #endregion Properties

        public Entity GetInstance()
        {
            return (Entity)Activator.CreateInstance(this.type);
        }

        #region Command Methods

        protected virtual void AddCommandExecute()
        {
            SelectedItem = GetInstance();

            DialogState = DialogState.Add;
        }

        protected virtual bool CanAddCommandExecute()
        {
            return dialogState == DialogState.View;
        }

        protected virtual void DeleteCommandExecute()
        {
            MessageBoxResult result = MessageBox.Show("Do you want to delete this item?", "Delete confirm!", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.No)
            {
                return;
            }

            if (DatabaseRemove(SelectedItem))
            {
                Items.Remove(SelectedItem);
            }
        }

        protected virtual bool DatabaseRemove(Entity item)
        {
            return true;
        }

        protected virtual bool CanDeleteCommandExecute()
        {
            return SelectedItem != null && dialogState == DialogState.View;
        }

        protected virtual void EditCommandExecute()
        {
            oldItem = SelectedItem.ExportObject<Entity>();
            SelectedItem.Validation = true;
            DialogState = DialogState.Edit;
        }

        protected virtual bool CanEditCommandExecute()
        {
            return SelectedItem != null && dialogState == DialogState.View;
        }

        protected virtual void FindCommandExecute()
        {
            DialogState = DialogState.Find;
        }

        protected virtual bool CanFindCommandExecute()
        {
            return SelectedItem != null && dialogState == DialogState.View;
        }

        protected virtual void OkCommandExecute()
        {
            switch (DialogState)
            {
                case DialogState.Add:
                    DialogState = DialogState.View;
                    OkAfterAdd();
                    break;
                case DialogState.Edit:
                    DialogState = DialogState.View;
                    OkAfterEdit();
                    break;
            }



            if (SelectedItem != null)
            {
                return;
            }

            SelectedItem.Validation = false;
            SelectedItem = null;
        }

        protected virtual bool CanOkCommandExecute()
        {
            return dialogState != DialogState.View && !SelectedItem.HasErrors();
        }

        protected virtual void CancelCommandExecute()
        {
            if (DialogState == DialogState.Edit)
            {
                SelectedItem.ImportObject(OldItem);
            }

            DialogState = DialogState.View;
            SelectedItem.Validation = false;
            Init();
        }

        protected virtual bool CanCancelCommandExecute()
        {

            return DialogState != DialogState.View;
        }

        #endregion

        protected virtual void DoSearch()
        {

        }

        protected virtual bool OkAfterAdd()
        {
            if (SelectedItem.HasErrors())
            {
                return false;
            }

            if (OkAfterAddDatabase() == null)
            {
                return false;
            }

            if (SelectedItem is User)
            {
                if (!userService.CheckEmail(((User)SelectedItem).Email))
                {
                    MessageBox.Show("User with this Email already exists!");
                    return false;
                }
                if (!userService.CheckJmbg(((User)SelectedItem).Jmbg))
                {
                    MessageBox.Show("User with this JMBG already exists!");
                    return false;
                }
                SelectedItem.ID = ApplicationContext.Instance.GenerateIDForUser();
                ApplicationContext.Instance.Users.Add(SelectedItem);
            }
            else if (SelectedItem is Medicine)
            {
                SelectedItem.ID = ApplicationContext.Instance.GenerateIDForMedicine();
                ApplicationContext.Instance.Medicines.Add(SelectedItem);
            }

            Items.Add(SelectedItem);
            ApplicationContext.Instance.Save();
            return true;
        }

        protected virtual bool OkAfterEdit()
        {
            if (SelectedItem.HasErrors())
            {
                return false;
            }

            if (OkAfterEditDatabase() == null)
            {
                return false;
            }
            if(SelectedItem is Medicine)
            {
                ((Medicine)SelectedItem).ReasonByDoctor = "";
                ((Medicine)SelectedItem).ReasonByFarmacist = "";
                ((Medicine)SelectedItem).CounterForFarmacist = 0;
                ((Medicine)SelectedItem).CounterForDoctor = 0;
                OnPropertyChanged("Medicines");
            }
            OnPropertyChanged("Users");
            ApplicationContext.Instance.Save();
            return true;
        }

        protected virtual Entity OkAfterAddDatabase()
        {
            SelectedItem.Validate();
            if (SelectedItem.IsValid)
            {
                return selectedItem;
            }
            DialogState = DialogState.Add;
            return null;
        }

        protected virtual Entity OkAfterEditDatabase()
        {
            SelectedItem.Validate();
            if (SelectedItem.IsValid)
            {
                return selectedItem;
            }
            DialogState = DialogState.Edit;
            return null;
        }

        #region Abstract Methods

        protected abstract void Init();

        #endregion Abstract Methods
    }
}
