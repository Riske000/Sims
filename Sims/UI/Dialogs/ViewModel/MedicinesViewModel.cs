using Sims.CompositeComon;
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
    public class MedicinesViewModel : BaseDialogViewModel
    {
        private MedicineRepository repository = new MedicineRepository();
        private bool dataGridEnabled;
        private List<ComboData<Medicine>> medicines = new List<ComboData<Medicine>>();
        private RelayCommand searchCommand;
        private string searchText;
        private ObservableCollection<IngredientTableModel> ingredientTableModels;

        public MedicinesViewModel(MedicinesView view) : base(view, typeof(Medicine))
        {
            Init();
            LoadMedicines();
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
            foreach (Medicine medicine in repository.GetAll())
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
        public bool DataGridEnabled { get => dataGridEnabled; set => dataGridEnabled = value; }
        public List<ComboData<Medicine>> Medicines { get => medicines; set => medicines = value; }
        public string SearchText { get => searchText; set => searchText = value; }

        protected override void Init()
        {
            Items = new ObservableCollection<Entity>(repository.GetAll());
        }

        protected void SearchCommandExecute()
        {
            repository.searchIngredients(searchText);
        }

        protected virtual bool CanSearchCommandExecute()
        {
            return true;
        }
    }
}
