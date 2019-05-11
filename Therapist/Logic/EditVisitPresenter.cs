using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Therapist.Data;
using Therapist.View;

namespace Therapist.Logic
{
    public class EditVisitPresenter
    {
        public Visit Visit { get; set; }
        public IEditVisitView View { get; set; }

        public EditVisitPresenter(IEditVisitView view)
        {
            this.View = view;
        }

        public EditVisitPresenter(IEditVisitView view, int visitId)
        {
            this.View = view;
            this.Load(visitId);
        }

        protected void FillVisit()
        {
            Visit.DoctorID = View.DoctorId;
            Visit.PatientID = View.PatientId;
            Visit.VisitDate = View.VisitDate;
            Visit.Notes = View.Notes;
            Visit.Reason = View.Reason;
            Visit.Prescription = View.Prescription;
        }

        protected void FillView()
        {
            if (Membership.CurrentUser.RoleID == 2)
            {
                int ID = (int)Membership.CurrentUser.DoctorID;
                Doctor doctor = DoctorDataAccess.GetDoctorById(ID);
                View.DoctorId = doctor.DoctorID;
                View.DoctorName = doctor.Name;
            }
            else
            {
                int doctorId = Visit.DoctorID.HasValue ? Visit.DoctorID.Value : 1;
                View.DoctorId = doctorId;
                var consultationDoctor = DoctorDataAccess.GetDoctorById(doctorId);
                if (consultationDoctor != null)
                {
                    View.DoctorName = consultationDoctor.Name;
                }
                else
                {
                    View.DoctorName = "Не выбран врач";
                }
            }

            int patientId = Visit.PatientID.HasValue ? Visit.PatientID.Value : 1;
            View.PatientId = patientId;
            var consultationPatient = PatientsDataAccess.GetPatientById(patientId);
            if (consultationPatient != null)
            {
                View.PatientName = consultationPatient.Name;
            }
            else
            {
                View.PatientName = "Не выбран пациент";
            }

            DateTime scheduleDate = Visit.VisitDate.HasValue ? Visit.VisitDate.Value : DateTime.Now;
            View.VisitDate = scheduleDate;

            View.Notes = Visit.Notes;
            View.Reason = Visit.Reason;
            View.Prescription = Visit.Prescription;
            View.VisitId = Visit.VisitID;
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

            if (!Visit.VisitDate.HasValue)
            {
                message += String.Format("Поле '{0}' пусто!\n", "Дата");
                isValid = false;
            }

            if ((!Visit.PatientID.HasValue) || (Visit.PatientID == 0))
            {
                message += String.Format("Поле '{0}' пусто!\n", "Пациент");
                isValid = false;
            }

            if (!Visit.DoctorID.HasValue)
            {
                message += String.Format("Поле '{0}' пусто!\n", "Врач");
                isValid = false;
            }
            if (String.IsNullOrEmpty(Visit.Reason))
            {
                message += String.Format("Поле '{0}' пусто!\n", "Диагноз");
                isValid = false;
            }
            if (String.IsNullOrEmpty(Visit.Notes))
            {
                message += String.Format("Поле '{0}' пусто!\n", "Симптомы");
                isValid = false;
            }
            return isValid;
        }

        public void Save()
        {
            this.FillVisit();
            bool isValid = IsValid();
            if (isValid)
            {
                SaveModel(Visit);
                FillView();
            }
        }

        private void SaveModel(Visit model)
        {
            try
            {
                if (Visit.VisitID == 0)
                {
                    VisitsDataAccess.InsertVisit(Visit);
                }
                else
                {
                    VisitsDataAccess.UpdateVisit(Visit);
                }
                View.Message = "Успешная запись!";
            }
            catch (Exception e)
            {
                var message = String.Format("Ошибка хранилища! Позвоните администратору!/n {0} ", e.Message);
                View.Message = message;
            }

        }

        public void CreateNew()
        {
            var newVisit = new Visit();
            this.Visit = newVisit;

            var currentUser = Membership.CurrentUser;

            var currentUserDoctor = currentUser.Doctor;
            if (currentUserDoctor != null)
            {
                this.Visit.DoctorID = currentUserDoctor.DoctorID;
                this.View.DoctorName = currentUserDoctor.Name;
            }

            this.FillView();
        }

        public void Load(int visitId)
        {
            try
            {
                if (visitId == 0)
                {
                    throw new ArgumentNullException("visitId должен отличаться от 0!");
                }
                var visit = VisitsDataAccess.GetVisitsById(visitId);
                this.Visit = visit;
                this.FillView();
            }
            catch (Exception e)
            {
                string message = "Ошибка!:" + e.Message;
                View.Message = message;
            }
        }
    }
}
