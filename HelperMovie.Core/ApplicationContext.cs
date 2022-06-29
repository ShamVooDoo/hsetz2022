using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperMovie.Core
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        static public List<Invite> invites = new List<Invite>();
        static public List <Tech> techniques = new List<Tech>();
        static public List<Project> projects = new List<Project>();

        public ApplicationContext() : base("DefaultConnection")
        {
        }
    }
}
