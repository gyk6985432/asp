using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using System.Data.Entity;

namespace BusinessLayer
{
    public class BusinessSettings
    {
        public static void Setbusiness()
        {
            DatabaseSettings.SetDatabase();
        }
    }
}
