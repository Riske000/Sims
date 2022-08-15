using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims.Model
{
    public class Ingredient : Entity
    {
        private int id;
        private string name;
        private string description;

        public int ID { get { return id; } set { id = value; } }
        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged("Name"); }
        }

        public string Description
        {
            get { return description; }
            set { description = value; OnPropertyChanged("Description"); }
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
