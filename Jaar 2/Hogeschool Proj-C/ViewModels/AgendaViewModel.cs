using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using zorgapp.Models;

namespace zorgapp.ViewModels
{
    public class AgendaViewModel
    {
        public List<string> Days;
        public List<string> Dates;
        public List<string> Hours;
        public List<int> Hoursi;
        public List<int> Minutes;
        public int CurrentDate;
        public int dayOffset;
        public bool sameWeek;

        public List<Case> Cases;
        public List<Appointment> Appointments;
    }
}
