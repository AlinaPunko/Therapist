using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Therapist.Data
{
    class PatientsDataAccess
    {
        public static IQueryable<Patient> GetPatients()
        {
             TherapistContainer1 context = new  TherapistContainer1();
            return context.Patients;
        }

        public static Patient GetPatientById(int patientId)
        {
             TherapistContainer1 context = new  TherapistContainer1();
            var patient = context.Patients
                                .Include("Consultations")
                                .Include("Visit")
                                .Where(p => p.PatientID == patientId)
                                .FirstOrDefault();
            context.Detach(patient);
            return patient;
        }

        public static Patient GetPatientWithConsultationsAndVisitsById(int patientId)
        {
             TherapistContainer1 context = new  TherapistContainer1();
            var patient = context.Patients
                                .Include("Consultations")
                                .Include("Visit")
                                .Where(p => p.PatientID == patientId)
                                .FirstOrDefault();
            context.Detach(patient);
            return patient;
        }

        public static void InsertPatient(Patient patient)
        {
             TherapistContainer1 context = new  TherapistContainer1();
            if (patient.EntityState != System.Data.EntityState.Detached)
            {
                context.ObjectStateManager.ChangeObjectState(patient, System.Data.EntityState.Modified);
            }
            else
            {
                context.Patients.AddObject(patient);
            }
            context.SaveChanges();

            //detach because object is stateless
            context.Detach(patient);
        }

        public static void UpdatePatient(Patient patient)
        {
             TherapistContainer1 context = new  TherapistContainer1();
            context.Patients.AddObject(patient);
            context.ObjectStateManager.ChangeObjectState(patient, System.Data.EntityState.Modified);
            context.SaveChanges();

            //detach because object is stateless
            context.Detach(patient);
        }

        public static void DeletePatient(Patient patient)
        {
             TherapistContainer1 context = new  TherapistContainer1();
            if (patient.EntityState == System.Data.EntityState.Detached)
            {
                context.Patients.Attach(patient);
            }
            context.Patients.DeleteObject(patient);
            context.SaveChanges();
        }

        public static void DeletePatientById(int patientId)
        {
             TherapistContainer1 context = new  TherapistContainer1();
            var patient = context.Patients.Where(p => p.PatientID == patientId).FirstOrDefault();

            context.Patients.DeleteObject(patient);
            context.SaveChanges();
        }
    }
}
