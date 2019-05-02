using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Therapist.Data
{
    class VisitsDataAccess
    {
        public static IQueryable<Visit> GetVisits()
        {
            TherapistContainer1 context = new TherapistContainer1();
            return context.Visits;
        }

        public static IQueryable<Visit> GetVisitsByPatientId(int patientId)
        {
            TherapistContainer1 context = new TherapistContainer1();
            var visits = context.Visits.Include("Doctor").Where(d => d.PatientID == patientId);

            return visits;
        }

        public static IQueryable<Visit> GetVisitsByDoctorId(int doctorId)
        {
            TherapistContainer1 context = new TherapistContainer1();
            var visits = context.Visits.Include("Patient").Where(d => d.DoctorID == doctorId);

            return visits;
        }

        public static Visit GetVisitsById(int visitsId)
        {
            TherapistContainer1 context = new TherapistContainer1();
            var visits = context.Visits.Where(p => p.VisitID == visitsId).FirstOrDefault();
            context.Detach(visits);
            return visits;
        }

        public static void InsertVisit(Visit visits)
        {
            TherapistContainer1 context = new TherapistContainer1();
            if (visits.EntityState != System.Data.EntityState.Detached)
            {
                context.ObjectStateManager.ChangeObjectState(visits, System.Data.EntityState.Added);
            }
            else
            {
                context.Visits.AddObject(visits);
            }
            context.SaveChanges();
        }

        public static void UpdateVisit(Visit visits)
        {
            TherapistContainer1 context = new TherapistContainer1();
            context.Visits.AddObject(visits);
            context.ObjectStateManager.ChangeObjectState(visits, System.Data.EntityState.Modified);
            context.SaveChanges();
        }

        public static void DeleteVisit(Visit visits)
        {
            TherapistContainer1 context = new TherapistContainer1();
            if (visits.EntityState == System.Data.EntityState.Detached)
            {
                context.Visits.Attach(visits);
            }
            context.Visits.DeleteObject(visits);
            context.SaveChanges();
        }

        public static void DeleteVisitById(int visitsId)
        {
            TherapistContainer1 context = new TherapistContainer1();
            var visits = context.Visits.Where(p => p.VisitID == visitsId).FirstOrDefault();

            context.Visits.DeleteObject(visits);
            context.SaveChanges();
        }
    }
}
