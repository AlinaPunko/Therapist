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
    /// Логика взаимодействия для PatientsForm.xaml
    /// </summary>
    public partial class PatientsForm : Window, IPatientsView
    {
        public PatientsForm()
        {
            InitializeComponent();
            this.Presenter = new PatientsPresenter(this);
            this.Presenter.LoadAllPatients();
        }
        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.Presenter.LoadPatientsByCriterias();
        }
        public PatientsPresenter Presenter { get; set; }
        public string NameSearch
        {
            get
            {
                return textBoxName.Text;
            }
            set
            {
                this.textBoxName.Text = value;
            }
        }
        private void buttonSearch_Click(object sender, RoutedEventArgs e)
        {
            //string name = textBoxName.Text;
            this.Presenter.LoadPatientsByCriterias();//name);
        }
        private void PatientsForm_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        private void buttonEdit_Click(object sender, RoutedEventArgs e)
        {
            var patient = GetSelectedPatient();
            if (patient == null)
            {
                return;
            }

            int patientId = patient.PatientID;
            EditPatientForm patientForm = new EditPatientForm(patientId);
            patientForm.ShowDialog();
        }
        private Patient GetSelectedPatient()
        {
            var row = this.dataGridViewResult.SelectedItem;//currentrow было вместо колумна
            if (row == null)
            {
                return null;
            }

            var patient = (Patient)row;//row.DataBoundItem;
            return patient;
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            EditPatientForm editpatientForm = new EditPatientForm(0);
            editpatientForm.ShowDialog();
            this.Presenter.LoadPatientsByCriterias();
        }

        #region IPatientsView Members

        public IEnumerable<Patient> Patients
        {
            set
            {
                this.dataGridViewResult.AutoGenerateColumns = false;
                this.dataGridViewResult.DataContext = value;//datasource был
            }
        }

        public string Message
        {
            set
            {

                Message message = new Message(value);
                message.Show();
            }
        }

        #endregion

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            var row = this.dataGridViewResult.SelectedItem;//currentrow было вместо колумна
            if (row == null)
            {
                return;
            }

            if (MessageBox.Show("Вы действительно хотите удалить эту консультацию ? ", "Подтверждение удаления", MessageBoxButton.OKCancel) != MessageBoxResult.OK)//messageboxresult System.Windows.Forms.DialogResult
            {
                return;
            }

            try
            {
                var patient = (Patient)row;//row.DataBoundItem;
                int patientId = patient.PatientID;
                PatientsDataAccess.DeletePatientById(patientId);

                this.Presenter.LoadAllPatients();
            }
            catch (Exception ex)
            {
                string errorMessage = string.Format("При удалении объекта произошла ошибка!\n {0}", ex.Message);
                this.Message = errorMessage;
            }
        }

        private void buttonChoose_Click(object sender, RoutedEventArgs e)
        {
            //this.DialogResult = DialogResult;//.OK
            this.Close();
        }

        public bool TryChoosePatient(out Patient patient)
        {
            patient = null;
            this.panelButtons.Visibility = Visibility.Hidden;//cкрывает кнопку false
            this.panelChooseButtons.Visibility = Visibility.Visible;//visible видимый
            this.ShowDialog();

            if (this.DialogResult != DialogResult)//.OK
            {
                return false;
            }

            var selectedPatient = GetSelectedPatient();
            if (selectedPatient == null)
            {
                return false;
            }

            patient = selectedPatient;

            return true;
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            //this.DialogResult = DialogResult;//.Cancel
            this.Close();
        }

        private void dataGridViewResult_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void CommandBinding1_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }
        private void textBoxName_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Presenter.LoadPatientsByCriterias();
        }
    }
    
}
