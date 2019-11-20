using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
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

namespace Therapist.View
{
    /// <summary>
    /// Логика взаимодействия для EditPatientForm.xaml
    /// </summary>
    public partial class EditPatientForm : Window, IEditPatientView
    {
        public EditPatientPresenter Presenter { get; set; }
        public EditPatientForm()
        {
            InitializeComponent();
            this.Presenter = new EditPatientPresenter(this);
        }
        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void EditPatientForm_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        public EditPatientForm(int patientId) : this()
        {
            if (patientId == 0)
            {
                this.Presenter.CreateNew();
            }
            else
            {
                this.Presenter.Load(patientId);
                this.Presenter.LoadVisit();
                this.Presenter.LoadConsultations();
            }
        }

        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        protected void LoadPatientById(int patientId)
        {
            this.Presenter.Load(patientId);
        }

        #region IEditPatientView Members


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

        public DateTime Birthdate
        {
            get
            {
                return dateTimePickerBirthdate.SelectedDate.Value;
            }
            set
            {
                dateTimePickerBirthdate.SelectedDate = value;//DisplayDate
            }
        }


        public string PatientName
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

        public int PatientId
        {
            get
            {
                int patientId = 0;
                if (Int32.TryParse(labelId.ContentStringFormat, out patientId))
                {
                    return patientId;
                }

                return 0;

            }
            set
            {
                labelId.Content = value.ToString();
            }
        }

        public string Message
        {
            get
            {
                return labelMessage.ContentStringFormat;
            }
            set
            {
                labelMessage.Content = value;
            }
        }

        public IEnumerable<Data.Visit> Visits
        {
            set
            {
                dataGridViewVisits.AutoGenerateColumns = false;
                dataGridViewVisits.DataContext = value;
            }

        }

        public IEnumerable<Data.Consultation> Consultations
        {
            set
            {
                dataGridViewConsultations.AutoGenerateColumns = false;
                dataGridViewConsultations.DataContext = value;
            }
        }

        #endregion

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            this.Presenter.Save();
        }
        #region COnsultations operations

        private Consultation GetSelectedConsultation()
        {
            var row = this.dataGridViewConsultations.SelectedItem;//currentrow было вместо колумна
            if (row == null)
            {
                return null;
            }

            var consultation = (Consultation)row;//row.DataBoundItem;
            return consultation;
        }
        private void buttonAddConsultation_Click(object sender, RoutedEventArgs e)
        {
            var editConsultationForm = new EditConsultationForm(0);
            editConsultationForm.ShowDialog();
            this.Presenter.LoadConsultations();
        }

        private void buttonEditConsultation_Click(object sender, RoutedEventArgs e)
        {
            var selectedConsultation = this.GetSelectedConsultation();
            if (selectedConsultation == null)
            {
                return;
            }

            int selectedConsultationId = selectedConsultation.ConsultationID;
            var editConsultationForm = new EditConsultationForm(selectedConsultationId);
            editConsultationForm.ShowDialog();
            this.Presenter.LoadConsultations();
        }

        private void buttonDeleteConsultation_Click(object sender, RoutedEventArgs e)
        {
            var selectedConsultation = this.GetSelectedConsultation();
            if (selectedConsultation == null)
            {
                return;
            }

            if (MessageBox.Show("Вы действительно хотите удалить эту консультацию?", "Подтверждение удаления", MessageBoxButton.OKCancel) != MessageBoxResult.OK)//messageboxresult System.Windows.Forms.DialogResult
            {
                return;
            }

            try
            {
                int consultationId = selectedConsultation.ConsultationID;
                ConsultationDataAccess.DeleteConsultationById(consultationId);
                this.Presenter.LoadConsultations();
            }
            catch (Exception ex)
            {
                string errorMessage = string.Format("При удалении объекта произошла ошибка!\n {0}", ex.Message);
                this.Message = errorMessage;
            }
        }
        #endregion

        #region Visits operations
        private Visit GetSelectedVisit()
        {
            var row = this.dataGridViewVisits.SelectedItem;//currentrow было вместо колумна
            if (row == null)
            {
                return null;
            }

            var visit = (Visit)row;//row.DataBoundItem;
            return visit;
        }

        private void buttonAddVisit_Click(object sender, RoutedEventArgs e)
        {
            var editVisitForm = new EditVisitForm(0);
            editVisitForm.ShowDialog();
            this.Presenter.LoadVisit();
        }

        private void buttonEditVisit_Click(object sender, RoutedEventArgs e)
        {
            var selectedVisit = this.GetSelectedVisit();
            if (selectedVisit == null)
            {
                return;
            }

            int selectedVisitId = selectedVisit.VisitID;
            var editVisitForm = new EditVisitForm(selectedVisitId);
            editVisitForm.ShowDialog();
            this.Presenter.LoadVisit();
        }

        private void buttonDeleteVisit_Click(object sender, RoutedEventArgs e)
        {
            var selectedVisit = this.GetSelectedVisit();
            if (selectedVisit == null)
            {
                return;
            }

            if (MessageBox.Show("Вы действительно хотите удалить эту консультацию?", "Подтверждение удаления", MessageBoxButton.OKCancel) != MessageBoxResult.OK)//messageboxresult System.Windows.Forms.DialogResult
            {
                return;
            }

            try
            {
                int visitId = selectedVisit.VisitID;
                VisitsDataAccess.DeleteVisitById(visitId);
                this.Presenter.LoadVisit();
            }
            catch (Exception ex)
            {
                string errorMessage = string.Format("При удалении объекта произошла ошибка!\n {0}", ex.Message);
                this.Message = errorMessage;
            }
        }

        #endregion

        private void UIElement_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            var editVisitForm = new EditVisitForm(0);
            editVisitForm.ShowDialog();
            this.Presenter.LoadVisit();
            this.Presenter.LoadConsultations();
        }

        private void dataGridViewConsultations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void buttonPrint_Click(object sender, RoutedEventArgs e)
        {

            string Text = "Имя " + textBoxName.Text.ToString() + "\r\nДата рождения " + dateTimePickerBirthdate.Text.ToString() + "\r\nТелефон "
                     + textBoxPhone.Text.ToString() + "\r\nАдрес " + textBoxAddress.Text.ToString() + "\r\n";
            Text += "ВИЗИТЫ\r\n";
            foreach (Visit v in dataGridViewVisits.Items)
            {
                Text += "Диагноз " + v.Reason + " cимптомы " + v.Notes + " дата " + v.VisitDate.ToString() + " доктор " + v.DoctorName + "\r\n";
            }
            Text += "КОНСУЛЬТАЦИИ\r\n";
            foreach (Consultation c in dataGridViewConsultations.Items)
            {
                Text += "Причина " + c.Reason + " дата " + c.ScheduleDate.ToString() + " время " + c.ScheduleTime.ToString() + " доктор " + c.DoctorName + "\r\n";
            }
            PrintDocument p = new PrintDocument();
            p.PrintPage += delegate (object sender1, PrintPageEventArgs e1)
            {
                e1.Graphics.DrawString(Text, new Font("Times New Roman", 12), new SolidBrush(System.Drawing.Color.Black), new RectangleF(0, 0, p.DefaultPageSettings.PrintableArea.Width, p.DefaultPageSettings.PrintableArea.Height));
            };
            try
            {
                p.Print();
            }
            catch (Exception ex)
            {
                throw new Exception("Exception Occured While Printing", ex);
            }
        }

    }
}
