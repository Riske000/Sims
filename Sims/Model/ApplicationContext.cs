using Sims.CompositeComon.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims.Model
{
    public class ApplicationContext
    {
        private static ApplicationContext instance = null;
        private List<Entity> users = new List<Entity>();
        private List<Entity> ingredients = new List<Entity>();
        private List<Entity> medicines = new List<Entity>();
        private MainWindowViewModel mainWindowViewModel;
        private User user;

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

                Ingredient ingredient= new Ingredient();
                ingredient.ID = data[0];
                ingredient.Name= data[1];
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
                string [] data2 = data[5].Split(';');
                foreach(string s in data2)
                {
                    string[] data3 = s.Split(',');
                    double d = double.Parse(data3[0], System.Globalization.CultureInfo.InvariantCulture);
                    medicine.Ingredients.Add(d, findByID(int.Parse(data3[1])));
                }
                medicine.Accepted = Boolean.Parse(data[6]);
                medicine.Deleted = Boolean.Parse(data[7]);
                medicine.Price = double.Parse(data[8]);
                result.Add(medicine);
            }

            medicines = result;
        }

        public Ingredient findByID(int id)
        {
            foreach(Ingredient i in ingredients)
            {
                if(int.Parse(i.ID) == id)
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
                    line += ((User)entity).UserType.ToString();

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
                    foreach(KeyValuePair<double, Ingredient> kvp in ((Medicine)entity).Ingredients)
                    {
                        line += kvp.Key + ",";
                        line += kvp.Value.ID + ";";
                    }
                    line = line.Remove(line.Length - 1, 1) + "|";
                    line += ((Medicine)entity).Accepted + "|";
                    line += ((Medicine)entity).Deleted;
                    line +=((Medicine)entity).Price;

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

        
        
        public void Save()
        {
            //SaveUsers();
            //SaveIngredients();
            //SaveMedicines();
        }
    }
}
