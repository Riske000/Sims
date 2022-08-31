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
using System.Windows;

namespace Sims.UI.Dialogs.ViewModel
{
    public class MedicinesController : BaseDialogController
    {
        private MedicineService service = new MedicineService();
        private bool dataGridEnabled;
        private List<ComboData<Medicine>> medicines = new List<ComboData<Medicine>>();
        private List<ComboData<Ingredient>> ingredients = new List<ComboData<Ingredient>>();
        private RelayCommand searchCommand;
        private string searchText;
        private ObservableCollection<IngredientTableModel> ingredientTableModels;
        private string sortType;
        private string category;
        private string quantity;
        private string price1;
        private string price2;
        private IngredientTableModel ingredientSelectedItem;
        private RelayCommand addIngredientToMedicineCommand;
        private RelayCommand removeIngredientFromMedicineCommand;
        private float amount;
        private ComboData<Ingredient> ingredientToAdd;
        private int amountToAdd;
        private Nullable<DateTime> dayOfAdding;
        private RelayCommand addAmountOfMedicineCommand;
        private RelayCommand acceptMedicineCommand;
        private RelayCommand declineMedicineCommand;
        private string reasonFarmacist;
        private string reasonDoctor;
        private RelayCommand refreshMedicinesCommand;
        int temp;
        private RelayCommand pendingMedicnesCommand;
        private RelayCommand declinedMedicinesCommand;
        private RelayCommand acceptedMedicinesCommand;
        double cena1;
        double cena2;
        private RelayCommand usersCommand;
        private string fullName;
        private string typeOfUser;
        private SelectedMedicines selectedMedicines;
        private RelayCommand refreshThisTabCommand;
        public MedicinesController(MedicinesView view) : base(view, typeof(Medicine))
        {
            Init();
            LoadMedicines();
            GetIngredients();
            SortType = "Ascending";
            Category = "Code";
            DayOfAdding = null;
            FullName = ApplicationContext.Instance.User.FirstName + " " + ApplicationContext.Instance.User.LastName;
            TypeOfUser = ApplicationContext.Instance.User.UserType.ToString();
        }

        public override Entity SelectedItem
        {
            get { return selectedItem; }
            set
            {
                if (selectedItem != value)
                {
                    selectedItem = value;
                    OnPropertyChanged(nameof(SelectedItem));
                    OnPropertyChanged(nameof(IngredientTableModels));
                }
            }
        }

        public IngredientTableModel IngredientSelectedItem
        {
            get { return ingredientSelectedItem; }
            set
            {
                if (ingredientSelectedItem != value)
                {
                    ingredientSelectedItem = value;
                    OnPropertyChanged(nameof(IngredientSelectedItem));
                }
            }
        }

        public Visibility PriceSelected
        {
            get { return category == "Price" ? Visibility.Visible : Visibility.Collapsed; }
        }

        public Visibility PriceNotSelected
        {
            get { return category != "Price" ? Visibility.Visible : Visibility.Collapsed; }
        }

        public Visibility ManagerLogged
        {
            get { return ApplicationContext.Instance.User.UserType == UserType.Manager ? Visibility.Visible : Visibility.Collapsed; }
        }

        public Visibility ManagerNotLogged
        {
            get { return ApplicationContext.Instance.User.UserType != UserType.Manager ? Visibility.Visible : Visibility.Collapsed; }
        }

        public Visibility DoctorLogged
        {
            get { return ApplicationContext.Instance.User.UserType == UserType.Doctor ? Visibility.Visible : Visibility.Collapsed; }
        }

        public Visibility DoctorNotLogged
        {
            get { return ApplicationContext.Instance.User.UserType != UserType.Doctor ? Visibility.Visible : Visibility.Collapsed; }
        }

