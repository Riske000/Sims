using Sims.CompositeComon.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims.Model
{
    public class User : Entity
    {
        private string jmbg;
        private string email;
        private string password;
        private string firstName;
        private string lastName;
        private string phone;
        private UserType userType;

        public string Jmbg
        {
            get { return jmbg; }
            set { jmbg = value; OnPropertyChanged("Jmbg");  }
        }

        public string Email
        {
            get { return email; }
            set { email = value; OnPropertyChanged("Email"); }
        }

        public string Password
        {
            get { return password; }
            set { password = value; OnPropertyChanged("Password"); }
        }

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; OnPropertyChanged("FirstName"); }
        }

        public string LastName
        {
            get {  return lastName; }
            set { lastName = value; OnPropertyChanged("LastName"); }
        }

        public string Phone
        {
            get { return phone; }
            set { phone = value; OnPropertyChanged("Phone"); }
        }

        public UserType UserType
        {
            get { return userType; }
            set { userType = value; OnPropertyChanged("UserType"); }
        }
        public override void InitExportList()
        {
            throw new NotImplementedException();
        }

        public override string Validate(string columnName)
        {
            throw new NotImplementedException();
        }

        protected override void ValidateSelf()
        {
            throw new NotImplementedException();
        }

        public string FullName(User user)
        {
            string fullName = user.FirstName + " " + user.LastName;
            return fullName;
        }
    }
}
