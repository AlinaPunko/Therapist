﻿using System;
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
    /// Логика взаимодействия для EditDoctorForm.xaml
    /// </summary>
    public partial class EditDoctorForm : Window, IEditDoctorView
    {
        public bool Flag = false;
        public EditDoctorForm()
        {
            InitializeComponent();
            this.Presenter = new EditDoctorPresenter(this);
        }
        public EditDoctorPresenter Presenter { get; set; }

        public EditDoctorForm(int doctorId): this()
        {
            if (doctorId == 0)
            {
                this.Presenter.CreateNew();
            }
            else
            {
                this.Presenter.Load(doctorId);
            }
        }
        public EditDoctorForm(bool flag) : this()
        {
            Flag = true;
            this.Presenter.CreateNew(true);
        }

        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            if (Flag == false)
            this.Presenter.Save();
            else
                this.Presenter.Save1();
            //this.Close();
        }
        protected void LoadDoctorById(int doctorId)
        {
            this.Presenter.Load(doctorId);
        }
        #region IEditDoctorView Members

        public string Skills
        {
            get
            {
                return textBoxSkills.Text;
            }
            set
            {
                textBoxSkills.Text = value;
            }
        }

        public string Address
        {
            get
            {
                return textBoxAddress.Text;
            }
            set
            {
                textBoxAddress.Text = value;
            }
        }

        public string Phone
        {
            get
            {
                return textBoxPhone.Text;
            }
            set
            {
                textBoxPhone.Text = value;
            }
        }

        public string DoctorName
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

        public int DoctorId
        {
            set
            {
                labelId.Content = value.ToString();//ContentStringFormat на text
            }
        }

        public string Message
        {
            get
            {
                return labelMessage.ToString();//ContentStringFormat на text
            }
            set
            {
                labelMessage.Content = value;//ContentStringFormat на text
            }
        }

        #endregion
        private void EditDoctorForm_MouseDown(object sender, MouseButtonEventArgs e)
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
