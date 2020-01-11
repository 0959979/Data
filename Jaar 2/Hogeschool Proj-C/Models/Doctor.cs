using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace zorgapp.Models

{
    // model for doctor

    public class Doctor
    {
        public int DoctorId { get; set; }
        public List<string> LocalId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Specialism { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public ICollection<PatientsDoctors> PatientsDoctorss { get; set; }
    }
}
