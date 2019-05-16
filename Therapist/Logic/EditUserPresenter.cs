using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Therapist.Data;
using Therapist.Models;
using Therapist.View;

namespace Therapist.Logic
{
    public class EditUserPresenter
    {
        public User User { get; set; }
        public IEditUserView View { get; set; }

        public EditUserPresenter(IEditUserView view)
        {
            this.View = view;
        }

        public EditUserPresenter(IEditUserView view, int userId)
        {
            this.View = view;
            this.Load(userId);
        }

        protected void FillUser()
        {
            User.UserName = View.UserName;
            User.Password = View.Password;
        }

        protected void FillView()
        {
            View.UserId = User.UserID;
            View.UserName = User.UserName;
        }

        protected bool IsValid()
        {
            string message = string.Empty;
            bool isValid = IsDataValid(out message);
            View.Message = message;
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

            if (String.IsNullOrEmpty(User.UserName))
            {
                message += String.Format("Поле '{0}' пусто!\n", "Имя пользователя");
                isValid = false;
            }

            if (String.IsNullOrEmpty(User.Password))
            {
                message += String.Format("Поле '{0}' пусто!\n", "Пароль");
                isValid = false;
            }

            if (User.Password.Length < 3)
            {
                message += String.Format("Поле '{0}' должно быть длиннее 2!\n", "Пароль");
                isValid = false;
            }

            if (View.Password != View.ConfirmPassword)
            {
                message += String.Format("Поля '{0}' и '{1}' не совпадают!\n", "Пароль", "Подтвердить пароль");
                isValid = false;
            }

            return isValid;
        }

        public void Save()
        {
            this.FillUser();
            bool isValid = IsValid();
            if (isValid)
            {
                SaveModel(User);
                FillView();
            }
            else { MessageBox.Show("Проблема"); };
        }

        private void SaveModel(User model)
        {
            try
            {
                if (User.UserID == 0 && User.RoleID==2)
                {
                    User.RoleID = (int)UserRoles.Doctor;
                    UsersDataAccess.InsertUser(User);
                }
                else if (User.UserID == 0 && User.RoleID == 1)
                {
                    User.RoleID = (int)UserRoles.Admin;
                    UsersDataAccess.InsertUser(User);
                }
                else
                {
                    UsersDataAccess.UpdateUser(User);
                }

                View.Message = "Успешная запись!";
            }
            catch (Exception e)
            {
                var message = String.Format("Ошибка хранилища!Позвоните администратору!/ n {0} ",
                    e.Message);
                View.Message = message;
            }

        }

        public void CreateNew()
        {
            var newUser = new User() { RoleID = 1, UserName = " " };
            this.User = newUser;
            this.FillView();
        }
        public void CreateNew(int doctorid)
        {
            var newUser = new User() {RoleID=2, DoctorID=doctorid, UserName="" };
            //var newDoctor = new Doctor() {/* Name = "Нет имени", Number = "Нет номера" */};
            //newUser.Doctor = newDoctor;
            this.User = newUser;
            this.FillView();
        }
        public void Load(int userId)
        {
            try
            {
                if (userId == 0)
                {
                    throw new ArgumentNullException("userId должен отличаться от 0!");
                }

                var user = UsersDataAccess.GetUserById(userId);
                this.User = user;
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
