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

namespace Therapist.Presentation
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Main.Background = Brushes.Aquamarine;
            Main.Content = new Page2();
        }
        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {

            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            Main.Background = Brushes.Aquamarine;
            Main.Content = new Page4();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Main.Background = Brushes.Aquamarine;
            Main.Content = new Page5();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Main.Background = Brushes.Aquamarine;
            Main.Content = new Page6();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Button_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            ImageBrush myBrush = new ImageBrush();
            Image image = new Image();
            image.Source = new BitmapImage(
                new Uri(
                   "pack://application:,,,/Picture/cat.jpg"));
            myBrush.ImageSource = image.Source;
            Grid grid = new Grid();
            Main.Background = myBrush;
            Main.Content = null;
        }
    }
}
