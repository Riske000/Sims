using Sims.CompositeComon.Enums;
using Sims.CompositeComon.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sims.Service;
using Sims.UI.Dialogs.ViewModel;
using System.Threading;

namespace Sims.Model
{
    public class ApplicationContext
    {
        private static ApplicationContext instance = null;
        private List<Entity> users = new List<Entity>();
        private List<Entity> ingredients = new List<Entity>();
        private List<Entity> medicines = new List<Entity>();
        private List<ScheudledAddition> scheudledAddiotons = new List<ScheudledAddition>();
        private MainWindowViewModel mainWindowViewModel;
        private User user;
        private MedicineService service = new MedicineService();



        public ApplicationContext()
        {
            // Load();
        }

        public static ApplicationContext Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ApplicationContext();
                    instance.LoadUsers();
                    instance.LoadIngredients();
                    instance.LoadMedicines();
                    instance.LoadScheudledAdditions();
                    foreach (ScheudledAddition scheudledAddition in ApplicationContext.Instance.ScheudledAddiotons)
                    {
                        Instance.AddAmount(scheudledAddition);
                    }
                }
                return instance;
            }
            
        }

        public List<Entity> Users
        {
            get { return users; }
            set { users = value; }
        }

        public List<Entity> Ingredients
        {
            get { return ingredients; }
            set { ingredients = value; }
        }

        public List<Entity> Medicines
        {
            get { return medicines; }
            set { medicines = value; }
        }

        public List<ScheudledAddition> ScheudledAddiotons
        {
            get { return scheudledAddiotons; }
            set { scheudledAddiotons = value; }
        }

        public MainWindowViewModel MainWindowViewModel
        {
            get { return mainWindowViewModel; }
            set { mainWindowViewModel = value; }
        }

        public User User { get => user; set => user = value; }

        public UserType GetUserType(string value)
        {
            if (value == "Pharmacist")
            {
                return UserType.Pharmacist;
            }
            else if (value == "Manager")
            {
                return UserType.Manager;
            }
            else
            {
                return UserType.Doctor;
            }
        }

        public void LoadUsers()
        {
            List<Entity> result = new List<Entity>();

            if (!File.Exists("users.txt"))
            {
                users = result;
                return;
            }

            StreamReader reader = new StreamReader("users.txt");
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                string[] data = line.Split('|');

                User user = new User();
                user.ID = data[0];
                user.Jmbg = data[1];
                user.Email = data[2];
                user.Password = data[3];
                user.FirstName = data[4];
                user.LastName = data[5];
                user.Phone = data[6];
                user.UserType = GetUserType(data[7]);
                user.Blocked = Boolean.Parse(data[8]);
                result.Add(user);
            }

            users = result;
        }

        public void LoadIngredients()
        {
            List<Entity> result = new List<Entity>();

            if (!File.Exists("ingredients.txt"))
            {
                ingredients = result;
                return;
            }

            StreamReader reader = new StreamReader("ingredients.txt");
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                string[] data = line.Split('|');

                Ingredient ingredient = new Ingredient();
                ingredient.ID = data[0];
                ingredient.Name = data[1];
                ingredient.Description = data[2];
                result.Add(ingredient);
            }

            ingredients = result;
        }

        public void LoadMedicines()
        {
            List<Entity> result = new List<Entity>();

            if (!File.Exists("medicines.txt"))
            {
                medicines = result;
                return;
            }

            StreamReader reader = new StreamReader("medicines.txt");
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                string[] data = line.Split('|');
                

                Medicine medicine = new Medicine();
                medicine.ID = data[0];
                medicine.Code = data[1];
                medicine.Name = data[2];
                medicine.Producer = data[3];
                medicine.Quantity = int.Parse(data[4]);
                string[] data2 = data[5].Split(';');
                foreach (string s in data2)
                {
                    string[] data3 = s.Split('_');
                    double d = double.Parse(data3[0]);
                    medicine.Ingredients.Add(d, findByID(int.Parse(data3[1])));
                }
                medicine.Accepted = Boolean.Parse(data[6]);
                medicine.Declined = Boolean.Parse(data[7]);
                medicine.Deleted = Boolean.Parse(data[8]);
                medicine.Price = double.Parse(data[9]);
                medicine.ReasonByFarmacist = data[10];
                medicine.ReasonByDoctor = data[11];
                medicine.CounterForFarmacist = int.Parse(data[12]);
                medicine.CounterForDoctor = int.Parse(data[13]);
                result.Add(medicine);
            }

            medicines = result;
        }

        public void LoadScheudledAdditions()
        {
            List<ScheudledAddition> result = new List<ScheudledAddition>();

            if (!File.Exists("scheudledAdditions.txt"))
            {
                scheudledAddiotons = result;
                return;
            }

            StreamReader reader = new StreamReader("scheudledAdditions.txt");
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                string[] data = line.Split('|');

                ScheudledAddition scheudledAddition = new ScheudledAddition();

                scheudledAddition.IdOfMedicine = data[0];
                scheudledAddition.AmountToAdd = int.Parse(data[1]);
                scheudledAddition.DateOfAddition = DateTime.Parse(data[2]);
                result.Add(scheudledAddition);
            }

            scheudledAddiotons = result;
        }

        public void SaveScheudledAdditions()
        {
            if (scheudledAddiotons == null)
            {
                return;
            }

            using (StreamWriter file = new StreamWriter("scheudledAdditions.txt"))
            {
                foreach (ScheudledAddition scheudledAddition in scheudledAddiotons)
                {
                    string line = string.Empty;

                    line += scheudledAddition.IdOfMedicine + "|";
                    line += scheudledAddition.AmountToAdd + "|";
                    line += scheudledAddition.DateOfAddition;

                    file.WriteLine(line);
                }
            }
        }



        public Ingredient findByID(int id)
        {
            foreach (Ingredient i in ingredients)
            {
                if (int.Parse(i.ID) == id)
                {
                    return i;
                }
            }
            return null;
        }

        public void SaveUsers()
        {
            if (users == null)
            {
                return;
            }

            using (StreamWriter file = new StreamWriter("users.txt"))
            {
                foreach (Entity entity in users)
                {
                    string line = string.Empty;

                    line += ((User)entity).ID + "|";
                    line += ((User)entity).Jmbg + "|";
                    line += ((User)entity).Email + "|";
                    line += ((User)entity).Password + "|";
                    line += ((User)entity).FirstName + "|";
                    line += ((User)entity).LastName + "|";
                    line += ((User)entity).Phone + "|";
                    line += ((User)entity).UserType.ToString() + "|";
                    line += ((User)entity).Blocked;

                    file.WriteLine(line);
                }
            }
        }

        public void SaveIngredients()
        {
            if (ingredients == null)
            {
                return;
            }

            using (StreamWriter file = new StreamWriter("ingredients.txt"))
            {
                foreach (Entity entity in ingredients)
                {
                    string line = string.Empty;

                    line += ((Ingredient)entity).ID + "|";
                    line += ((Ingredient)entity).Name + "|";
                    line += ((Ingredient)entity).Description;

                    file.WriteLine(line);
                }
            }
        }

        public void SaveMedicines()
        {
            if (medicines == null)
            {
                return;
            }

            using (StreamWriter file = new StreamWriter("medicines.txt"))
            {
                foreach (Entity entity in medicines)
                {
                    string line = string.Empty;

                    line += ((Medicine)entity).ID + "|";
                    line += ((Medicine)entity).Code + "|";
                    line += ((Medicine)entity).Name + "|";
                    line += ((Medicine)entity).Producer + "|";
                    line += ((Medicine)entity).Quantity + "|";
                    foreach (KeyValuePair<double, Ingredient> kvp in ((Medicine)entity).Ingredients)
                    {
                        line += kvp.Key + "_";
                        line += kvp.Value.ID + ";";
                    }
                    line = line.Remove(line.Length - 1, 1) + "|";
                    line += ((Medicine)entity).Accepted + "|";
                    line += ((Medicine)entity).Declined + "|";
                    line += ((Medicine)entity).Deleted + "|";
                    line += ((Medicine)entity).Price + "|";
                    line += ((Medicine)entity).ReasonByFarmacist + "|";
                    line += ((Medicine)entity).ReasonByDoctor + "|";
                    line += ((Medicine)entity).CounterForFarmacist + "|";
                    line += ((Medicine)entity).CounterForDoctor;
                    file.WriteLine(line);
                }
            }
        }

        public List<Entity> Get(Type type)
        {
            if (type == typeof(User))
            {
                return Users;
            }
            else if (type == typeof(Ingredient))
            {
                return Ingredients;
            }
            else
            {
                return Medicines;
            }
        }

        public string GenerateIDForUser()
        {
            int id = (int.Parse(ApplicationContext.Instance.Users.Last().ID) + 1);
            return id.ToString();
        }

        public string GenerateIDForMedicine()
        {
            int id = (int.Parse(ApplicationContext.Instance.Medicines.Last().ID) + 1);
            return id.ToString();
        }

        public void Save()
        {
            SaveUsers();
            SaveIngredients();
            SaveMedicines();
        }

        public void AddAmount(ScheudledAddition scheudledAddition)
        {
            if (scheudledAddition.DateOfAddition == DateTime.Today)
            {
                Medicine medicine = service.getMedicineById(scheudledAddition.IdOfMedicine);
                medicine.Quantity += scheudledAddition.AmountToAdd;


            }

        }

        public void RemoveScheudledAddition()
        {
            foreach (ScheudledAddition scheudledAddition in scheudledAddiotons.ToList())
            {
                if (scheudledAddition.DateOfAddition == DateTime.Today)
                {
                    Instance.ScheudledAddiotons.Remove(scheudledAddition);

                    Instance.SaveScheudledAdditions();
                }
            }
        }
    }
}
