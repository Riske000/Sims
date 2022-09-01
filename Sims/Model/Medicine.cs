using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims.Model
{
    public class Medicine : Entity
    {
        private string code;
        private string name;
        private string producer;
        private int quantity;
        private Dictionary<double, Ingredient> ingredients;
        private Boolean accepted;
        private Boolean declined;
        private Boolean deleted;
        private double price;
        private string reasonByFarmacist;
        private string reasonByDoctor;
        private int counterForFarmacist;
        private int counterForDoctor;
        public Medicine()
        {
            this.ingredients = new Dictionary<double, Ingredient>();
        }

        public string Code
        {
            get { return code; }
            set { code = value; OnPropertyChanged("Code"); }
        }

        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged("Name"); }
        }

        public string Producer
        {
            get { return producer; }
            set { producer = value; OnPropertyChanged("Producer"); }
        }

        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; OnPropertyChanged("Quantity"); }
        }

        public Dictionary<double, Ingredient> Ingredients
        {
            get { return ingredients; }
            set { ingredients = value; OnPropertyChanged("Ingredients"); }
        }

        public Boolean Accepted
        {
            get { return accepted; }
            set { accepted = value; OnPropertyChanged("Accepted"); }
        }

        public Boolean Declined
        {
            get { return declined; }
            set { declined = value; OnPropertyChanged("Declined"); }
        }

        public bool Deleted
        {
            get { return deleted; }
            set { deleted = value; OnPropertyChanged("Deleted"); }
        }

        public double Price
        {
            get { return price; }
            set { price = value; OnPropertyChanged("Price"); }
        }

        public string ReasonByFarmacist
        {
            get { return reasonByFarmacist; }
            set { reasonByFarmacist = value; OnPropertyChanged("ReasonByFarmacist"); }
        }

        public string ReasonByDoctor
        {
            get { return reasonByDoctor; }
            set { reasonByDoctor = value; OnPropertyChanged("ReasonByDoctor"); }
        }

        public int CounterForFarmacist
        {
            get { return counterForFarmacist; }
            set { counterForFarmacist = value; OnPropertyChanged("CounterForFarmacist"); }
        }

        public int CounterForDoctor
        {
            get { return counterForDoctor; }
            set { counterForDoctor = value; OnPropertyChanged("CounterForDoctor"); }
        }

        public override void InitExportList()
        {
            throw new NotImplementedException();
        }

        public override string Validate(string columnName)
        {
            return string.Empty;
        }

        protected override void ValidateSelf()
        {
            if (this.Code == null || this.Code== "")
            {
                this.ValidationErrors["Code"] = "Code is required.";
            }
            if (this.Name == null || this.Name == "")
            {
                this.ValidationErrors["Name"] = "Name is required.";
            }
            if (this.Producer == null || this.Producer == "")
            {
                this.ValidationErrors["Producer"] = "Producer is required.";
            }
            if (this.Price == 0 || this.price < 0)
            {
                this.ValidationErrors["Price"] = "Price must be positive.";
            }
            
        }
    }
}
