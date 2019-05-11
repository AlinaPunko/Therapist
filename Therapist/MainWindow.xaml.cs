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
using Therapist.Data;
using Therapist.Logic;
using Therapist.Models;
using Therapist.Presentation;
using Therapist.View;

namespace Therapist
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //Date.Text = DateTime.Now.Date.ToString("dd.MM.yyy");
            //Time.Text = DateTime.Now.TimeOfDay.ToString();
            UserName.Text = Membership.CurrentUser.UserName.ToString();
            //Time.Content = DateTime.Now.TimeOfDay;
            if (Membership.CurrentUser.UserID == 2)
            {
                ItemDoctors.Visibility = Visibility.Collapsed;
                ItemAddDoctor.Visibility = Visibility.Collapsed;
                ItemAddAdmin.Visibility = Visibility.Collapsed;
                ItemAddPatient.Visibility = Visibility.Collapsed;
            }
        
        }

        private void MainForm_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Visible;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            ButtonOpenMenu.Visibility = Visibility.Visible;
        }


        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "ItemDoctors":
                    {
                        DoctorsForm doctorsForm = new DoctorsForm();
                        //doctorsForm.Topmost = true;
                        doctorsForm.ShowDialog();
                        break;
                    }
                case "ItemPatients":
                    {
                        PatientsForm patientsForm = new PatientsForm();
                       // patientsForm.Topmost = true;
                        patientsForm.ShowDialog();
                        break;
                    }
                case "ItemVisits":
                    {
                        VisitsForm visitsForm = new VisitsForm();
                       // visitsForm.Topmost = true;
                        visitsForm.ShowDialog();
                        break;
                    }
                case "ItemConsultations":
                    {
                        ConsultationsForm consultationsForm = new ConsultationsForm();
                        //consultationsForm.Topmost = true;
                        consultationsForm.ShowDialog();
                        break;
                    }
                case "ItemAddDoctor":
                    {
                        EditDoctorForm newForm = new EditDoctorForm(true);
                       // newForm.Topmost = true;
                        newForm.ShowDialog();
                        break;
                    }
                case "ItemAddAdmin":
                    {
                        EditUserForm newForm = new EditUserForm(0);
                        // newForm.Topmost = true;
                        newForm.ShowDialog();
                        break;
                    }
                case "ItemAddPatient":
                    {
                        EditPatientForm newForm = new EditPatientForm(0);
                        // newForm.Topmost = true;
                        newForm.ShowDialog();
                        break;
                    }
                default:
                    break;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Membership.LogOutUser();
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Window1 window = new Window1();
            window.ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
    
