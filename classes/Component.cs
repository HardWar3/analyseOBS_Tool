using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace hardy_analyseOBS.classes
{
    internal abstract class Component
    {
        protected ManagementObjectSearcher _information = new ManagementObjectSearcher();

        protected List<int> _usage_list = new List<int>();

        protected int _count_on_start = 0;
        protected int _count_on_stop = 0;

        protected Component() 
        {
            set_intformation("select * from Win32_Processor");
        }

        public void set_intformation(string query_String)
        {
            _information = new ManagementObjectSearcher(query_String);
        }

        public abstract int get_usage();

        public virtual string get_name()
        {
            string name = "";
            foreach (ManagementObject item in _information.Get())
            {
                // wert kann nicht null sein nicht in diesem Fall
                name = item["name"].ToString();
            }

            return name;
        }

        public virtual void add_peak(int peak = -1)
        {
            if(peak < 0)
            {
                int _peak = get_usage();
                _usage_list.Add(_peak);
            } else
            {
                _usage_list.Add(peak);
            }
        }

        public virtual List<int> get_usage_list()
        {
            return _usage_list;
        }

        public virtual int get_last_peak()
        {
            if (_usage_list.Count == 0)
            {
                return 0;
            }
            int last_peak = _usage_list.Last();
            return last_peak;
        }

        public virtual int get_usage_average()
        {
            int count = _count_on_stop - _count_on_start;
            if (count <= 0)
            {
                return 0;
            } else if (count > _usage_list.Count || _count_on_stop > _usage_list.Count)
            {
                count = _usage_list.Count - _count_on_start;
            }
            List<int> usage_range = _usage_list.GetRange(_count_on_start, count);
            return Convert.ToInt32(usage_range.Average());
        }

        public virtual void set_count_on_start()
        {
            _count_on_start = _usage_list.Count();
        }

        public virtual int get_count_on_start()
        {
            return _count_on_start;
        }

        public virtual void set_count_on_stop()
        {
            _count_on_stop = _usage_list.Count();
        }

        public virtual int get_count_on_stop()
        {
            return _count_on_stop;
        }

    }
}
