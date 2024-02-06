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
using System.Windows.Threading;

namespace analyseOBS_Tool
{
    /// <summary>
    /// Interaktionslogik für AnalyseTool.xaml
    /// </summary>
    public partial class AnalyseTool : Window
    {
        private Cpu cpu = new Cpu();
        private Ram ram = new Ram();
        private Gpu gpu = new Gpu();

        public AnalyseTool()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1000);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        void timer_Tick(object? sender, EventArgs e)
        {
            Task task_all_add_peak = new Task(() => all_add_peak());
            task_all_add_peak.Start();

            cpu_usage_bar.Value = cpu.get_last_peak();
            cpu_usage_label.Content = String.Format("CPU:   {0:00} %", cpu_usage_bar.Value);
            ram_usage_bar.Value = ram.get_last_usage_peak();
            ram_usage_label.Content = String.Format("RAM:  {0:00} %", ram_usage_bar.Value);
            gpu_usage_bar.Value = gpu.get_last_peak();
            gpu_usage_label.Content = String.Format("GPU:   {0:00} %", gpu_usage_bar.Value);
        }

        public void all_add_peak()
        {      
            cpu.add_peak();
            ram.add_peak();
            gpu.add_peak();
        }

        private void start_stop_button_Click(object sender, RoutedEventArgs e)
        {
            if (start_stop_button.Content.Equals("Start"))
            {
                start_stop_button.Content = "Stop";
                start_stop_button.Background = Brushes.DarkRed;

                cpu.set_count_on_start();
                ram.set_count_on_start();
                gpu.set_count_on_start();
            }
            else if (start_stop_button.Content.Equals("Stop"))
            {
                start_stop_button.Content = "Start";
                start_stop_button.Background = Brushes.DarkGreen;

                cpu.set_count_on_stop();
                ram.set_count_on_stop();
                gpu.set_count_on_stop();

                //antwort fenster
                Analyzed analyzed = new Analyzed();
                analyzed.show_analyze(cpu.get_usage_average()
                                     ,ram.get_usage_average()
                                     ,gpu.get_usage_average());
            }
        }
    }
}
