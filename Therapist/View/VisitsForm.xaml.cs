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
using Therapist;
using Therapist.Logic;

namespace Therapist.View
{
    /// <summary>
    /// Логика взаимодействия для VisitsForm.xaml
    /// </summary>
    public partial class VisitsForm : Window, IVisitsView
    {
        public VisitsForm()
        {
            InitializeComponent();
            this.Presenter = new VisitsPresenter(this);
            this.Presenter.LoadVisitsByCriterias();
        }
        public VisitsPresenter Presenter { get; set; }

        #region IVisitsView Members
        public DateTime? VisitDateFromCriteria
        {
            get
            {
                return this.dateTimePickerFrom.SelectedDate.Value;//DisplayDate
            }
            set
            {
                var newValue = value;
                if (newValue.HasValue)
                {
                    this.dateTimePickerFrom.SelectedDate = value;//DisplayDate
                }
            }
        }

        public DateTime? VisitDateToCriteria
        {
            get
            {
                return this.dateTimePickerTo.SelectedDate.Value;//DisplayDate
            }
            set
            {
                var newValue = value;
                if (newValue.HasValue)
                {
                    this.dateTimePickerTo.SelectedDate = value;//DisplayDate
                }
            }
        }
        public IEnumerable<Data.Visit> Visits
        {
            set
            {
                dataGridViewResult.AutoGenerateColumns = false;
                dataGridViewResult.DataContext = value;//datasource был
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

        public string ReasonSearch
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

        #endregion

        private void buttonSearch_Click(object sender, RoutedEventArgs e)
        {
            this.Presenter.LoadVisitsByCriterias();
        }

        private Visit GetSelectedVisit()
        {
            var row = this.dataGridViewResult.SelectedItem;//currentrow было вместо колумна
            if (row == null)
            {
                return null;
            }

            var visit = (Visit)row;//row.DataBoundItem;
            return visit;
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            var editVisitForm = new EditVisitForm(0);
            editVisitForm.ShowDialog();
            this.Presenter.LoadVisitsByCriterias();
        }
        private void VisitsForm_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        private void buttonEdit_Click(object sender, RoutedEventArgs e)
        {
            var selectedVisit = this.GetSelectedVisit();
            if (selectedVisit == null)
            {
                return;
            }

            int selectedVisitId = selectedVisit.VisitID;
            var editVisitForm = new EditVisitForm(selectedVisitId);
            editVisitForm.ShowDialog();
            this.Presenter.LoadVisitsByCriterias();
        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            var selectedVisit = this.GetSelectedVisit();
            if (selectedVisit == null)
            {
                return;
            }

            if (MessageBox.Show("Вы действительно хотите удалить этот визит?", "Подтверждение удаления", MessageBoxButton.OKCancel) != MessageBoxResult.OK)//messageboxresult System.Windows.Forms.DialogResult
            {
                return;
            }

            try
            {
                int visitId = selectedVisit.VisitID;
                VisitsDataAccess.DeleteVisitById(visitId);
                this.Presenter.LoadAllVisits();
            }
            catch (Exception ex)
            {
                string errorMessage = string.Format("При удалении объекта произошла ошибка!\n {0}", ex.Message);
                this.Message = errorMessage;
            }
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void CommandBinding1_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.Presenter.LoadVisitsByCriterias();
        }
        private void textBoxReason_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Presenter.LoadVisitsByCriterias();
        }

       
    }
}