        public Visibility PharmacistLogged
        {
            get { return ApplicationContext.Instance.User.UserType == UserType.Pharmacist ? Visibility.Visible : Visibility.Collapsed; }
        }
        public Visibility PharmacistNotLogged
        {
            get { return ApplicationContext.Instance.User.UserType != UserType.Pharmacist ? Visibility.Visible : Visibility.Collapsed; }
        }
        //public Visibility QuantitySelected
        //{
        //    get { return category == "Quantity" ? Visibility.Visible : Visibility.Collapsed; }
        //}
        public ObservableCollection<IngredientTableModel> IngredientTableModels
        {
            get
            {
                ingredientTableModels = new ObservableCollection<IngredientTableModel>();

                if (SelectedItem != null)
                {
                    foreach (KeyValuePair<double, Ingredient> pair in ((Medicine)SelectedItem).Ingredients)
                    {
                        IngredientTableModel model = new IngredientTableModel();
                        model.Quantity = pair.Key;
                        model.IngredientName = pair.Value.Name;

                        ingredientTableModels.Add(model);
                    }
                }


                return ingredientTableModels;
            }
        }

        public void LoadMedicines()
        {
            foreach (Medicine medicine in service.GetAll())
            {
                medicines.Add(new ComboData<Medicine> { Name = medicine.Name, Value = medicine });
            }
        }

        public RelayCommand SearchCommand
        {
            get
            {
                return searchCommand ?? (searchCommand = new RelayCommand(param => this.SearchCommandExecute(), param => this.CanSearchCommandExecute()));
            }
        }

        public RelayCommand AddIngredientToMedicineCommand
        {
            get
            {
                return addIngredientToMedicineCommand ?? (addIngredientToMedicineCommand = new RelayCommand(param => this.AddIngredientToMedicineCommandExecute(), param => this.CanAddIngredientToMedicineCommandExecute()));
            }
        }

        public RelayCommand RemoveIngredientFromMedicineCommand
        {
            get
            {
                return removeIngredientFromMedicineCommand ?? (removeIngredientFromMedicineCommand = new RelayCommand(param => this.RemoveIngredientFromMedicineCommandExecute(), param => this.CanRemoveIngredientFromMedicineCommandExecute()));

            }
        }
        public RelayCommand AddAmountOfMedicineCommand
        {
            get
            {
                return addAmountOfMedicineCommand ?? (addAmountOfMedicineCommand = new RelayCommand(param => this.AddAmountOfMedicineCommandExecute(), param => this.CanAddAmountOfMedicineCommandExecute()));
            }
        }

        public RelayCommand AcceptMedicineCommand
        {
            get
            {
                return acceptMedicineCommand ?? (acceptMedicineCommand = new RelayCommand(param => this.AcceptMedicineCommandExecute(), param => this.CanAcceptMedicineCommandExecute()));

            }
        }

        public RelayCommand DeclineMedicineCommand
        {
            get
            {
                return declineMedicineCommand ?? (declineMedicineCommand = new RelayCommand(param => this.DeclineMedicineCommandExecute(), param => this.CanDeclineMedicineCommandExecute()));
            }
        }

        public RelayCommand RefreshMedicinesCommand
        {
            get
            {
                return refreshMedicinesCommand ?? (refreshMedicinesCommand = new RelayCommand(param => this.RefreshMedicinesCommandExecute(), param => this.CanRefreshMedicinesCommandExecute()));
            }
        }

        public RelayCommand PendingMedicinesCommand
        {
            get
            {
                return pendingMedicnesCommand ?? (pendingMedicnesCommand = new RelayCommand(param => this.PendingMedicinesCommandExecute(), param => this.CanPendingMedicinesCommandExecute()));
            }
        }

        public RelayCommand AcceptedMedicinesCommand
        {
            get
            {
                return acceptedMedicinesCommand ?? (acceptedMedicinesCommand = new RelayCommand(param => this.AcceptedMedicinesCommandExecute(), param => this.CanAcceptedMedicinesCommandExecute()));
            }
        }

        public RelayCommand DeclinedMedicinesCommand
        {
            get
            {
                return declinedMedicinesCommand ?? (declinedMedicinesCommand = new RelayCommand(param => this.DeclinedMedicinesCommandExecute(), param => this.CanDeclinedMedicinesCommandExecute()));
            }
        }

        public RelayCommand UsersCommand
        {
            get
            {
                return usersCommand ?? (usersCommand = new RelayCommand(param => this.UsersCommandExecute(), param => this.CanUsersCommandExecute()));
            }
        }

        public RelayCommand RefreshThisTabCommand
        {
            get
            {
                return refreshThisTabCommand ?? (refreshThisTabCommand = new RelayCommand(param => this.RefreshThisTabCommandExecute(), param => this.CanRefreshThisTabCommandExecute()));
            }
        }

