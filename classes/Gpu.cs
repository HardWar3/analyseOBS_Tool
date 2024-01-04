using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;

namespace hardy_analyseOBS.classes
{
    internal class Gpu : Component
    {
        private ManagementObjectSearcher usage = new ManagementObjectSearcher("select * from " +
            "Win32_PerfFormattedData_GPUPerformanceCounters_GPUEngine where Name Like '%engtype_3D'");

        public Gpu() 
        {
            set_intformation("select * from Win32_VideoController");
        }

        public override int get_usage()
        {
            int counter = 0;

            System.Threading.Thread.Sleep(100);

            foreach(ManagementObject item in usage.Get()) 
            {
                counter += Convert.ToInt32(item["UtilizationPercentage"]);
            }

            return counter;
        }
    }
}
