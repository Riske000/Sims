using Sims.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims.Persistance
{
    public class UserRepository : Repository<User>
    {
        //public override IEnumerable<Entity> Search(string term = "")
        //{
        //    List<Entity> result = new List<Entity>();

        //    foreach (Entity entity in ApplicationContext.Instance.Users)
        //    {
        //        if (((User)entity).ID.Contains(term))
        //        {
        //            result.Add(entity);
        //        }
        //    }

        //    return result;
        //}

        public User getUserWithEmailAndPassword(string email, string password)
        {
            foreach (Entity entity in ApplicationContext.Instance.Users)
            {
                if (((User)entity).Email == email && ((User)entity).Password == password)
                {
                    return (User)entity;
                }
            }

            return null;
        }
    }
}
