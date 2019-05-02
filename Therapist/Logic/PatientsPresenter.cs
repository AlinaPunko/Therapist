using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Therapist.Data;
using Therapist.View;

namespace Therapist.Logic
{
    public class PatientsPresenter
    {

        public PatientsPresenter(IPatientsView view)
        {
            this.View = view;
        }

        private IEnumerable<Patient> _patients;
        public IEnumerable<Patient> Patients
        {
            get
            {
                return _patients;
            }
            set
            {
                _patients = value;
                View.Patients = _patients;
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

        public IPatientsView View { get; set; }

        /// <summary>
        /// Filters patients by name and number and sets the datagrdview source
        /// </summary>
        /// <param name="name"></param>
        public void LoadPatientsByCriterias(string name)
        {
            try
            {
                IQueryable<Patient> patientsQuery;
                patientsQuery = PatientsDataAccess.GetPatients();
                if (!string.IsNullOrEmpty(name))
                {
                    patientsQuery = patientsQuery.Where(p => p.Name.Contains(name));
                }               
                this.Patients = patientsQuery.ToList();
            }
            catch (Exception e)
            {
                this.Message = "Ошибка при запросе базы данных!Вызовите администратора!";
            }
        }

        internal void LoadAllPatients()
        {
            this.LoadPatientsByCriterias(string.Empty);
        }
    }
}
