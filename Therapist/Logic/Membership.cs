using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Therapist.Data;

namespace Therapist.Logic
{
    class Membership
    {
        private static User _currentUser;

        /// <summary>
        /// Currently logged user
        /// </summary>
        public static User CurrentUser
        {
            get
            {
                if (_currentUser == null)
                {
                    _currentUser = UsersDataAccess.AnonimousUser;
                }

                return _currentUser;
            }
            set
            {
                _currentUser = value;
            }
        }

        /// <summary>
        /// Checks if login details are correct
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool IsValidLoginDetails(string username, string password)
        {
            bool isValid = UsersDataAccess.IsValidLoginData(username, password);
            return isValid;
        }

        /// <summary>
        /// Validates login details and logs in the user
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool ValidateAndLogin(string username, string password)
        {
            if (IsValidLoginDetails(username, password) == false)
            {
                return false;
            }

            var user = UsersDataAccess.GetUserByName(username);
            LogInUser(user);

            if (CurrentUser.UserName == username)
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// Logs in passed user
        /// </summary>
        /// <param name="user"></param>
        public static void LogInUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("пользователь не должен иметь значение null!");
            }
            Membership.CurrentUser = user;
        }

        /// <summary>
        /// Logs out curent user
        /// </summary>
        internal static void LogOutUser()
        {
            Membership.CurrentUser = UsersDataAccess.AnonimousUser;
        }
    }
}
