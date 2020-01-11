using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using zorgapp.Models;

namespace zorgapp.ViewModels
{
    public class PatInfoViewModel
    {
        public Case CurrentCase;
        public List<Case> Cases;
        public List<Medicine> Medicines;

        public List<Appointment> appointments;
        //public List<Appointment> PassedAppointments; 
        public DateTime Today;
    }
}
