using Sims.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims.Persistance
{
    public class MedicineRepository : Repository<Medicine>
    {
        public override IEnumerable<Entity> Search(string term = "")
        {
            List<Entity> result = new List<Entity>();

            foreach (Entity entity in ApplicationContext.Instance.Medicines)
            {
                if (((Medicine)entity).Name.Contains(term))
                {
                    result.Add(entity);
                }
            }

            return result;
        }

        public IEnumerable<Entity> Search(string category, string term = "", double price1 = 0, double price2 = 100000000, int quantity = 0)
        {
            List<Entity> result = new List<Entity>();

            switch (category)
            {
                case "Code":
                    foreach (Entity entity in ApplicationContext.Instance.Medicines)
                    {
                        if (((Medicine)entity).Code.ToLower().Contains(term))
                        {
                            result.Add(entity);
                        }
                    }
                    break;
                case "Name":
                    foreach (Entity entity in ApplicationContext.Instance.Medicines)
                    {
                        if (((Medicine)entity).Name.ToLower().Contains(term))
                        {
                            result.Add(entity);
                        }
                    }
                    break;
                case "Producer":
                    foreach (Entity entity in ApplicationContext.Instance.Medicines)
                    {
                        if (((Medicine)entity).Producer.ToLower().Contains(term))
                        {
                            result.Add(entity);
                        }
                    }
                    break;
                case "Price":
                    foreach (Entity entity in ApplicationContext.Instance.Medicines)
                    {
                        if (price1 <= ((Medicine)entity).Price && ((Medicine)entity).Price <= price2)
                        {
                            result.Add(entity);
                        }
                    }
                    break;
                case "Quantity":
                    foreach(Entity entity in ApplicationContext.Instance.Medicines)
                    {
                        if(((Medicine)entity).Quantity >= quantity)
                        {
                            result.Add(entity);
                        }
                    }
                    break;
                case "Ingredients":
                    result = searchIngredients(term);
                    break;
            }

            return result;
        }

        public List<Entity> searchIngredients(string term = "")
        {
            List<Entity> result = new List<Entity>();
            string[] data = term.Split(' ');
            List<string> strings = new List<string>();
            List<string> operators = new List<string>();
            string begin = "";
            string end = data.Last();
            foreach (string s in data)
            {
                if(s == "&" || s == "|")
                {
                    strings.Add(begin.Trim());
                    begin = string.Empty;
                    operators.Add(s.Trim());
                    continue;
                }
                if(s == end)
                {
                    strings.Add(s.Trim());
                    break;
                }
                begin += " " + s;
            }
            return result;
        }
    }
}
