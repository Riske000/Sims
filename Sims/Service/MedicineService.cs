using Sims.Model;
using Sims.Persistance;
using System;
using System.Collections.Generic;
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

        public IEnumerable<Entity> Search(string category, string sortType, string term = "", double price1 = 0, double price2 = 100000000, int quantity = 0)
        {
            return medicineRepository.Search(category, sortType, term, price1, price2, quantity);
        }

        public List<Entity> searchIngredients(string term = "")
        {
            return medicineRepository.searchIngredients(term);
        }

        public bool checkIfIngredientsHasString(Medicine m, string str)
        {
            return medicineRepository.checkIfIngredientsHasString(m, str);
        }

        public List<Entity> getAllPendingMedicines()
        {
            return medicineRepository.getAllPendingMedicines();
        }

        public Medicine getMedicineById(string id)
        {
            return medicineRepository.getMedicineById(id);
        }
    }
}
