using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MazeSolver
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Bitmap bm = new Bitmap(100, 100);
        List<Task<Types.ImageResult>> tasks = new List<Task<Types.ImageResult>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            INM.Focus();
        }

        private void INM_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if(ofd.ShowDialog().HasValue)
            {
                System.Drawing.Color white = System.Drawing.Color.FromArgb(255, 255, 255);
                if(bm.Height == 100 && bm.Width == 100) // && bm.GetPixel(0, 0) == white && bm.GetPixel(100, 100) == white)
                {
                    bm = new Bitmap(ofd.FileName);
                    StartMS.IsEnabled = true;
                } else
                {
                    MessageBox.Show("The image you imported does not meet the requirements of a maze. (100x100 & white at (0,0) and (100,100)).\n\n" +
                        "Please import an image that matches these requirements to continue with Maze Solver.");
                }
            }
        }

        private void StartMS_Click(object sender, RoutedEventArgs e)
        {
            StartMS.IsEnabled = false;
            StopMS.IsEnabled = true;

            for (int i = 0; i < 100; i++)
            {
                Task<Types.ImageResult> task = new Task<Types.ImageResult>(new Types.Solver().Solve(bm));

            }
        }

        private void StopMS_Click(object sender, RoutedEventArgs e)
        {
            tasks.ForEach(task =>
            {
                task.Dispose();
            });
        }

        public BitmapImage ToBitmapImage(Bitmap bitmap) // https://stackoverflow.com/a/23831231/16426057
        {
            using (var memory = new MemoryStream())
            {
                bitmap.Save(memory, ImageFormat.Png);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();

                return bitmapImage;
            }
        }

        public void AddImageToList(Bitmap bitmap)
        {
            Button button = new Button();
            button.Content = new System.Windows.Controls.Image() { Source = ToBitmapImage(bitmap) };
            button.Width = 100;
            button.Height = 100;
            Images.Children.Add(button);
        }
    }
}
