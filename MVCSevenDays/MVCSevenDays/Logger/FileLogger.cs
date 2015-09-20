using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace MVCSevenDays.Logger
{
    public class FileLogger
    {
        public void LogException(Exception e)
        {
            File.WriteAllLines("E:\\Visual Studio 2013\\Projects\\MVCSevenDays\\Errors\\" + DateTime.Now.ToString("dd-MM-yyyy mm hh ss") + ".txt", new string[] {
                "Message : "+e.Message,"Stacktrace"+e.StackTrace
            });
        }
    }
}