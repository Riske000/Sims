using Sims.Model;
using Sims.UI.Dialogs.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims.Persistance
{
    public class MedicineRepository : Repository<Medicine>
    {
        public override IEnumerable<Entity> Search(string term = "")
        {
            List<Entity> result = new List<Entity>();

            foreach (Entity entity in ApplicationContext.Instance.Medicines)
            {
                if (((Medicine)entity).Name.Contains(term))
                {
                    result.Add(entity);
                }
            }

            return result;
        }

        public IEnumerable<Entity> Search( ObservableCollection<Entity> medicines, string category, string sortType, string term = "", double price1 = 0, double price2 = 100000000, int quantity = 0)
        {
            List<Entity> result = new List<Entity>();

            switch (category)
            {
                case "Code":
                    foreach (Entity entity in medicines)
                    {
                        if (((Medicine)entity).Code.ToLower().Contains(term))
                        {
                            result.Add(entity);
                        }
                    }
                    break;
                case "Name":
                    foreach (Entity entity in medicines)
                    {
                        if (((Medicine)entity).Name.ToLower().Contains(term))
                        {
                            result.Add(entity);
                        }
                    }
                    break;
                case "Producer":
                    foreach (Entity entity in medicines)
                    {
                        if (((Medicine)entity).Producer.ToLower().Contains(term))
                        {
                            result.Add(entity);
                        }
                    }
                    break;
                case "Price":
                    foreach (Entity entity in medicines)
                    {
                        if (price1 <= ((Medicine)entity).Price && ((Medicine)entity).Price <= price2)
                        {
                            result.Add(entity);
                        }
                    }
                    break;
                case "Quantity":
                    foreach (Entity entity in medicines)
                    {
                        if (((Medicine)entity).Quantity >= quantity)
                        {
                            result.Add(entity);
                        }
                    }
                    break;
                case "Ingredients":
                    result = searchIngredients(medicines, term);
                    break;
            }


            if (category == "Name")
            {
                if (sortType == "Ascending")
                {
                    return result.OrderBy(x => ((Medicine)x).Name);
                }
                else
                {
                    return result.OrderByDescending(x => ((Medicine)x).Name);
                }

            }
            else if (category == "Price")
            {
                if (sortType == "Ascending")
                {
                    return result.OrderBy(x => ((Medicine)x).Price);
                }
                else
                {
                    return result.OrderByDescending(x => ((Medicine)x).Price);
                }
            }
            else
            {
                if (sortType == "Ascending")
                {
                    return result.OrderBy(x => ((Medicine)x).Quantity);
                }
                else
                {
                    return result.OrderByDescending(x => ((Medicine)x).Quantity);
                }

            }

        }

        public List<Entity> searchIngredients(ObservableCollection<Entity> medicines, string term = "")
        {
            List<Entity> result = new List<Entity>();
            List<Entity> temp = new List<Entity>();
            string[] data = term.Split(' ');
            List<string> strings = new List<string>();
            List<string> operators = new List<string>();
            string begin = "";
            string end = data.Last();

            foreach (string s in data)
            {
                if (s == "&" || s == "|")
                {
                    strings.Add(begin.Trim());
                    begin = string.Empty;
                    operators.Add(s.Trim());
                    continue;
                }
                if (s == end)
                {
                    strings.Add(s.Trim());
                    break;
                }
                begin += " " + s;
            }
            if (strings[0] == "")
            {
                result = medicines.ToList();
            }

            if(operators.Count == 0)
            {
                foreach(Medicine medicine in ApplicationContext.Instance.Medicines)
                {
                    if(checkIfIngredientsHasString(medicine, strings[0]))
                    {
                        result.Add(medicine);
                    }
                }
                return result;
            }

            foreach (Medicine medicine in medicines)
            {
                for (int i = 0; i < strings.Count - 1; i++)
                {
                    for (int j = 0; j < operators.Count; j++)
                    {
                        if (operators[j] == "&")
                        {


                            if (checkIfIngredientsHasString(medicine, strings[i]) && checkIfIngredientsHasString(medicine, strings[i + 1]))
                            {
                                temp.Add(medicine);
                            }
                            else
                            {
                                temp.Clear();
                            }


                        }
                        else
                        {

                            if (checkIfIngredientsHasString(medicine, strings[i]) || checkIfIngredientsHasString(medicine, strings[i + 1]))
                            {
                                temp.Add(medicine);
                            }
                        }

                    }
                }
                if (temp.Count == 0)
                {
                    continue;
                }
                Medicine med = (Medicine)temp[0];
                temp.Clear();
                result.Add(med);
            }
            return result;
        }

        public bool checkIfIngredientsHasString(Medicine m, string str)
        {
            foreach (Ingredient ingredient in m.Ingredients.Values)
            {
                if (ingredient.Name.ToLower().Contains(str.ToLower()))
                {
                    return true;
                }
            }

            return false;
        }

        public Medicine getMedicineById(string id)
        {
            foreach(Medicine medicine in ApplicationContext.Instance.Medicines)
            {
                if(medicine.ID == id)
                {
                    return medicine;
                }
            }
            return null;
        }

        public ObservableCollection<Medicine> getAllPendingMedicines()
        {
            ObservableCollection<Medicine> result = new ObservableCollection<Medicine>();

            foreach (Medicine medicine in ApplicationContext.Instance.Medicines)
            {
                if(medicine.Accepted == false && medicine.Declined == false)
                {
                    result.Add(medicine);
                }
            }

            return result;
        }

        public ObservableCollection<Medicine> getAllAcceptedMedicines()
        {
            ObservableCollection<Medicine> result = new ObservableCollection<Medicine>();

            foreach (Medicine medicine in ApplicationContext.Instance.Medicines)
            {
                if (medicine.Accepted == true && medicine.Declined == false)
                {
                    result.Add(medicine);
                }
            }

            return result;
        }

        public ObservableCollection<Medicine> getAllDeclinedMedicines()
        {
            ObservableCollection<Medicine> result = new ObservableCollection<Medicine>();

            foreach (Medicine medicine in ApplicationContext.Instance.Medicines)
            {
                if (medicine.Accepted == false && medicine.Declined == true)
                {
                    result.Add(medicine);
                }
            }

            return result;
        }
    }
}
