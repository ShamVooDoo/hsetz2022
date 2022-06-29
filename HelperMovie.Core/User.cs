using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperMovie.Core
{
    public class User
    {
        //int id;
        string login;
        string pass;
        string name;
        int? age;
        string position;
        string team;
        int admin;

        public int? id { get; set; }

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

        public string Name
        {
            get { return name; }
            set { name = value ?? throw new ArgumentException("password is required."); }
        }

        public int? Age
        {
            get { return (int)age; }
            set { if (value == null) { throw new ArgumentNullException("age is required."); } else { age = value; } }
        }

        public string Position 
        {   
            get { return position; }
            set { position = value ?? throw new ArgumentNullException("position is required."); }
        }

        public string Team { get { return team; } set { team = value; } }

        public int Admin { get; set; }

        // public string position { get; private set; }

        public User()
        {

        }
        public User(string login, string password, string name, int age, string position)
        {
            this.login = login;
            this.pass = password;
            this.name = name;
            this.age = age;
            this.position = position;
            this.admin = 1;
        }

        public User(string login, string password, string name, int age, string position, int admin)
        {
            this.login = login;
            this.pass = password;
            this.name = name;
            this.age = age;
            this.position = position;
            this.admin = admin;
        }
    }
}
