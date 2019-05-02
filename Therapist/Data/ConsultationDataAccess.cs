using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
namespace Therapist.Data
{
    public class ConsultationDataAccess
    {
        public static IQueryable<Consultation> GetConsultations()
        {
             TherapistContainer1 context = new  TherapistContainer1();
            var consultations = context.Consultations.Include("Doctor").Include("Patient");
            return consultations;
        }

        public static Consultation GetConsultationById(int consultationId)
        {
             TherapistContainer1 context = new  TherapistContainer1();
            var consultation = context
                                .Consultations
                                .Include("Doctor")
                                .Include("Patient")
                                .Where(p => p.ConsultationID == consultationId)
                                .FirstOrDefault();
            context.Detach(consultation);
            return consultation;
        }

        public static void InsertConsultation(Consultation consultation)
        {
             TherapistContainer1 context = new  TherapistContainer1();
            if (consultation.EntityState != System.Data.EntityState.Detached)
            {
                context.ObjectStateManager.ChangeObjectState(consultation, System.Data.EntityState.Added);
            }
            else
            {
                context.Consultations.AddObject(consultation);
            }
            context.SaveChanges();
            //detaching the object - ablity to share between different contexts
            context.Detach(consultation);

        }

        public static void UpdateConsultation(Consultation consultation)
        {
             TherapistContainer1 context = new  TherapistContainer1();
            context.Consultations.AddObject(consultation);
            context.ObjectStateManager.ChangeObjectState(consultation, System.Data.EntityState.Modified);
            context.SaveChanges();
            context.Detach(consultation);
        }

        public static void DeleteConsultation(Consultation consultation)
        {
             TherapistContainer1 context = new  TherapistContainer1();
            if (consultation.EntityState == System.Data.EntityState.Detached)
            {
                context.Consultations.Attach(consultation);
            }
            context.Consultations.DeleteObject(consultation);
            context.SaveChanges();
        }

        public static void DeleteConsultationById(int consultationId)
        {
             TherapistContainer1 context = new  TherapistContainer1();
            var consultation = context.Consultations.Where(p => p.ConsultationID == consultationId).FirstOrDefault();

            context.Consultations.DeleteObject(consultation);
            context.SaveChanges();
        }

        public static IQueryable<Consultation> GetConsultationsByPatientId(int patientId)
        {
             TherapistContainer1 context = new  TherapistContainer1();
            var consultations = context.Consultations
                                        .Include("Doctor")
                                        .Where(p => p.PatientID == patientId);
            return consultations;
        }

        public static IQueryable<Consultation> GetConsultationsByDoctorId(int doctorId)
        {
             TherapistContainer1 context = new  TherapistContainer1();
            var consultations = context.Consultations
                                        .Include("Patient")
                                        .Where(p => p.DoctorID == doctorId);
            return consultations;
        }

        public static IQueryable<Consultation> GetConsultationsInNextDays(int patientId, int days)
        {
             TherapistContainer1 context = new  TherapistContainer1();
            DateTime fromDate = DateTime.Now;
            DateTime toDate = DateTime.Now.AddDays(3);
            var consultations = context.Consultations
                                        .Include("Patient")
                                        .Where(c => c.PatientID == patientId)
                                        .Where(c => c.ScheduleDate >= fromDate && c.ScheduleDate <= toDate);
            return consultations;
        }


    }
}
