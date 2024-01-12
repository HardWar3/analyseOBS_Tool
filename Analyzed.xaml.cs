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
        string[,,] twitch_broadcasting_specs = { { { "x264 (CPU)", "1080p 60fps / 30fps", "1920x1080", "6000kbps / 4500kbps", "CBR", "60 fps / 30 fps", "2 seconds", "veryfast <-> medium", "Main / High" },
                                                   { "x264 (CPU)", "720p 60fps / 30fps", "1280x720", "4500kbps / 3000kbps", "CBR", "60 fps / 30 fps", "2 seconds", "very <-> medium", "Main / High" } },
                                                 { { "NVENC/AMD H.264 (GPU)", "1080p 60fps / 30fps", "1920x1080", "6000kbps / 4500kbps", "CBR", "60 fps / 30 fps", "2 seconds", "Quality", "2"},
                                                   { "NVENC/AMD H.264 (GPU)", "720p 60fps / 30fps", "1280x720", "4500kbps / 3000kbps", "CBR", "60 fps / 30 fps", "2 seconds", "Quality", "2"} } };
        string[] label_names = { "Resolution: ", "Bitrate: ", "Rate Control: ", "Framerate: ", "Keyframe Interval: ", "Preset: "};
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

            switch (gpu_avg)
            {
                case < 80:
                    add_broadcasting_infos(1);
                    break;
                case > 80:
                    if (cpu_avg + 20 < 85)
                    {
                        add_broadcasting_infos(0);
                    }
                    add_broadcasting_infos(1);
                    break;
                default:
                    break;
            }

            Show();
        }

        public Brush check_avg_value(int avg)
        {
            switch (avg)
            {
                case <= 50:
                    return Brushes.Green;
                case <= 80:
                    return Brushes.Orange;
                default:
                    return Brushes.Red;
            }
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
            for (int i = 0; i < twitch_broadcasting_specs.GetLength(1); i++)
            {
                TextBlock encoderHead = new TextBlock();
                encoderHead.Background = Brushes.BlueViolet;
                encoderHead.Foreground = Brushes.White;
                encoderHead.Text = twitch_broadcasting_specs[index, i, 0];
                encoderHead.FontSize = 28;

                TextBlock head = new TextBlock();
                head.Background = Brushes.BlueViolet;
                head.Foreground = Brushes.White;
                head.Text = twitch_broadcasting_specs[index, i, 1];
                head.FontSize = 38;
                StackPanel specs_groupPanel = new StackPanel();
                specs_groupPanel.Margin = new Thickness(8);
                for (int j = 2; j < twitch_broadcasting_specs.GetLength(2); j++)
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
                        case <= 7:
                            label.Content = label_names[j - 2];
                            textBlock.Text = twitch_broadcasting_specs[index,i,j];
                            break;
                        case 8:
                            textBlock.Text = twitch_broadcasting_specs[index,i,j];
                            if (index == 0)
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

                broadcasting_specs.Children.Add(encoderHead);
                broadcasting_specs.Children.Add(head);
                broadcasting_specs.Children.Add(specs_groupPanel);
            }
        }
    }
}