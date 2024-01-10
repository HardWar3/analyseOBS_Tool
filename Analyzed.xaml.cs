using hardy_analyseOBS.classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace analyseOBS_Tool
{
    /// <summary>
    /// Interaktionslogik für Analyzed.xaml
    /// </summary>
    public partial class Analyzed : Window
    {
        public Analyzed()
        {
            InitializeComponent();
        }

        public void show_analyze(int cpu_avg, int ram_avg, int gpu_avg)
        {
            cpu_textblock.Text = String.Format("CPU: {0:00} %", cpu_avg);
            ram_textblock.Text = String.Format("RAM: {0:00} %", ram_avg);
            gpu_textblock.Text = String.Format("GPU: {0:00} %", gpu_avg);

            cpu_textblock.Foreground = check_avg_value(cpu_avg);
            ram_textblock.Foreground = check_avg_value(ram_avg);
            gpu_textblock.Foreground = check_avg_value(gpu_avg);

            // green for good
            // yellow for naja
            // red for bad idea

            // done
            // green is between 0 and 50
            // yellow is between 50 and 70
            // red is between 70 and 100

            // if cpu lower than gpu 
            // cpu rendering recommended
            // otherwise gpu rendering recommended

            // ram alone check

            Show();
        }

        public Brush check_avg_value(int avg)
        {
            switch (avg)
            {
                case <= 50:
                    return Brushes.Green;
                case <= 70:
                    return Brushes.Orange;
                case < 100:
                    return Brushes.Red;
                default:
                    break;
            }
            return Brushes.Pink;
        }
    }
}
