using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Therapist.Data;

namespace Therapist.View
{
    public interface IEditPatientView
    {
        int PatientId { set; }
        string PatientName { get; set; }
        string Address { get; set; }
        string Phone { get; set; }
        DateTime Birthdate { get; set; }
        IEnumerable<Visit> Visits { set; }
        IEnumerable<Consultation> Consultations { set; }
        string Message { get; set; }
    }
}
