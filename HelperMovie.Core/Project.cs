using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperMovie.Core
{
    public class Project
    {
        public int projectId { get; set; }
        public string projectName { get; set; }
        public string projectDescp { get; set; }
        public string projectStatus { get; set; }
        public string projectLead { get; set; }
        public string projectLeasedTech { get; set; }
    }
}
