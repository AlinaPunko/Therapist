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
    public class EditDoctorPresenter
    {
        public Doctor Doctor { get; set; }
        public IEditDoctorView View { get; set; }

        public EditDoctorPresenter(IEditDoctorView view)
        {
            this.View = view;
        }

        public EditDoctorPresenter(IEditDoctorView view, int doctorId)
        {
            this.View = view;
            this.Load(doctorId);
        }

        protected void FillDoctor()
        {
            Doctor.Name = View.DoctorName;
            Doctor.Skils = View.Skills;
            Doctor.Phone = View.Phone;
            Doctor.Address = View.Address;
        }

        protected void FillView()
        {
            View.DoctorName = Doctor.Name;
            View.Skills = Doctor.Skils;
            View.Phone = Doctor.Phone;
            View.Address = Doctor.Address;
            View.DoctorId = Doctor.DoctorID;
        }

        protected bool IsValid()
        {
            string message = string.Empty;
            bool isValid = IsDataValid(out message);
            View.Message = message;

            return isValid;
        }

        protected bool IsDataValid(out string message)
        {
            message = string.Empty;
            bool isValid = true;
            string _regex = @"\d{13}";

            if (String.IsNullOrEmpty(Doctor.Name))
            {
                message += String.Format("Поле '{0}' пусто!\n", "Имя");
                isValid = false;
            }
            if (String.IsNullOrEmpty(Doctor.Address))
            {
                message += String.Format("Поле '{0}' пусто!\n", "Адрес");
                isValid = false;
            }
            if (String.IsNullOrEmpty(Doctor.Skils))
            {
                message += String.Format("Поле '{0}' пусто!\n", "Опыт");
                isValid = false;
            }
            //if (!Regex.IsMatch(Doctor.Phone, _regex))
            //{
            //    message += String.Format("Неверный формат телефона");
            //    isValid = false;
            //}
            return isValid;
        }

        public void Save()
        {

            this.FillDoctor();
            bool isValid = IsValid();
            if (isValid)
            {
                MessageBox.Show("Успешно");
                SaveModel(Doctor);
                FillView();
            }
            else
            { MessageBox.Show("Проблема");
                FillView();
            }
        }
        public void Save1()
        {

            this.FillDoctor();
            bool isValid = IsValid();
            if (isValid)
            {
                MessageBox.Show("Успешно");
                SaveModel(Doctor);
                FillView();
                EditUserForm editUserForm = new EditUserForm(Doctor.DoctorID, true);
                editUserForm.Show();
            }
            else MessageBox.Show("Проблема");
        }

        private void SaveModel(Doctor model)
        {
            try
            {
                if (Doctor.DoctorID == 0)
                {
                    DoctorDataAccess.InsertDoctor(Doctor);
                }
                else
                {
                    DoctorDataAccess.UpdateDoctor(Doctor);
                }
                View.Message = "Успешная запись!";
            }
            catch (Exception e)
            {
                var message = String.Format("Ошибка хранилища!Позвоните администратору!/ n [0] ", e.Message);
                View.Message = message;
            }

        }

        public void CreateNew()
        {
            var newDoctor = new Doctor();
            this.Doctor = newDoctor;
            this.FillView();
        }

        public void CreateNew(bool flag)
        {
            var newDoctor = new Doctor();
            this.Doctor = newDoctor;
            this.FillView();
        }

        public void Load(int doctorId)
        {
            try
            {
                if (doctorId == 0)
                {
                    throw new ArgumentNullException("doctorId должен отличаться от 0!");
                }
                var doctor = DoctorDataAccess.GetDoctorById(doctorId);
                this.Doctor = doctor;
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
