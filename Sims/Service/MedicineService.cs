using Sims.Model;
using Sims.Persistance;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims.Service
{
    public class MedicineService : IMedicineService<Medicine>
    {
        private MedicineRepository medicineRepository;

        public MedicineService()
        {
            medicineRepository = new MedicineRepository();
        }

        public Entity Get(string id)
        {
            return medicineRepository.Get(id);
        }

        public IEnumerable<Entity> GetAll()
        {
            return medicineRepository.GetAll();
        }

        public IEnumerable<Entity> Search(string term = "")
        {
            return medicineRepository.Search(term);
        }

        public void Add(Entity entity)
        {
            medicineRepository.Add(entity);
        }

        public void Remove(Entity entity)
        {
            medicineRepository.Remove(entity);
        }

        public IEnumerable<Entity> Search(ObservableCollection<Entity> medicines, string category, string sortType, string term = "", double price1 = 0, double price2 = 100000000, int quantity = 0)
        {
            return medicineRepository.Search(medicines, category, sortType, term, price1, price2, quantity);
        }

        public List<Entity> searchIngredients(ObservableCollection<Entity> medicines,  string term = "")
        {
            return medicineRepository.searchIngredients(medicines, term);
        }

        public bool checkIfIngredientsHasString(Medicine m, string str)
        {
            return medicineRepository.checkIfIngredientsHasString(m, str);
        }

        public Medicine getMedicineById(string id)
        {
            return medicineRepository.getMedicineById(id);
        }

        public ObservableCollection<Medicine> getAllPendingMedicines()
        {
            return medicineRepository.getAllPendingMedicines();
        }

        public ObservableCollection<Medicine> getAllAcceptedMedicines()
        {
            return medicineRepository.getAllAcceptedMedicines();
        }

        public ObservableCollection<Medicine> getAllDeclinedMedicines()
        {
            return medicineRepository.getAllDeclinedMedicines();
        }
    }
}
