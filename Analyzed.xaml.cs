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
        string[,,] twitch_broadcasting_specs = { { { "1080p 60fps / 30fps", "1920x1080", "6000kbps / 4500kbps", "CBR", "60 fps / 30 fps", "2 seconds", "veryfast <-> medium", "Main / High" },
                                                   { "720p 60fps / 30fps", "1280x720", "4500kbps / 3000kbps", "CBR", "60 fps / 30 fps", "2 seconds", "very <-> medium", "Main / High" } },
                                                 { { "1080p 60fps / 30fps", "1920x1080", "6000kbps / 4500kbps", "CBR", "60 fps / 30 fps", "2 seconds", "Quality", "2"},
                                                   { "720p 60fps / 30fps", "1280x720", "4500kbps / 3000kbps", "CBR", "60 fps / 30 fps", "2 seconds", "Quality", "2"} } };
        string[] label_names = { "", "Resolution: ", "Bitrate: ", "Rate Control: ", "Framerate: ", "Keyframe Interval: ", "Preset: "};
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

        public void add_broadcasting_infos(int cpu_0_gpu_1)
        {
            int index = 0;
            //<TextBlock Background="BlueViolet" Foreground="White" Height="Auto" Text="1080p 60fps / 30fps" FontSize="38"/>
            switch(cpu_0_gpu_1)
            {
                case 0:
                    break;
                case 1:
                    index = 1;
                    break;
                default:
                    return;
            }
            for (int i = 0; i < 2; i++)
            {

                TextBlock head = new TextBlock();
                head.Background = Brushes.BlueViolet;
                head.Foreground = Brushes.White;
                head.Text = twitch_broadcasting_specs[index,i,0];
                head.FontSize = 38;
                StackPanel specs_groupPanel = new StackPanel();
                specs_groupPanel.Margin = new Thickness(8);
                for (int j = 1; j < 7; j++)
                {
                    StackPanel stackPanel = new StackPanel();
                    stackPanel.Orientation = Orientation.Horizontal;
                    specs_groupPanel.Children.Add(stackPanel);

                    Label label = new Label();
                    label.FontWeight = FontWeights.Bold;
                    label.FontSize = 16;
                    label.VerticalContentAlignment = VerticalAlignment.Center;
                    label.Padding = new Thickness(0);

                    TextBlock textBlock = new TextBlock();
                    textBlock.FontSize = 16;

                    switch (j)
                    {
                        case <= 6:
                            label.Content = label_names[j];
                            textBlock.Text = twitch_broadcasting_specs[index,i,j];
                            break;
                        case 7:
                            textBlock.Text = twitch_broadcasting_specs[index,i,j];
                            if (i == 0)
                            {
                                label.Content = "Profile: ";
                            }
                            else
                            {
                                label.Content = "B-frames: ";
                            }
                            break;
                        default:
                            break;
                    }

                    stackPanel.Children.Add(label);
                    stackPanel.Children.Add(textBlock);
                }

                broadcasting_specs.Children.Add(head);
                broadcasting_specs.Children.Add(specs_groupPanel);
            }
        }
    }
}