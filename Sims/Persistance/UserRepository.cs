using Sims.CompositeComon.Enums;
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
        public override IEnumerable<Entity> Search(string term = "")
        {
            List<Entity> result = new List<Entity>();

            foreach (Entity entity in ApplicationContext.Instance.Users)
            {
                if (((User)entity).ID.Contains(term))
                {
                    result.Add(entity);
                }
            }

            return result;
        }

        public IEnumerable<Entity> FilterAndSortUsers(string filterType, string sortType, string sortBy)
        {
            List<Entity> result = new List<Entity>();

            foreach (Entity entity in ApplicationContext.Instance.Users)
            {
                if (((User)entity).UserType.ToString() == filterType)
                {
                    result.Add(entity);
                }
                else
                {
                    continue;
                }
            }

            if (sortBy == "First name")
            {
                if (sortType == "Ascending")
                {
                    return result.OrderBy(x => ((User)x).FirstName);
                }
                else
                {
                    return result.OrderByDescending(x => ((User)x).FirstName);
                }

            }
            else
            {
                if (sortType == "Ascending")
                {
                    return result.OrderBy(x => ((User)x).LastName);
                }
                else
                {
                    return result.OrderByDescending(x => ((User)x).LastName);
                }

            }


            return result;
        }

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

        public bool CheckEmail(string email)
        {
            foreach(User user in ApplicationContext.Instance.Users)
            {
                if(user.Email == email)
                {
                    return false;
                }
            }
            return true;
        }

        public bool CheckJmbg(string jmbg)
        {
            foreach (User user in ApplicationContext.Instance.Users)
            {
                if (user.Jmbg == jmbg)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
