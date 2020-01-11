using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace zorgapp.Models
{
    // model for medicine
    public class Medicine
    {
        public int MedicineId { get; set; }
        public string Name { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public int Amount { get; set; }
        public float Mg { get; set; }
        public int PatientId { get; set; }
    }
}
