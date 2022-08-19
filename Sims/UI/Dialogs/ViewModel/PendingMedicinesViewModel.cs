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
    public class PendingMedicinesViewModel : BaseDialogViewModel
    {
        private MedicineRepository repository = new MedicineRepository();
        private bool dataGridEnabled;
        private List<ComboData<Medicine>> medicines = new List<ComboData<Medicine>>();

        public PendingMedicinesViewModel(MedicinesView view) : base(view, typeof(Medicine))
        {
            Init();
            LoadMedicines();
        }

        public bool DataGridEnabled { get => dataGridEnabled; set => dataGridEnabled = value; }
        public List<ComboData<Medicine>> Medicines { get => medicines; set => medicines = value; }
        public void LoadMedicines()
        {
            foreach (Medicine medicine in repository.GetAll())
            {
                if (medicine.Accepted != true) {
                    medicines.Add(new ComboData<Medicine> { Name = medicine.Name, Value = medicine });
                }
            }
        }

        protected override void Init()
        {
            Items = new ObservableCollection<Entity>(repository.GetAll());
        }
    }
}