        public bool DataGridEnabled { get => dataGridEnabled; set => dataGridEnabled = value; }
        public List<ComboData<Medicine>> Medicines { get => medicines; set => medicines = value; }

        public List<ComboData<Ingredient>> Ingredients
        {
            get { return ingredients; }
            set { ingredients = value; OnPropertyChanged("Ingredients"); }
        }

        public List<ComboData<Ingredient>> GetIngredients()
        {
            foreach (Ingredient ingredient in ApplicationContext.Instance.Ingredients)
            {
                ingredients.Add(new ComboData<Ingredient> { Name = ingredient.Name, Value = ingredient });
            }
            return ingredients;
        }
        public string SearchText
        {
            get { return searchText; }
            set { searchText = value; OnPropertyChanged("SearchText"); }
        }
        public string Price1
        {
            get { return price1; }
            set { price1 = value; OnPropertyChanged("Price1"); }
        }
        public string Price2
        {
            get { return price2; }
            set { price2 = value; OnPropertyChanged("Price2"); }
        }
        public string SortType
        {
            get { return sortType; }
            set { sortType = value; OnPropertyChanged("SortType"); }
        }

        public string Quantity
        {
            get { return quantity; }
            set { quantity = value; OnPropertyChanged("Quantity"); }
        }

        public float Amount
        {
            get { return amount; }
            set { amount = value; OnPropertyChanged("Amount"); }
        }

        public ComboData<Ingredient> IngredientToAdd
        {
            get { return ingredientToAdd; }
            set { ingredientToAdd = value; OnPropertyChanged("IngredientToAdd"); }
        }
        public string Category
        {
            get { return category; }
            set { category = value; OnPropertyChanged("Category"); OnPropertyChanged("PriceSelected"); OnPropertyChanged("PriceNotSelected"); }
        }

        public int AmountToAdd
        {
            get { return amountToAdd; }
            set { amountToAdd = value; OnPropertyChanged("AmountToAdd"); }
        }

        public Nullable<DateTime> DayOfAdding
        {
            get { return dayOfAdding; }
            set { dayOfAdding = value; OnPropertyChanged("DayOfAdding"); }
        }

        public string ReasonFarmacist
        {
            get { return reasonFarmacist; }
            set { reasonFarmacist = value; OnPropertyChanged("ReasonFarmacist"); }
        }

        public string ReasonDoctor
        {
            get { return reasonDoctor; }
            set { reasonDoctor = value; OnPropertyChanged("ReasonDoctor"); }
        }

        public int Temp { get => temp; set => temp = value; }
        public string FullName { get => fullName; set => fullName = value; }

        public string TypeOfUser
        {
            get { return typeOfUser; }
            set { typeOfUser = value; }
        }

        public SelectedMedicines SelectedMedicines
        {
            get { return selectedMedicines; }
            set { selectedMedicines = value; }

        }
        protected override void Init()
        {
            Items = new ObservableCollection<Entity>(service.GetAll());
        }

        protected void SearchCommandExecute()
        {

            if (category == "Quantity")
            {
                if (Int32.TryParse(searchText, out temp))
                {
                    temp = Int32.Parse(searchText);
                }
            }
            else
            {
                temp = 0;
            }
            if (price1 == null || price1 == string.Empty)
            {
                cena1 = 0;
            }
            else
            {
                cena1 = Double.Parse(price1);
            }

            if (price2 == null || price2 == string.Empty)
            {
                cena2 = 1000000;
            }
            else
            {
                cena2 = Double.Parse(price2);
            }
            ObservableCollection<Entity> medicines = new ObservableCollection<Entity>();

            switch (SelectedMedicines)
            {
                case SelectedMedicines.All:
                    medicines = new ObservableCollection<Entity>(service.GetAll());
                    Items = new ObservableCollection<Entity>(service.Search(medicines, Category, SortType, SearchText, cena1, cena2, temp));
                    OnPropertyChanged("Medicines");
                    break;
                case SelectedMedicines.Pending:
                    medicines = new ObservableCollection<Entity>(service.getAllPendingMedicines());
                    Items = new ObservableCollection<Entity>(service.Search(medicines, Category, SortType, SearchText, cena1, cena2, temp));
                    OnPropertyChanged("Medicines");
                    break;
                case SelectedMedicines.Accepted:
                    medicines = new ObservableCollection<Entity>(service.getAllAcceptedMedicines());
                    Items = new ObservableCollection<Entity>(service.Search(medicines, Category, SortType, SearchText, cena1, cena2, temp));
                    OnPropertyChanged("Medicines");
                    break;
                case SelectedMedicines.Declined:
                    medicines = new ObservableCollection<Entity>(service.getAllDeclinedMedicines());
                    Items = new ObservableCollection<Entity>(service.Search(medicines, Category, SortType, SearchText, cena1, cena2, temp));
                    OnPropertyChanged("Medicines");
                    break;
            }


        }
        //Search(string category, string sortType, string term = "", double price1 = 0, double price2 = 100000000, int quantity = 0)
        protected virtual bool CanSearchCommandExecute()
        {
            return true;
        }

