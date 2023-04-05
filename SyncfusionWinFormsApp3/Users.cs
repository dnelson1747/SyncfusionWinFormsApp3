using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncfusionWinFormsApp3
{
    public class Users
    {
        public int UserID { get; set; }
        public string Username { get; set; }

        public Users(int userID, string username)
        {
            UserID = userID;
            Username = username;
        }

        public override string ToString()
        {
            return Username;
        }
    }

}
