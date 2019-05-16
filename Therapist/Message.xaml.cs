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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Therapist
{
    /// <summary>
    /// Логика взаимодействия для Message.xaml
    /// </summary>
    public partial class Message : UserControl
    {
        public Message()
        {
            InitializeComponent();
        }
        public Message(string content) : this()
        {
            Content.Text = content;
        }
        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            Window window = this;
            window.Close();
        }

        public static implicit operator Window(Message v)
        {
            throw new NotImplementedException();
        }
    }
}
