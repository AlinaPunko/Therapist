using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using Therapist.Data;
using Therapist.View;

namespace Therapist.Logic
{
    public class EditPatientPresenter
    {
        public Patient Patient { get; set; }
        public IEditPatientView View { get; set; }

        public EditPatientPresenter(IEditPatientView view)
        {
            this.View = view;
        }

        public EditPatientPresenter(IEditPatientView view, int patientId)
        {
            this.View = view;
            this.Load(patientId);
        }

        protected void FillPatient()
        {
            Patient.Name = View.PatientName;
            Patient.Phone = View.Phone;
            Patient.Address = View.Address;
            Patient.Birthdate = View.Birthdate;
        }

        protected void FillView()
        {
            View.PatientName = Patient.Name;
            View.Phone = Patient.Phone;
            View.Address = Patient.Address;
            View.Birthdate = Patient.Birthdate.HasValue ? Patient.Birthdate.Value : DateTime.Today;
            View.PatientId = Patient.PatientID;
            View.Consultations = Patient.Consultations;
            View.Visits = Patient.Visit;
        }

        protected bool IsValid()
        {
            string message = string.Empty;
            bool isValid = IsDataValid(out message);
            if (!isValid)
            {
                View.Message = message;
            }

            return isValid;
        }

        protected bool IsDataValid(out string message)
        {
            message = string.Empty;
            bool isValid = true;
            string _regex = @"\d{13}";
            if (String.IsNullOrEmpty(Patient.Name))
            {
                message += String.Format("Поле '{0}' пусто!\n", "Имя");
                isValid = false;
            }

            if (String.IsNullOrEmpty(Patient.PatientID.ToString()))
            {
                message += String.Format("Поле '{0}' пусто!\n", "Номер ");
                isValid = false;
            }
            if (String.IsNullOrEmpty(Patient.Address))
            {
                message += String.Format("Поле '{0}' пусто!\n", "Адрес ");
                isValid = false;
            }
            //if (!Regex.IsMatch(Patient.Phone, _regex))
            //{
            //    message += String.Format("Неверный формат телефона");
            //    isValid = false;
            //}

            return isValid;
        }

        public void Save()
        {
            this.FillPatient();
            bool isValid = IsValid();
            if (isValid)
            {
                MessageBox.Show("Успешно");
                SaveModel(Patient);
                FillView();
            }
            else MessageBox.Show("Проблема");
        }

        private void SaveModel(Patient model)
        {
            try
            {
                if (Patient.PatientID == 0)
                {
                    PatientsDataAccess.InsertPatient(Patient);
                }
                else
                {
                    PatientsDataAccess.UpdatePatient(Patient);
                }
                View.Message = "Успешная запись!";
            }
            catch (Exception e)
            {
                var message = String.Format("Ошибка хранилища");
                MessageBox.Show(e.Message);
                View.Message = message;
            }

        }

        public void CreateNew()
        {
            var newPatient = new Patient();
            this.Patient = newPatient;
            this.FillView();
        }

        public void Load(int patientId)
        {
            try
            {
                if (patientId == 0)
                {
                    throw new ArgumentNullException("patientId должен отличаться от 0!");
                }
                var patient = PatientsDataAccess.GetPatientWithConsultationsAndVisitsById(patientId);
                this.Patient = patient;
                this.FillView();
                this.LoadConsultations();
            }
            catch (Exception e)
            {
                string message = "Ошибка!:" + e.Message;
                View.Message = message;
            }
        }

        public void LoadConsultations()
        {
            try
            {
                if (this.Patient == null || this.Patient.PatientID == 0)
                {
                    return;
                }

                int patientId = this.Patient.PatientID;
                var consultations = ConsultationDataAccess.GetConsultationsByPatientId(patientId);
                this.View.Consultations = consultations;
            }
            catch (Exception e)
            {
                string message = "Ошибка при загрузке консультации для пациента!\n" + e.Message;
                View.Message = message;
            }
        }

        public void LoadVisit()
        {
            try
            {
                if (this.Patient == null || this.Patient.PatientID == 0)
                {
                    return;
                }

                int patientId = this.Patient.PatientID;
                var visits = VisitsDataAccess.GetVisitsByPatientId(patientId);
                this.View.Visits = visits;
            }
            catch (Exception e)
            {
                string message = "Ошибка при загрузке диагнозов для пациента!\n" + e.Message;
                View.Message = message;
            }
        }

    }
}

