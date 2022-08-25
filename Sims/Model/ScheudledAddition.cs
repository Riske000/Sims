using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims.Model
{
    public class ScheudledAddition
    {
        private string idOfMedicine;
        private int amountToAdd;
        private DateTime dateOfAddition;

        public string IdOfMedicine
        {
            get { return idOfMedicine; }
            set { idOfMedicine = value; }
        }

        public int AmountToAdd
        {
            get { return amountToAdd; }
            set { amountToAdd = value; }
        }

        public DateTime DateOfAddition
        {
            get { return dateOfAddition; }
            set { dateOfAddition = value; }
        }
    }
}
