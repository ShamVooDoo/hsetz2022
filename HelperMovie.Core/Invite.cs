using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperMovie.Core
{
    public class Invite
    {
        public string SenderLogin { get; set; }
        public string AddresseLogin { get; set; }
        public string SenderPosition { get; set; } 
        public string AddressePosition { get; set; }
        public string TeamName { get; set; }

        public Invite (string senderLogin, string addresseLogin, string senderPosition, string addressePosition ,string teamName)
        {
            SenderLogin = senderLogin;
            AddresseLogin = addresseLogin;
            SenderPosition = senderPosition;
            AddressePosition = addressePosition;
            TeamName = teamName;
        }
    }
}
