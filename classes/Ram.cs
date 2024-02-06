using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Diagnostics;
using System.Security.Permissions;

namespace hardy_analyseOBS.classes
{
    internal class Ram : Component
    {
        private PerformanceCounter usage = new PerformanceCounter("Memory", "Available Mbytes");

        public Ram()
        {
            set_intformation("select * from Win32_PhysicalMemory");
        }

        public override int get_usage()
        {
            double total = get_total();
            double available = get_available();

            int result = Convert.ToInt32(Math.Round(total - available, 2));

            return result;
        }

        public override void add_peak(int peak = -1)
        {
            if (peak < 0)
            {
                int _peak = get_available_percent();

                _usage_list.Add(_peak);
            }
            else
            {
                _usage_list.Add(peak);
            }
        }

        public int get_usage_percent()
        {
            int usage_percent = 100 - get_available_percent();

            return usage_percent;
        }

        public int get_available()
        {
            float first_ram_usage_value = usage.NextValue();

            double ram_available = usage.NextValue() / 1024; // führt zur korrekten Berechnung (MB to GB(Gibibyte))

            return Convert.ToInt32(ram_available);
        }

        public int get_available_percent()
        {
            double total = get_total();
            double available = get_available();

            int result = Convert.ToInt32(Math.Round(available / (total / 100),2));

            return result;
        }

        public double get_total()
        {
            double total = 0;

            foreach (ManagementObject item in _information.Get())
            {
                total += Convert.ToDouble(item["Capacity"]) / Math.Pow(1024,3); // führt zur korrekten Berechnung (byte to Gibibyte)
            }

            return total;
        }

        public override string get_name()
        {
            string name = "";

            foreach (ManagementObject item in _information.Get())
            {
                // wert kann nicht null sein in diesem Fall
                name = item["ManuFacturer"].ToString()!;
            }

            return name;
        }

        public int get_last_usage_peak()
        {
            if (_usage_list.Count == 0)
            {
                return 0;
            }
            int last_peak = 100 - _usage_list.Last();

            return last_peak;
        }

        public int get_last_available_peak()
        {
            if (_usage_list.Count == 0)            
            {
                return 0;
            }
            int last_available_peak = _usage_list.Last();

            return last_available_peak;
        }

        public override int get_usage_average()
        {
            int count = _count_on_stop - _count_on_start;

            if (count == 0)
            {
                return 0;
            } else if (count > _usage_list.Count || _count_on_stop > _usage_list.Count)
            {
                count = _usage_list.Count - _count_on_start;
            }

            List<int> usage_rang = _usage_list.GetRange(_count_on_start, count);
            int usage = 100 - Convert.ToInt32(usage_rang.Average());
            return usage;
        }
    }
}
