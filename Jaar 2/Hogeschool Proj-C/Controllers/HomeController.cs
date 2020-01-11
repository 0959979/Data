using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using zorgapp.Models;

namespace zorgapp.Controllers
{
    public class HomeController : Controller
    
    {
        private readonly DatabaseContext _context;

        public HomeController(DatabaseContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            Doctor doc = _context.Doctors.FirstOrDefault(u => u.DoctorId == -1);
            if (doc.LocalId == null)
            {
                doc.LocalId = new List<String>();
            }
            Doctor doc2 = _context.Doctors.FirstOrDefault(u => u.DoctorId == -2);
            if (doc2.LocalId == null)
            {
                doc2.LocalId = new List<String>();
            }
            Doctor doc3 = _context.Doctors.FirstOrDefault(u => u.DoctorId == -3);
            if (doc3.LocalId == null)
            {
                doc3.LocalId = new List<String>();
            }

            Patient pat = _context.Patients.FirstOrDefault(u => u.PatientId == -1);
            if (pat.LocalId == null || pat.ICanSeeId == null || pat.CanSeeMeId == null)
            {
                pat.LocalId = new List<String>();
                pat.ICanSeeId = new List<int>();
                pat.CanSeeMeId = new List<int>();

            }

            Patient pat2 = _context.Patients.FirstOrDefault(u => u.PatientId == -2);
            if (pat2.LocalId == null || pat2.ICanSeeId == null || pat2.CanSeeMeId == null)
            {
                pat2.LocalId = new List<String>();
                pat2.ICanSeeId = new List<int>();
                pat2.CanSeeMeId = new List<int>();

            }


            Patient pat3 = _context.Patients.FirstOrDefault(u => u.PatientId == -3);
            if (pat3.LocalId == null || pat3.ICanSeeId == null || pat3.CanSeeMeId == null)
            {
                pat3.LocalId = new List<String>();
                pat3.ICanSeeId = new List<int>();
                pat3.CanSeeMeId = new List<int>();

            }
            _context.SaveChanges();
            return View();
        }

    }
    
}
