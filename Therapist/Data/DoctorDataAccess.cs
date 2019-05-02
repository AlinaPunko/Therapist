using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Therapist.Data
{
    public class DoctorDataAccess
    {
        public static IQueryable<Doctor> GetDoctors()
        {
            TherapistContainer1 context = new  TherapistContainer1();
            return context.Doctors;
        }

        public static Doctor GetDoctorById(int doctorId)
        {
            TherapistContainer1 context = new TherapistContainer1();
            var doctor = context.Doctors.Where(p => p.DoctorID == doctorId).FirstOrDefault();
            if (doctor != null)
            {
                context.Detach(doctor);
            }
            return doctor;
        }

        public static void InsertDoctor(Doctor doctor)
        {
            TherapistContainer1 context = new TherapistContainer1();
            if (doctor.EntityState != System.Data.EntityState.Detached)
            {
                context.ObjectStateManager.ChangeObjectState(doctor, System.Data.EntityState.Added);
            }
            else
            {
                context.Doctors.AddObject(doctor);
            }
            context.SaveChanges();
        }

        public static void UpdateDoctor(Doctor doctor)
        {
             TherapistContainer1 context = new TherapistContainer1();
            context.Doctors.AddObject(doctor);
            context.ObjectStateManager.ChangeObjectState(doctor, System.Data.EntityState.Modified);
            context.SaveChanges();
        }

        public static void DeleteDoctor(Doctor doctor)
        {
             TherapistContainer1 context = new   TherapistContainer1();
            if (doctor.EntityState == System.Data.EntityState.Detached)
            {
                context.Doctors.Attach(doctor);
            }
            context.Doctors.DeleteObject(doctor);
            context.SaveChanges();
        }

        public static void DeleteDoctorById(int doctorId)
        {
             TherapistContainer1 context = new  TherapistContainer1();
            var doctor = context.Doctors.Where(p => p.DoctorID == doctorId).FirstOrDefault();

            context.Doctors.DeleteObject(doctor);
            context.SaveChanges();
        }
    }
}