        protected void AddIngredientToMedicineCommandExecute()
        {
            ((Medicine)SelectedItem).Ingredients.Add(Amount, IngredientToAdd.Value);
            OnPropertyChanged("IngredientTableModels");
            ApplicationContext.Instance.SaveMedicines();
        }


        protected virtual bool CanAddIngredientToMedicineCommandExecute()
        {
            return Amount != 0 && IngredientToAdd != null && SelectedItem != null;
        }

        protected void RemoveIngredientFromMedicineCommandExecute()
        {
            ((Medicine)SelectedItem).Ingredients.Remove(IngredientSelectedItem.Quantity);
            OnPropertyChanged("IngredientTableModels");
            OnPropertyChanged("Medicine");
            ApplicationContext.Instance.SaveMedicines();
        }

        protected virtual bool CanRemoveIngredientFromMedicineCommandExecute()
        {
            return selectedItem != null && IngredientSelectedItem != null;
        }

        protected void AddAmountOfMedicineCommandExecute()
        {
            if (DayOfAdding == null)
            {
                ((Medicine)SelectedItem).Quantity += AmountToAdd;
                OnPropertyChanged("Medicines");
                ApplicationContext.Instance.SaveMedicines();
            }
            else
            {
                ScheudledAddition scheudledAddition = new ScheudledAddition();
                scheudledAddition.IdOfMedicine = ((Medicine)SelectedItem).ID;
                scheudledAddition.AmountToAdd = AmountToAdd;
                scheudledAddition.DateOfAddition = (DateTime)DayOfAdding;

                ApplicationContext.Instance.ScheudledAddiotons.Add(scheudledAddition);
                ApplicationContext.Instance.SaveScheudledAdditions();
            }
        }

        protected virtual bool CanAddAmountOfMedicineCommandExecute()
        {
            return SelectedItem != null && AmountToAdd != 0;
        }

        protected void AcceptMedicineCommandExecute()
        {
            if (ApplicationContext.Instance.User.UserType is UserType.Doctor)
            {
                ((Medicine)SelectedItem).CounterForDoctor++;
            }
            if (ApplicationContext.Instance.User.UserType is UserType.Pharmacist)
            {
                ((Medicine)SelectedItem).CounterForFarmacist++;
                Accept accept = new Accept();
                accept.Medicine = (Medicine)SelectedItem;
                accept.PharmacistWhoAccepted = ApplicationContext.Instance.User;
                ApplicationContext.Instance.Accepts.Add(accept);
                ApplicationContext.Instance.SaveAccepts();
            }
            if (((Medicine)SelectedItem).CounterForFarmacist == 2 && ((Medicine)SelectedItem).CounterForDoctor == 1)
            {
                ((Medicine)SelectedItem).Accepted = true;
            }
            ApplicationContext.Instance.SaveMedicines();
            OnPropertyChanged("Medicines");

        }

        protected virtual bool CanAcceptMedicineCommandExecute()
        {
            if (!CanDoctorAccept())
            {
                return false;
            }
            if (!CanPharmacistAccept())
            {
                return false;
            }
            return SelectedItem != null && ((Medicine)SelectedItem).Accepted != true && ((Medicine)SelectedItem).Declined != true &&
                 ApplicationContext.Instance.User.UserType != UserType.Manager;
        }

        public bool CanPharmacistAccept()
        {
            return service.CanPharmacistAccept(SelectedItem);
        }

