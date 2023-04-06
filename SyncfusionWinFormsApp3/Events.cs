using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace SyncfusionWinFormsApp3
{
    public class Events
    {
        public int EventID { get; set; }
        public string EventName { get; set; }

        public override string ToString()
        {
            return EventName;
        }

    }
}




