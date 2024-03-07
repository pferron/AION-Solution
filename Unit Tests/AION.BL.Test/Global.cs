using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AION.BL.Test
{
    public static class Global
    {

        public static bool FreezeTesting
        {
            get
            {
                return bool.Parse(ConfigurationManager.AppSettings["KillAllTests"].ToUpper());
            }
        }

    }
}
