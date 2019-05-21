using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using Therapist.Data;
using Therapist.Logic;
using Therapist.Models;
using Therapist.Presentation;
using Therapist.View;

namespace Therapist
{
    /// <summary>
    /// Логика взаимодействия для LoginForm.xaml
    /// </summary>
    public partial class LoginForm : Window
    {
        public LoginForm()
        {
            InitializeComponent();
            
        }
        private void CommandBinding1_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            TryLogin();
        }
        private void LoginForm_MouseDown ( object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            TryLogin();
        }

        private void TryLogin()
        {
            string username = UserName.Text;
            string password = UserPassword.Password;

            string loginResultMessage = string.Empty;
            if (TryLogin(username, password, out loginResultMessage))
            {
                MainWindow a = new MainWindow();
                a.Show();
                this.Close();
            }
            else
            {
                UserName.Text = null;
                UserPassword.Password = null;
                labelMessage.Content = loginResultMessage;
                labelMessage.Foreground = Brushes.Red;
                labelMessage.BorderBrush = Brushes.Red;
            }
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {

            this.Close();
        }

        private bool TryLogin(string username, string password, out string message)
        {
            message = string.Empty;
            bool isLoginDetailsValid = Membership.IsValidLoginDetails(username, password);
            if (isLoginDetailsValid == false)
            {
                message = "Неверное имя или пароль!";
                return false;
            }

            bool isLoginSuccessfull = Membership.ValidateAndLogin(username, password);
            if (isLoginSuccessfull == false)
            {
                message = "Ошибка при входе. Обратитесь к администратору!";
                return false;
            }
            
            return true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window1 window = new Window1();
            window.ShowDialog();
        }
    }
    public class NewCustomCommand
    {
        private static RoutedUICommand enterCommand;
        static NewCustomCommand()
        {
            InputGestureCollection inputs = new InputGestureCollection();
            inputs.Add(new KeyGesture(Key.Enter));
            enterCommand = new RoutedUICommand("PNV1", "PNV1", typeof(NewCustomCommand), inputs);
           
        }
        public static RoutedUICommand EnterCommand
        {
            get { return enterCommand; }
        }
       
    }
}
