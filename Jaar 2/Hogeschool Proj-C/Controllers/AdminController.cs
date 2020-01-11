using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using zorgapp.Models;
using zorgapp.ViewModels;

namespace zorgapp.Controllers
{
    public class AdminController : Controller
    {
        private readonly DatabaseContext _context;

        public AdminController(DatabaseContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Profile()
        {
            return View();
        }

        public ActionResult Link()
        {
            // return the view of Link and if a tempdata named "message" is not null, put the tempdata in a viewbag to return to the view
            if (TempData["message"] != null)
            {
                ViewBag.Message = TempData["message"].ToString();
                TempData.Remove("message");
            }
            return View();
        }



        //  links patient with doctor 

        public ActionResult SubmitLink(int patientid, int doctorid)
        {
            //find the doctor and patient with the given id's
            Doctor doctor = _context.Doctors.FirstOrDefault(m => m.DoctorId == doctorid);
            Patient patient = _context.Patients.FirstOrDefault(y => y.PatientId == patientid);

            //try to take the link in the db where doctor and patient are linked
            PatientsDoctors patientsDoctors_ = _context.PatientsDoctorss.FirstOrDefault(
                p => p.PatientId == patientid && p.DoctorId == doctorid
            );

            //check if the link is already made
            bool linkmade = _context.PatientsDoctorss.Contains(patientsDoctors_);

            //create the link
            PatientsDoctors patientsDoctors = new PatientsDoctors()
            {
                PatientId = patientid,
                DoctorId = doctorid
            };
            //check if patient exists, if the patient does not exists, warn the user 
            if (patient == null)
            {
                if (TempData != null)
                { TempData["message"] = "PatientId does not exist"; }
                return RedirectToAction("Link", "Admin");
            }
            //check if doctor exists, if the doctor does not exists, warn the user 
            if (doctor == null)
            {
                if (TempData != null)
                { TempData["message"] = "Doctorid does not exist"; }
                return RedirectToAction("Link", "Admin");
            }

            //when both doctor and patient exist and link is not made, add link to db
            if (!linkmade)
            {
                _context.PatientsDoctorss.Add(patientsDoctors);
                _context.SaveChanges();
            }

            //if link is already made, redirect to same page with text to warn that the link is already made
            else if (linkmade)
            {
                if (TempData != null)
                { TempData["message"] = "Link has already been made"; }
                return RedirectToAction("Link", "Admin");
            }

            ViewData["Doctor"] = doctor.FirstName;
            ViewData["Patient"] = patient.FirstName;

            return View();
        }
 

        //TESTING
        public DatabaseContext getContext()
        {
            return _context;
        }
        public IActionResult noAccess()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        public IActionResult TestPage()
        {
            if (User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value.ToLower() != "admin")
            {
                return RedirectToAction("Login", "Patient");
            }
            List<Tuple<string, string>> tupleList = new List<Tuple<string, string>>();

            List<AdminTest> testlist = new List<AdminTest>();
            {
                testlist.Add(new TemplateTestA(this));
            }
            foreach (AdminTest T in testlist)
            {
                tupleList.Add(new Tuple<string, string>(T.Id, T.Id));
            }

            TestListViewModel testlistmodel = new TestListViewModel { tuples = tupleList };
            return View(testlistmodel);
        }

        public IActionResult StartTest(string TestId)
        {
            List<AdminTest> testlist = new List<AdminTest>();
            {
                testlist.Add(new TemplateTestA(this));
            }
            AdminTest testobj = testlist.FirstOrDefault();
            foreach (AdminTest T in testlist)
            {
                if (T.Id == TestId)
                {
                    testobj = T;
                }
            }
            TestViewModel Model = testobj.Run();
            return View(Model);
        }
    }
    internal abstract class AdminTest
    {
        public abstract TestViewModel Run();
        public AdminController testController;
        public string Id;
        public string Description;
        public string Steps;
        public string Criteria;
        public string Inputstr;
        public string Aresult;
        public string Eresult;
        public bool Pass;
    }
    internal class TemplateTestA : AdminTest
    {
        public TemplateTestA(AdminController tc)
        {
            testController = tc;
            Id = ".Integration.";
            Description = " test 1";
            Steps = "Check if true == true";
            Criteria = "true must be true";
            Inputstr = "true";
            Aresult = "";
            Eresult = "True";
        }

        public override TestViewModel Run()
        {
            TestViewModel model;

            //arrange
            bool Pass = false;
            AdminController controller = testController;

            //act
            try
            {
                if (true == true)
                {
                    Aresult = "True";
                }
            }
            catch (Exception e)
            {
                Pass = false;
                Aresult = e.ToString();
                model = new TestViewModel()
                {
                    id = Id,
                    time = DateTime.Now,
                    description = Description,
                    steps = Steps,
                    criteria = Criteria,
                    input = Inputstr,
                    aresult = Aresult,
                    eresult = Eresult,
                    pass = Pass
                };
                return model;
            }

            //assert
            if (Aresult == Eresult)
            {
                Pass = true;
            }
            else
            {
                Pass = false;
            }

            model = new TestViewModel()
            {
                id = Id,
                time = DateTime.Now,
                description = Description,
                steps = Steps,
                criteria = Criteria,
                input = Inputstr,
                aresult = Aresult,
                eresult = Eresult,
                pass = Pass
            };
            return model;
        }
    }
}



