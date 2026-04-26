using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace KillToss1
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                foreach (Process proc in Process.GetProcessesByName("TOSCONSOLE1"))
                {
                    proc.Kill();
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
