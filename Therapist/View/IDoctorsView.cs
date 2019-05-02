using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Therapist.Data;

namespace Therapist.View
{
    public interface IDoctorsView
    {
        IEnumerable<Doctor> Doctors { set; }
        string Message { set; }

        string NameSearch { get; set; }
    }
}
