using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Diagnostics;

namespace hardy_analyseOBS.classes
{
    internal class Cpu : Component
    {
        private PerformanceCounter usage = new PerformanceCounter("Processor", "% Processor Time", "_Total");

        public Cpu()
        {
            set_intformation("select * from Win32_Processor");
        }

        public override int get_usage()
        {
            float first_cpu_usage_value = usage.NextValue();

            System.Threading.Thread.Sleep(500);

            return Convert.ToInt32(usage.NextValue());
        }
    }
}