        public bool CanDoctorAccept()
        {
            return service.CanDoctorAccept(SelectedItem);
        }
        protected void DeclineMedicineCommandExecute()
        {
            ((Medicine)SelectedItem).Declined = true;
            if (ApplicationContext.Instance.User.UserType is UserType.Doctor)
            {
                ((Medicine)SelectedItem).ReasonByDoctor = ApplicationContext.Instance.User.FirstName + " " + ApplicationContext.Instance.User.LastName + " is declined this medicine because of: " +
                   reasonDoctor;
            }
            if (ApplicationContext.Instance.User.UserType is UserType.Pharmacist)
            {
                ((Medicine)SelectedItem).ReasonByFarmacist = ApplicationContext.Instance.User.FirstName + ApplicationContext.Instance.User.LastName + " is declined this medicine because of: " +
                   reasonFarmacist;
            }
            OnPropertyChanged("Medicines");
            ApplicationContext.Instance.SaveMedicines();
        }

        protected virtual bool CanDeclineMedicineCommandExecute()
        {
            return SelectedItem != null && ((Medicine)SelectedItem).Accepted != true && ((Medicine)SelectedItem).Declined != true
                && ApplicationContext.Instance.User.UserType != UserType.Manager;
        }

        protected void RefreshMedicinesCommandExecute()
        {
            searchText = "";
            quantity = "";
            Items = new ObservableCollection<Entity>(service.GetAll());
            selectedMedicines = SelectedMedicines.All;
            OnPropertyChanged("SearchText");
            OnPropertyChanged("Price1");
            OnPropertyChanged("Price2");
            OnPropertyChanged("Quantity");
            OnPropertyChanged("Users");
        }

        protected virtual bool CanRefreshMedicinesCommandExecute()
        {
            return true;
        }

        protected void PendingMedicinesCommandExecute()
        {
            Items = new ObservableCollection<Entity>(service.getAllPendingMedicines());
            selectedMedicines = SelectedMedicines.Pending;
            OnPropertyChanged("Medicines");
        }

        protected bool CanPendingMedicinesCommandExecute()
        {
            return true;
        }

        protected void AcceptedMedicinesCommandExecute()
        {
            Items = new ObservableCollection<Entity>(service.getAllAcceptedMedicines());
            selectedMedicines = SelectedMedicines.Accepted;
            OnPropertyChanged("Medicines");
        }

        protected bool CanAcceptedMedicinesCommandExecute()
        {
            return true;
        }

        protected void DeclinedMedicinesCommandExecute()
        {
            Items = new ObservableCollection<Entity>(service.getAllDeclinedMedicines());
            selectedMedicines = SelectedMedicines.Declined;
            OnPropertyChanged("Medicines");
        }

        protected bool CanDeclinedMedicinesCommandExecute()
        {
            return true;
        }

        protected void UsersCommandExecute()
        {
            UsersView view = new UsersView();
            view.ShowDialog();
        }

        protected bool CanUsersCommandExecute()
        {
            return true;
        }

        protected void RefreshThisTabCommandExecute()
        {
            switch (SelectedMedicines)
            {
                case SelectedMedicines.All:
                    Items = new ObservableCollection<Entity>(service.GetAll());
                    OnPropertyChanged("Medicines");
                    searchText = "";
                    quantity = "";
                    OnPropertyChanged("SearchText");
                    break;
                case SelectedMedicines.Pending:
                    Items = new ObservableCollection<Entity>(service.getAllPendingMedicines());
                    OnPropertyChanged("Medicines");
                    searchText = "";
                    quantity = "";
                    OnPropertyChanged("SearchText");
                    break;
                case SelectedMedicines.Accepted:
                    Items = new ObservableCollection<Entity>(service.getAllAcceptedMedicines());
                    OnPropertyChanged("Medicines");
                    searchText = "";
                    quantity = "";
                    OnPropertyChanged("SearchText");
                    break;
                case SelectedMedicines.Declined:
                    Items = new ObservableCollection<Entity>(service.getAllDeclinedMedicines());
                    OnPropertyChanged("Medicines");
                    searchText = "";
                    quantity = "";
                    OnPropertyChanged("SearchText");
                    break;
            }
        }

        protected bool CanRefreshThisTabCommandExecute()
        {
            return true;
        }
    }
}

