using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Therapist.View
{
    public interface IEditDoctorView
    {
        int DoctorId { set; }
        string DoctorName { get; set; }
        string Skills { get; set; }
        string Address { get; set; }
        string Phone { get; set; }


        string Message { get; set; }

    }
}
