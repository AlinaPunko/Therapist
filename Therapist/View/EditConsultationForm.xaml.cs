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
using Therapist.Data;
using Therapist.Logic;
namespace Therapist.View
{
    /// <summary>
    /// Логика взаимодействия для EditConsultationForm.xaml
    /// </summary>
    public partial class EditConsultationForm : Window, IEditConsultationView
    {
        public EditConsultationForm()
        {
            InitializeComponent();
            this.Presenter = new EditConsultationPresenter(this);
            if (Membership.CurrentUser.RoleID == 2)
            {
                int ID = (int)Membership.CurrentUser.DoctorID;
                Doctor doctor = DoctorDataAccess.GetDoctorById(ID);
                this.DoctorId = doctor.DoctorID;
                this.DoctorName = doctor.Name;
                labelId.Content = doctor.DoctorID.ToString();
                buttonLoadDoctor.IsEnabled = false;
                textBoxDoctorName.Text = doctor.Name;
            }
        }
        private void EditConsultationForm_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        public EditConsultationForm(int consultationId)
          : this()
        {
            if (consultationId == 0)
            {
                this.Presenter.CreateNew();
            }
            else
            {
                this.Presenter.Load(consultationId);
            }

        }

        #region IEditConsultationView Members

        public DateTime ScheduleDate
        {
            get
            {
                return dateTimePickerScheduleDate.SelectedDate.Value;//DisplayDate  SelectedTime.Value
            }
            set
            {
                dateTimePickerScheduleDate.SelectedDate = DateTime.Now;//DisplayDate  SelectedTime= value
            }
        }

        public TimeSpan ScheduleTime
        {
            get
            {
                return dateTimePickerScheduleTime.SelectedTime.Value.TimeOfDay;//DisplayDate
            }
            set
            {
                dateTimePickerScheduleTime.SelectedTime = DateTime.Now;//DisplayDate
            }
        }


        public string Reason
        {
            get
            {
                return textBoxReason.Text;
            }
            set
            {
                textBoxReason.Text = value;
            }
        }

        public string Notes
        {
            get
            {
                return textBoxNotes.Text;
            }
            set
            {
                textBoxNotes.Text = value;
            }
        }


        private int _patientId;
        public int PatientId
        {
            get
            {
                return _patientId;
            }
            set
            {
                _patientId = value;
            }
        }

        public string PatientName
        {
            get
            {
                return textBoxPatientName.Text;
            }
            set
            {
                textBoxPatientName.Text = value;
            }
        }
       

        private int _doctorId;
        public int DoctorId
        {
            get
            {
                return _doctorId;
            }
            set
            {
                _doctorId = value;
            }
        }

        public string DoctorName
        {
            get
            {
                return textBoxDoctorName.Text;
            }
            set
            {
                textBoxDoctorName.Text = value;
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
        public EditConsultationPresenter Presenter { get; set; }

        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            this.Presenter.Save();
        }

        private void buttonLoadPatient_Click(object sender, RoutedEventArgs e)
        {
            var patientsForm = new PatientsForm();
            Patient loadedPatient;
            if (patientsForm.TryChoosePatient(out loadedPatient))
            {
                this.PatientId = loadedPatient.PatientID;
                this.PatientName = loadedPatient.Name;
            }
        }
        #region IEditConsultationView Members

        public int ConsultationId
        {
            get
            {
                return Int32.Parse(this.labelId.ContentStringFormat);
            }
            set
            {
                this.labelId.Content = value.ToString();
            }
        }

        #endregion

        private void buttonLoadDoctor_Click(object sender, RoutedEventArgs e)
        {
            var doctorssForm = new DoctorsForm();
            Doctor loadedDoctor;
            if (doctorssForm.TryChooseDoctor(out loadedDoctor))
            {
                this.DoctorId = loadedDoctor.DoctorID;
                this.DoctorName = loadedDoctor.Name;
            }
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
