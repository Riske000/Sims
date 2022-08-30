using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims.Model
{
    public class Accept
    {
        Medicine medicine;
        User pharmacistWhoAccepted;

        public Medicine Medicine
        {
            get { return medicine; }
            set { medicine = value; }
        }

        public User PharmacistWhoAccepted
        {
            get { return pharmacistWhoAccepted; }
            set { pharmacistWhoAccepted = value; }
        }
    }
}
