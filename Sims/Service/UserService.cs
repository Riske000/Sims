using Sims.Model;
using Sims.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims.Service
{
    public class UserService : IUserService<User>
    {
        private UserRepository userRepository;

        public UserService()
        {
            userRepository = new UserRepository();
        }
        public Entity Get(string id)
        {
            return userRepository.Get(id);
        }

        public IEnumerable<Entity> GetAll()
        {
            return userRepository.GetAll();
        }

        public IEnumerable<Entity> Search(string term = "")
        {
            return userRepository.Search(term);
        }

        public void Add(Entity entity)
        {
            userRepository.Add(entity);
        }

        public void Remove(Entity entity)
        {
            userRepository.Remove(entity);
        }

        public IEnumerable<Entity> FilterAndSortUsers(string filterType, string sortType, string sortBy)
        {
            return userRepository.FilterAndSortUsers(filterType, sortType, sortBy);    
        }

        public User getUserWithEmailAndPassword(string email, string password)
        {
            return userRepository.getUserWithEmailAndPassword(email, password);
        }

        public bool CheckEmail(string email)
        {
            return userRepository.CheckEmail(email);
        }

        public bool CheckJmbg(string jmbg)
        {
            return userRepository.CheckJmbg(jmbg);
        }
    }
}
