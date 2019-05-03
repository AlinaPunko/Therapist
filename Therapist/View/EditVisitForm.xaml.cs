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
    /// Логика взаимодействия для EditVisitForm.xaml
    /// </summary>
    public partial class EditVisitForm : Window, IEditVisitView
    {
        public EditVisitForm()
        {
            InitializeComponent();
            this.Presenter = new EditVisitPresenter(this);
        }
        private void EditVisitForm_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        public EditVisitForm(int visitId)
            : this()
        {
            if (visitId == 0)
            {
                this.Presenter.CreateNew();
            }
            else
            {
                this.Presenter.Load(visitId);
            }

        }

        public EditVisitPresenter Presenter { get; set; }

        #region IEditVisitView Members

        public int VisitId
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

        public string Reason
        {
            get
            {
                return this.textBoxDiagnosis.Text;
            }
            set
            {
                this.textBoxDiagnosis.Text = value;
            }
        }

        public string Prescription
        {
            get
            {
                return this.textBoxPrescription.Text;
            }
            set
            {
                this.textBoxPrescription.Text = value;
            }
        }

        public string Notes
        {
            get
            {
                return textBoxSymptoms.Text;
            }
            set
            {
                textBoxSymptoms.Text = value;
            }
        }

        public DateTime VisitDate
        {
            get
            {

                return this.dateTimePickerVisitDate.DisplayDate;//DisplayDate
            }
            set
            {
                this.dateTimePickerVisitDate.SelectedDate = value;//DisplayDate
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
