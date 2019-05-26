using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Therapist.Data;
using Therapist.Models;
using Therapist.View;

namespace Therapist.Logic
{
    public class VisitsPresenter
    {
        public VisitsPresenter(IVisitsView view)
        {
            this.View = view;
            this.View.VisitDateFromCriteria = DateTime.Now.AddDays(-30);
            this.View.VisitDateToCriteria = DateTime.Now.AddDays(-1);
        }

        private IEnumerable<Visit> _visits;
        public IEnumerable<Visit> Visits
        {
            get
            {
                return _visits;
            }
            set
            {
                _visits = value;
                View.Visits = _visits;
            }
        }

        private string _message;
        public string Message
        {
            get
            {
                return _message;
            }
            set
            {
                _message = value;
                View.Message = _message;
            }
        }

        public IVisitsView View { get; set; }

        /// <summary>
        /// Filters visits by name and number and sets the datagrdview source
        /// </summary>
        /// <param name="name"></param>
        /// <param name="number"></param>
        public void LoadVisitByCriterias(DateTime? dateTimeFrom, DateTime? dateTimeTo, string subject, int doctorId)
        {
            try
            {
                IQueryable<Visit> VisitsQuery;
                VisitsQuery = VisitsDataAccess.GetVisits();

                if (!string.IsNullOrEmpty(subject))
                {
                    VisitsQuery = VisitsQuery.Where(d => d.Reason.Contains(subject));
                }
                if (dateTimeFrom.HasValue)
                {
                    DateTime dateTimeFromValue = dateTimeFrom.Value;
                    VisitsQuery = VisitsQuery.Where(p => p.VisitDate.Value > dateTimeFromValue);
                }

                if (dateTimeTo.HasValue)
                {
                    DateTime dateTimeToValue = dateTimeTo.Value;
                    VisitsQuery = VisitsQuery.Where(p => p.VisitDate.Value < dateTimeToValue);
                }
                if (doctorId != 0)
                {
                    VisitsQuery = VisitsQuery.Where(d => d.DoctorID == doctorId);
                }


                this.Visits = VisitsQuery.ToList();
            }
            catch (Exception e)
            {
                this.Message = "Ошибка при запросе базы данных!Вызовите администратора!";
            }
        }

        public void LoadVisitsByCriterias()
        {
            string subject = this.View.ReasonSearch;
            DateTime? dateTimeFrom = this.View.VisitDateFromCriteria;
            DateTime? dateTimeTo = this.View.VisitDateToCriteria;
            int currentDoctorID = 0;

            var currentUser = Membership.CurrentUser;
            if (currentUser.RoleID == (int)UserRoles.Doctor)
            {
                currentDoctorID = Membership.CurrentUser.DoctorID.HasValue ? Membership.CurrentUser.DoctorID.Value : 0;
            }
            else
            {
                currentDoctorID = 0;
            }

            this.LoadVisitByCriterias(dateTimeFrom, dateTimeTo, subject, currentDoctorID);
        }

        public void LoadAllVisits()
        {
            this.LoadVisitByCriterias(null, null, null, 0);
        }
    }
}
