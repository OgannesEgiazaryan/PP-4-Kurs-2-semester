using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp19.Classes
{
    internal class Logger
    {
        
        static Entities Entities = new Entities();

        public static void LogsEnter(int id_u, int id_a, string des)
        {
            Logs logs = new Logs();
            Entities.Logs.Add(logs);
            logs.ID_User = id_u;
            logs.ID_Action = id_a;
            logs.Date_ = DateTime.Now;
            logs.Description = des;
            Entities.SaveChanges();
        }
    }
}
