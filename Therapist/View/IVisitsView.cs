using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Therapist.Data;

namespace Therapist.View
{
    public interface IVisitsView
    {
        IEnumerable<Visit> Visits { set; }
        string Message { set; }
        DateTime? VisitDateFromCriteria { get; set; }
        DateTime? VisitDateToCriteria { get; set; }
        string ReasonSearch { get; set; }
    }
}
