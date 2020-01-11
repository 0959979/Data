using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace zorgapp.Models
{
    // model for patient
    public class Patient
    {

        public int PatientId { get; set; }
        public List<string> LocalId { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public List<int> CanSeeMeId { get; set; }
        public List<int> ICanSeeId { get; set; }
        public string LinkCode { get; set; }
        public int LinkUses { get; set; }
        public Doctor Doctor { get; set; }
        public ICollection<PatientsDoctors> PatientsDoctorss { get; set; }
    }

}
