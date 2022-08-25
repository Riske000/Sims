using Sims.Model;
using Sims.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims.Service
{
    public class IngredientService : IIngredientService<Ingredient>
    {
        private IngredientRepository ingredientRepository;

        public IngredientService()
        {
            ingredientRepository = new IngredientRepository();
        }

        public Entity Get(string id)
        {
            return ingredientRepository.Get(id);
        }

        public IEnumerable<Entity> GetAll()
        {
            return ingredientRepository.GetAll();
        }

        public IEnumerable<Entity> Search(string term = "")
        {
            return ingredientRepository.Search(term);
        }

        public void Add(Entity entity)
        {
            ingredientRepository.Add(entity);
        }

        public void Remove(Entity entity)
        {
            ingredientRepository.Remove(entity);
        }

    }
}
