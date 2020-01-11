using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using zorgapp.Models;

namespace zorgapp.ViewModels
{
    public class ProfileViewModel
    {
        public Patient UserInfo;
        public List<Appointment> Appointments;
        public List<Case> Cases;
        public List<Medicine> Medicines;
    }
}
