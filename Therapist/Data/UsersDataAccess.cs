using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
namespace Therapist.Data
{
    class UsersDataAccess
    {
        /// <summary>
        /// Get all users in the database
        /// </summary>
        /// <returns></returns>
        public static IQueryable<User> GetAllUsers()
        {
             TherapistContainer1 context = new  TherapistContainer1();
            var users = context.Users;

            return users.AsQueryable();
        }

        /// <summary>
        /// Get user by name from the database
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static User GetUserByName(string username)
        {
             TherapistContainer1 context = new  TherapistContainer1();
            var user = context.Users.Where(u => u.UserName == username).FirstOrDefault();
            var doctor = user.Doctor;

            return user;
        }

        /// <summary>
        /// Get user by name from the database
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static User GetUserById(int userId)
        {
             TherapistContainer1 context = new  TherapistContainer1();
            var user = context.Users.Include("Doctor")
                                    .Include("Patient")
                                    .Where(u => u.UserID == userId).FirstOrDefault();

            return user;
        }

        /// <summary>
        /// Validates login details
        /// </summary>
        /// <param name="username">Username of the user to log in</param>
        /// <param name="password">Password of the user to logi in</param>
        /// <returns></returns>
        public static bool IsValidLoginData(string username, string password)
        {
             TherapistContainer1 context = new  TherapistContainer1();
            var user = context.Users.Where(u => u.UserName == username && u.Password == password).FirstOrDefault();
            if (user == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static User AnonimousUser
        {
            get
            {
                var user = new User()
                {
                    UserName = "Anonimous",
                    UserID = 0,
                    DoctorID = null,
                    RoleID = 0
                };
                return user;
            }
        }

        public static void InsertUser(User user)
        {
             TherapistContainer1 context = new  TherapistContainer1();
            if (user.EntityState != System.Data.EntityState.Detached)
            {
                context.ObjectStateManager.ChangeObjectState(user, System.Data.EntityState.Added);
            }
            else
            {
                context.Users.AddObject(user);
            }
            context.SaveChanges();
        }

        public static void UpdateUser(User user)
        {
             TherapistContainer1 context = new  TherapistContainer1();
            context.Users.AddObject(user);
            context.ObjectStateManager.ChangeObjectState(user, System.Data.EntityState.Modified);
            context.SaveChanges();
        }

        public static void DeleteUser(User user)
        {
             TherapistContainer1 context = new  TherapistContainer1();
            if (user.EntityState == System.Data.EntityState.Detached)
            {
                context.Users.Attach(user);
            }
            context.Users.DeleteObject(user);
            context.SaveChanges();
        }

        public static void DeleteUserById(int userId)
        {
             TherapistContainer1 context = new  TherapistContainer1();
            var user = context.Users.Where(p => p.UserID == userId).FirstOrDefault();

            context.Users.DeleteObject(user);
            context.SaveChanges();
        }
        public static void DeleteUserByDoctorId(int doctorId)
        {
            TherapistContainer1 context = new TherapistContainer1();
            var user = context.Users.Where(p => p.DoctorID == doctorId).FirstOrDefault();

            context.Users.DeleteObject(user);
            context.SaveChanges();
        }
    }
}
