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
    public class ConsultationsPresenter
    {
        public ConsultationsPresenter(IConsultationsView view)
        {
            this.View = view;
            this.View.ScheduleDateFromCriteria = DateTime.Now.AddDays(-1);
            this.View.ScheduleDateToCriteria = DateTime.Now.AddDays(21);
        }

        private IEnumerable<Consultation> _consultations;
        public IEnumerable<Consultation> Consultations
        {
            get
            {
                return _consultations;
            }
            set
            {
                _consultations = value;
                View.Consultations = _consultations;
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

        public IConsultationsView View { get; set; }

        /// <summary>
        /// Filters consultations by name and number and sets the datagrdview source
        /// </summary>
        /// <param name="name"></param>
        /// <param name="number"></param>
        public void LoadConsultationsByCriterias(DateTime? dateTimeFrom, DateTime? dateTimeTo, int doctorId)
        {
            try
            {
                IQueryable<Consultation> consultationsQuery;
                consultationsQuery = ConsultationDataAccess.GetConsultations();
                if (doctorId != 0)
                {
                    consultationsQuery = consultationsQuery.Where(c => c.DoctorID == doctorId);
                }
                if (dateTimeFrom.HasValue)
                {
                    DateTime dateTimeFromValue = dateTimeFrom.Value;
                    consultationsQuery = consultationsQuery.Where(p => p.ScheduleDate.Value > dateTimeFromValue);
                }

                if (dateTimeTo.HasValue)
                {
                    DateTime dateTimeToValue = dateTimeTo.Value;
                    consultationsQuery = consultationsQuery.Where(p => p.ScheduleDate.Value < dateTimeToValue);
                }

                this.Consultations = consultationsQuery.ToList();
            }
            catch (Exception e)
            {
                this.Message = string.Format("Ошибка при запросе базы данных\n {0}", e.Message);
            }
        }

        public void LoadConsultationsByCriterias()
        {
            DateTime? dateTimeFrom = this.View.ScheduleDateFromCriteria;
            DateTime? dateTimeTo = this.View.ScheduleDateToCriteria;
            int currentDoctorId = 0;

            var currentUser = Membership.CurrentUser;
            if (currentUser.RoleID == (int)UserRoles.Doctor)
            {
                currentDoctorId = Membership.CurrentUser.DoctorID.HasValue ? Membership.CurrentUser.DoctorID.Value : 0;
            }
            else
            {
                currentDoctorId = 0;
            }

            this.LoadConsultationsByCriterias(dateTimeFrom, dateTimeTo, currentDoctorId);
        }

        internal void LoadAllConsultations()
        {
            this.LoadConsultationsByCriterias(null, null, 0);
        }
    }
}
