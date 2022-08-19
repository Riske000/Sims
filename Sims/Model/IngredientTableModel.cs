using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims.Model
{
    public class IngredientTableModel
    {
        private double quantity;
        private string ingredientName;
        
        public double Quantity { get => quantity; set => quantity = value; }
        public string IngredientName { get => ingredientName; set => ingredientName = value; }

        
    }
}
