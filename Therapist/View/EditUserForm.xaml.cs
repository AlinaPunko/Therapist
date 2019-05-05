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
using Therapist.Logic;

namespace Therapist.View
{
    /// <summary>
    /// Логика взаимодействия для EditUserForm.xaml
    /// </summary>
    public partial class EditUserForm : Window, IEditUserView
    {
        public EditUserPresenter Presenter { get; set; }
        public EditUserForm()
        {
            InitializeComponent();
            this.Presenter = new EditUserPresenter(this);
        }

        public EditUserForm(int userId)
            : this()
        {
            if (userId == 0)
            {
                this.Presenter.CreateNew();
            }
            else
            {
                this.Presenter.Load(userId);
            }
        }
        public EditUserForm(int doctorId, bool flag ) : this()
        {
                this.Presenter.CreateNew(doctorId);
        }
        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #region IEditUserView Members

        public int UserId
        {
            set
            {
                this.labelId.Content = value.ToString();
            }
        }

        public string UserName
        {
            get
            {
                return textBoxName.Text;
            }
            set
            {
                textBoxName.Text = value;
            }
        }

        public string Password
        {
            get
            {
                return Password1.Password;
            }
            set
            {
                Password1.Password = value;
            }
        }

        public string ConfirmPassword
        {
            get
            {
                return Password2.Password;
            }
            set
            {
                Password2.Password = value;
            }
        }

        public string Message
        {
            set
            {
                MessageBox.Show(value);
            }
        }

        #endregion

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            this.Presenter.Save();
        }
        private void EditUserForm_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
