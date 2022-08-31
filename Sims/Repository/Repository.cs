using Sims.Core;
using Sims.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims.Persistance
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        public void Add(Entity entity)
        {
            ApplicationContext.Instance.Get(typeof(TEntity)).Add(entity);
        }

        public Entity Get(string id)
        {
            return ApplicationContext.Instance.Get(typeof(TEntity)).Where(x => x.ID == id).FirstOrDefault();
        }

        public IEnumerable<Entity> GetAll()
        {
            if(ApplicationContext.Instance.Get(typeof(TEntity)).First() is Medicine)
            {
                foreach(Medicine medicine in ApplicationContext.Instance.Medicines.ToList())
                {
                    if(medicine.Deleted == true)
                    {
                        ApplicationContext.Instance.Medicines.Remove(medicine);
                    }
                }
            }

            return ApplicationContext.Instance.Get(typeof(TEntity));
        }

        public void Remove(Entity entity)
        {
            Entity entityToRemove = Get(entity.ID);
            ApplicationContext.Instance.Get(typeof(TEntity)).Remove(entityToRemove);
        }

        public virtual IEnumerable<Entity> Search(string term = "")
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            ApplicationContext.Instance.Save();
        }
    }
}
