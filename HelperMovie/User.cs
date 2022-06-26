using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperMovie
{
    public class User

     {
        public User()
        {

        }
        public int? id { get; set; }

        private string login, pass;

        public string Login
        {
            get { return login; }
            set { login = value ?? throw new ArgumentNullException("login is required."); }
        }

        public string Pass
        {
            get { return pass; }
            set { pass = value ?? throw new ArgumentNullException("password is required."); }
        }

        // public string position { get; private set; }


        public User(string login, string password)
        {
            this.Login = login;
            this.Pass = password;
        }
    }
}
