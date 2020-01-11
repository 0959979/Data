using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace zorgapp.Models
{
    // model for case
    public class Case
    {
        public string CaseId { get; set; }
        public string CaseInfo { get; set; }
        public string CaseName { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
    }
}
