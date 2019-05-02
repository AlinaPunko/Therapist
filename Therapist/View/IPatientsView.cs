using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Therapist.Data;

namespace Therapist.View
{
    public interface IPatientsView
    {
        IEnumerable<Patient> Patients { set; }
        string Message { set; }
    }
}
