using Sims.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims.Service
{
    public interface IIngredientService<TEntity> where TEntity : class
    {
        Entity Get(string id);

        IEnumerable<Entity> GetAll();
        IEnumerable<Entity> Search(string term = "");

        void Add(Entity entity);
        void Remove(Entity entity);
    }
}
