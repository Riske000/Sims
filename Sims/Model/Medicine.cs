using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims.Model
{
    public class Medicine : Entity
    {
        private int id;
        private string code;
        private string name;
        private string producer;
        private int quantity;
        private Dictionary<long, Ingredient> ingredients;
        private Boolean accepted;
        private Boolean deleted;

        public int Id { get { return id; } set { id = value; } }
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

        public Dictionary<long, Ingredient> Ingredients
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
        public override void InitExportList()
        {
            throw new NotImplementedException();
        }

        public override string Validate(string columnName)
        {
            throw new NotImplementedException();
        }

        protected override void ValidateSelf()
        {
            throw new NotImplementedException();
        }
    }
}
