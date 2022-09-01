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
        private Boolean blocked;

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
            return string.Empty;
        }

        public Boolean Blocked
        {
            get { return blocked; }
            set { blocked = value; OnPropertyChanged("Blocked"); }
        }

        protected override void ValidateSelf()
        {
            if(this.FirstName == null || this.FirstName == "")
            {
                this.ValidationErrors["FirstName"] = "First name is required.";
            }
            if (this.LastName == null || this.LastName == "")
            {
                this.ValidationErrors["LastName"] = "Last name is required.";
            }
            if (this.Password == null || this.Password == "")
            {
                this.ValidationErrors["Password"] = "Password is required.";
            }
            if (this.Jmbg == null || this.Jmbg == "")
            {
                this.ValidationErrors["Jmbg"] = "Jmbg is required.";
            }
            if (this.Email == null || this.Email == "")
            {
                this.ValidationErrors["Email"] = "Email is required.";
            }
            if (this.Phone == null || this.Phone == "")
            {
                this.ValidationErrors["Phone"] = "Phone is required.";
            }
        }

        public string FullName(User user)
        {
            string fullName = user.FirstName + " " + user.LastName;
            return fullName;
        }
    }
}
