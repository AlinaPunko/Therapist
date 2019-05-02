using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Therapist.View
{
        public interface IEditConsultationView
        {
            int ConsultationId { get; set; }
            DateTime ScheduleDate { get; set; }
            TimeSpan ScheduleTime { get; set; }
            string Reason { get; set; }
            string Notes { get; set; }

            int PatientId { get; set; }
            string PatientName { get; set; }


            int DoctorId { get; set; }
            string DoctorName { get; set; }

            string Message { set; }

        }
}
