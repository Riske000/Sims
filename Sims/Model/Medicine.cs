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
        private Boolean deleted;
        private double price;

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

        }
    }
}
