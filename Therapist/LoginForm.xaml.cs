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
            foreach (MenuItem item in Languages.Items)
            {
                string name = item.Name;
                item.Height = 25;
                item.Width = 50;
                item.ToolTip = name;
                item.Margin = new Thickness(10, 0, 0, 0);
                item.Background = new ImageBrush
                {
                    ImageSource = BitmapFrame.Create(new Uri(GetLanguageDirectory() + $"\\{name}.png", UriKind.Relative)),
                    Opacity = 0.7
                };
            }
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
        //#region локализация
        //private void OnLanguageChange(object sender, RoutedEventArgs e)
        //{
        //    MenuItem selectedLang = sender as MenuItem;
        //    string lang = selectedLang.Name;
        //    DirectoryInfo directory = new DirectoryInfo(GetLanguageDirectory() + "/" + lang);
        //    try
        //    {
        //        FileInfo[] dictionaries = directory.GetFiles();
        //        //Application.Current.Resources.Clear();
        //        foreach (FileInfo dictionary in dictionaries)
        //        {
        //            int index = dictionary.FullName.IndexOf($"Languages\\{lang}");
        //            string resourcePath = dictionary.FullName.Substring(index);
        //            var uri = new Uri(resourcePath, UriKind.Relative);
        //            ResourceDictionary resource = Application.LoadComponent(uri) as ResourceDictionary;
        //            Application.Current.Resources.MergedDictionaries.Add(resource);
        //        }
        //    }
        //    catch (Exception error)
        //    {
        //        MessageBox.Show(error.Message);
        //    }
        //}
        //private string GetLanguageDirectory()
        //{
        //    DirectoryInfo directory = new DirectoryInfo(Directory.GetCurrentDirectory());
        //    return directory.Parent.Parent.FullName + "\\Languages";
        //}

        //#endregion
        private void TryLogin()
        {
            string username = UserName.Text;
            string password = UserPassword.Password;

            string loginResultMessage = string.Empty;
            if (TryLogin(username, password, out loginResultMessage))
            {
                EditDoctorForm a = new EditDoctorForm();
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
    }
}
