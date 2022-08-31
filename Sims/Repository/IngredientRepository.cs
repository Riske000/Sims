using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sims.Model;

namespace Sims.Persistance
{
    public class IngredientRepository : Repository<Ingredient>
    {
        public Ingredient findByID(int id)
        {
            foreach (Ingredient i in ApplicationContext.Instance.Ingredients)
            {
                if (int.Parse(i.ID) == id)
                {
                    return i;
                }
            }
            return null;
        }
    }
}
