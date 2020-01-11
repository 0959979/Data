using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using zorgapp.Models;
using zorgapp.ViewModels;
using zorgapp.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace zorgapp.Controllers
{

    public class TestController : Controller
    {
        private readonly DatabaseContext _context;
        private List<Test> testlist;

        public TestController(DatabaseContext context)
        {
            _context = context;
            testlist = new List<Test>();
            {
                testlist.Add(new CreateDoctorAccountTest1(this));
                testlist.Add(new CreateDoctorAccountMissingParameterTest1(this));
                testlist.Add(new CreateDoctorAccountPasswordtest1(this));
                testlist.Add(new CreateDoctorAccountEmailTest1(this));
                testlist.Add(new CreateDoctorAccountUsernameTest1(this));
                testlist.Add(new LinkAdminLinkAlreadyMadeTest1(this));
                testlist.Add(new LinkAdminLinkNotMadeTest1(this));
                testlist.Add(new LinkAdminPatNullTest1(this));
                testlist.Add(new LinkAdminDocNullTest1(this));
                testlist.Add(new CreatePatientAccountTest1(this));
                testlist.Add(new CreatePatientAccountMissingParameterTest1(this));
                testlist.Add(new CreatePatientAccountPasswordtest1(this));
                testlist.Add(new CreatePatientAccountEmailTest1(this));
                testlist.Add(new CreatePatientAccountUsernameTest1(this));
                testlist.Add(new AddLocalIdToPatientTest1(this));
                testlist.Add(new AddLocalIdNullToPatientTest1(this));
                testlist.Add(new AddLocalIdAlreadyExistsToPatientTest1(this));



            }
        }

        public DatabaseContext getContext()
        {
            return _context;
        }

        public IActionResult noAccess()
        {
            return View();
        }

        public IActionResult TestPage()
        {
            List<Tuple<string, string>> tupleList = new List<Tuple<string, string>>();

            foreach (Test T in testlist)
            {
                tupleList.Add(new Tuple<string, string>(T.Id, T.Id));
            }

            TestListViewModel testlistmodel = new TestListViewModel { tuples = tupleList };
            return View(testlistmodel);
        }

        public IActionResult StartTest(string TestId)
        {
            Test testobj = testlist.FirstOrDefault();
            foreach (Test T in testlist)
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

    internal abstract class Test
    {
        public abstract TestViewModel Run();
        public TestController testController;
        public string Id;
        public string Description;
        public string Steps;
        public string Criteria;
        public string Inputstr;
        public string Aresult;
        public string Eresult;
        public bool Pass;
    }
    internal class CreateDoctorAccountTest1 : Test
    {
        public CreateDoctorAccountTest1(TestController tc)
        {
            testController = tc;
            Id = "A1.Integration.CDA1";
            Description = "Create Doctor Account Test 1";
            Steps = "Create a doctor with all values inserted";
            Criteria = "Pass: doctor is created in the database | Fail: exception error";
            Inputstr = "All parameters for a doctor";
            Aresult = "";
            Eresult = "Doctor added to database";
        }

        public override TestViewModel Run()
        {
            TestViewModel model;

            //arrange
            bool Pass = false;
            DoctorController controller = new DoctorController(testController.getContext());

            string firstName = "Harry";
            string lastName = "Jones";
            string eMail = "HarryJ1@email.com";
            string phoneNumber = "06-24685344";
            string specialism = "Bone fractures";
            string localId = "2515T1";
            string userName = "UserAT1";
            string password = "password12342";

            //act
            try
            {
                controller.SubmitDoctorAccount(firstName, lastName, eMail, phoneNumber, specialism, localId, userName, password);
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
            DatabaseContext Tcontext = testController.getContext();
            Doctor doc = Tcontext.Doctors.FirstOrDefault(u => u.UserName == userName);
            if (doc == null)
            {
                Aresult = "Doctor not added to database";
                Pass = false;
            }
            else
            {
                Aresult = "Doctor added to database";
                Pass = true;
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

    internal class CreateDoctorAccountMissingParameterTest1 : Test
    {
        public CreateDoctorAccountMissingParameterTest1(TestController tc)
        {
            testController = tc;
            Id = "A1.Integration.CDA2";
            Description = "Create Doctor account with a missing parameter";
            Steps = "Create a doctor account with a parameter missing";
            Criteria = "Pass: doctor is not created | Fail: doctor is created or exeption error";
            Inputstr = "All parameters except email";
            Aresult = "";
            Eresult = "Doctor is not created";
        }

        public override TestViewModel Run()
        {
            TestViewModel model;

            //arrange
            bool Pass = false;
            DoctorController controller = new DoctorController(testController.getContext());

            string firstName = "Haley";
            string lastName = "Jones";
            string eMail = null;
            string phoneNumber = "06-24685344";
            string specialism = "Bone fractures";
            string localId = "2515T2";
            string userName = "Haley";
            string password = "password";

            //act
            try
            {
                controller.SubmitDoctorAccount(firstName, lastName, eMail, phoneNumber, specialism, localId, userName, password);
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
            DatabaseContext Tcontext = testController.getContext();
            Doctor doc = Tcontext.Doctors.FirstOrDefault(u => u.UserName == userName);
            if (doc == null)
            {
                Aresult = "Doctor is not created";
                Pass = true;
            }
            else
            {
                Aresult = "Doctor is created";
                Pass= false;
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
    internal class CreateDoctorAccountPasswordtest1 : Test
    {
        public CreateDoctorAccountPasswordtest1(TestController tc)
        {
            testController = tc;
            Id = "A1.Integration.CDA3";
            Description = "Create Doctor Account with password less than 8 characters";
            Steps = "Try to create a doctor account with a password less than 8 characters long";
            Criteria = "Pass: doctor is not created | Fail: doctor is created";
            Inputstr = "All parameters entered with a password with less than 8 characters";
            Aresult = "";
            Eresult = "Doctor is not created";
        }

        public override TestViewModel Run()
        {
            TestViewModel model;

            //arrange
            bool Pass = false;
            DoctorController controller = new DoctorController(testController.getContext());

            string firstName = "Kim";
            string lastName = "Jones";
            string eMail = "KimmJJ@email.com";
            string phoneNumber = "06-24685344";
            string specialism = "Bone fractures";
            string localId = "2515T3";
            string userName = "KimJJ";
            string password = "passw";

            //act
            try
            {
                controller.SubmitDoctorAccount(firstName, lastName, eMail, phoneNumber, specialism, localId, userName, password);
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
            DatabaseContext Tcontext = testController.getContext();
            Doctor doc = Tcontext.Doctors.FirstOrDefault(u => u.UserName == userName);
            if (doc == null)
            {
                Aresult = "Doctor is not created";
                Pass = true;
            }
            else
            {
                Aresult = "Doctor is created";
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
    internal class CreateDoctorAccountEmailTest1 : Test
    {
        public CreateDoctorAccountEmailTest1(TestController tc)
        {
            testController = tc;
            Id = "A1.Integration.CDA4";
            Description = "Create Doctor Account Existing Email";
            Steps = "Try to create a doctor account with the email of an existing doctor";
            Criteria = "Pass: Account is not created| Fail: account is created or exeption error";
            Inputstr = "All parameters plus the email of an existing doctor";
            Aresult = "";
            Eresult = "Doctor is not created";
        }

        public override TestViewModel Run()
        {
            TestViewModel model;

            //arrange
            bool Pass = false;
            DatabaseContext Tcontext = testController.getContext();
            DoctorController controller = new DoctorController(testController.getContext());
            int DoctorID = new int();
            Doctor doctor = Tcontext.Doctors.FirstOrDefault(x => x.DoctorId == DoctorID);
            var doctors = from d in Tcontext.Doctors select d;
            bool doctorExists = false;

            //loops untill doctor is found that exists 
            for (int i = -5; !doctorExists; i++)
            {
                DoctorID = i;
                doctor = Tcontext.Doctors.FirstOrDefault(p => p.DoctorId == DoctorID);
                if (doctor != null)
                {
                doctorExists = true;
                }
            }
            string firstName = "Rose";
            string lastName = "Jones";
            string eMail = doctor.Email;
            string phoneNumber = "06-24685344";
            string specialism = "Bone fractures";
            string localId = "2515T3";
            string userName = "RoseJ";
            string password = "Password";

            //act
            try
            {
                controller.SubmitDoctorAccount(firstName, lastName, eMail, phoneNumber, specialism, localId, userName, password);
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
            Doctor doc = Tcontext.Doctors.FirstOrDefault(u => u.UserName == userName);
            if (doc == null)
            {
                Aresult = "Doctor is not created";
                Pass = true;
            }
            else
            {
                Aresult = "Doctor is created";
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
      internal class CreateDoctorAccountUsernameTest1 : Test
    {
        public CreateDoctorAccountUsernameTest1(TestController tc)
        {
            testController = tc;
            Id = "A1.Integration.CDA5";
            Description = "Create Doctor Account Existing Username";
            Steps = "Try to create a doctor account with the username of an existing doctor";
            Criteria = "Pass: Account is not created| Fail: account is created or exeption error";
            Inputstr = "All parameters plus the username of an existing doctor";
            Aresult = "";
            Eresult = "Doctor is not created";
        }

        public override TestViewModel Run()
        {
            TestViewModel model;

            //arrange
            bool Pass = false;
            DatabaseContext Tcontext = testController.getContext();
            DoctorController controller = new DoctorController(testController.getContext());
            int DoctorID = new int();
            Doctor doctor = Tcontext.Doctors.FirstOrDefault(x => x.DoctorId == DoctorID);
            var doctors = from d in Tcontext.Doctors select d;
            bool doctorExists = false;

            //loops untill doctor is found that exists 
            for (int i = -5; !doctorExists; i++)
            {
                DoctorID = i;
                doctor = Tcontext.Doctors.FirstOrDefault(p => p.DoctorId == DoctorID);
                if (doctor != null)
                {
                doctorExists = true;
                }
            }
            string firstName = "Rose";
            string lastName = "Jones";
            string eMail = "RoseJ@email.com";
            string phoneNumber = "06-24685344";
            string specialism = "Bone fractures";
            string localId = "2515T3";
            string userName = doctor.UserName;
            string password = "Password";

            //act
            try
            {
                controller.SubmitDoctorAccount(firstName, lastName, eMail, phoneNumber, specialism, localId, userName, password);
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
            Doctor doc = Tcontext.Doctors.FirstOrDefault(u => u.Email == eMail);
            if (doc == null)
            {
                Aresult = "Doctor is not created";
                Pass = true;
            }
            else
            {
                Aresult = "Doctor is created";
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
    internal class LinkAdminLinkAlreadyMadeTest1 : Test
    {
        public LinkAdminLinkAlreadyMadeTest1(TestController tc)
        {
            testController = tc;
            Id = "A2.Integration.LAT1";
            Description = "Link Admin Link Already Made test 1";
            Steps = "Check if link is made, no changes to database";
            Criteria = "Pass: no changes to database | Fail: double link";
            Inputstr = "PatientID and doctorID where link is already made";
            Aresult = "";
            Eresult = "Link not doubled";
        }

        public override TestViewModel Run()
        {
            TestViewModel model;

            //arrange
            DatabaseContext Tcontext = testController.getContext();
            bool Pass = false;
            AdminController controller = new AdminController(testController.getContext());
            var patientdoctors = from d in Tcontext.PatientsDoctorss select d;
            PatientsDoctors patdoc = patientdoctors.First();

            if (patdoc == null){
                Pass = false;
                Aresult = "No links already made, make a link and perform the test again";
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

            int PatientID = patdoc.PatientId;
            int DocterID = patdoc.DoctorId;

            // act
            try
            {
                controller.SubmitLink(PatientID, DocterID);
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
            var patientsDoctors = from d in Tcontext.PatientsDoctorss where d.DoctorId == DocterID select d;
            Patient patient = Tcontext.Patients.FirstOrDefault(x => x.PatientId == PatientID);
            List<int> list = new List<int>();

            foreach (var item in patientsDoctors)
            {
                if (item.PatientId == patient.PatientId)
                {
                    list.Add(1);

                };
            }
            //check if the list of patientsdoctors contains the link twice
            if (list.Count() > 1)
            {
                Aresult = "Link doubled";
            }
            else
            {
                Aresult = "Link not doubled";
            }

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
    internal class LinkAdminLinkNotMadeTest1 : Test
    {
        public LinkAdminLinkNotMadeTest1(TestController tc)
        {
            testController = tc;
            Id = "A2.Integration.LAT2";
            Description = "Link Admin Link not made";
            Steps = "Check if link is made, if not, add link";
            Criteria = "Pass: link added in database | Fail: no link made";
            Inputstr = "PatientID and doctorID where link is not already made";
            Aresult = "";
            Eresult = "Link made";
        }

        public override TestViewModel Run()
        {
            TestViewModel model;

            //arrange
            bool Pass = false;
            DatabaseContext Tcontext = testController.getContext();
            AdminController controller = new AdminController(testController.getContext());
            int PatientID = -1;
            int DoctorID = -1;
            Doctor doctor = Tcontext.Doctors.FirstOrDefault(x => x.DoctorId == DoctorID);

            Patient patient = Tcontext.Patients.FirstOrDefault(p => p.PatientId == PatientID);
            var patients = from d in Tcontext.Patients select d;
            bool patientExists = patients.Contains(patient);

            PatientsDoctors patientsDoctors_ = Tcontext.PatientsDoctorss.FirstOrDefault(
            p => p.PatientId == PatientID && p.DoctorId == doctor.DoctorId);
            bool linkmade = Tcontext.PatientsDoctorss.Contains(patientsDoctors_);

            //loops untill patient is found where doc is not already linked
            for (int i = -3; linkmade; i++)
            {
                PatientID = i;
                patientsDoctors_ = Tcontext.PatientsDoctorss.FirstOrDefault(
                p => p.PatientId == PatientID && p.DoctorId == doctor.DoctorId);
                linkmade = Tcontext.PatientsDoctorss.Contains(patientsDoctors_);

                patient = Tcontext.Patients.FirstOrDefault(p => p.PatientId == PatientID);
            // if all patients are linked, the loop is stopped
                if (patient == null)
                {
                    break;
                }
            }
            //if all patients are linked, the results below are viewed
            if (patient == null)
            {
                Pass = false;
                Aresult = "All patients are linked to the doctor, could not perform test, create new account";
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
            //if there is a patient that is not already linked, run submitlink method
            try
            {
                controller.SubmitLink(PatientID, DoctorID);
            }
            //if for some reason we get an error, the error will be viewed 
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
            var patientsDoctors = from d in Tcontext.PatientsDoctorss where d.DoctorId == DoctorID select d;
            Patient patient_ = Tcontext.Patients.FirstOrDefault(x => x.PatientId == PatientID);

            //check if link is made in the database
            foreach (var item in patientsDoctors)
            {
                if (item.PatientId == patient_.PatientId)
                {
                    Aresult = "Link made for Patientid:" + patient_.PatientId + " and Doctorid:" + DoctorID;
                    Pass = true;
                }
                else
                {
                    Aresult = "Link not made";
                    Pass = false;
                }
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
     internal class LinkAdminPatNullTest1 : Test
    {
        public LinkAdminPatNullTest1(TestController tc)
        {
            testController = tc;
            Id = "A2.Integration.LAT3";
            Description = "Link Admin Patient is null";
            Steps = "Check if patient does not exist, try to create link";
            Criteria = "Pass: link is not made, warning to admin that patient does not exist| Fail: exeption error";
            Inputstr = "PatientID of a non existent patient, DoctorID of existing doctor  ";
            Aresult = "";
            Eresult = "Patient does not exist";
        }

        public override TestViewModel Run()
        {
            TestViewModel model;

            //arrange
            bool Pass = false;
            DatabaseContext Tcontext = testController.getContext();
            AdminController controller = new AdminController(testController.getContext());
            int PatientID = new int();
            int DoctorID = -1;
            Doctor doctor = Tcontext.Doctors.FirstOrDefault(x => x.DoctorId == DoctorID);

            Patient patient = Tcontext.Patients.FirstOrDefault(p => p.PatientId == PatientID);
            var patients = from d in Tcontext.Patients select d;
            bool patientExists = true;
            if (patient == null)
            {
                patientExists = false;
            }

            //loops untill patient is found that doesnt exist 
            for (int i = 1; patientExists; i++)
            {
                PatientID = i;
                patient = Tcontext.Patients.FirstOrDefault(p => p.PatientId == PatientID);
                if (patient == null)
                {
                patientExists = false;
                }
            }

            //try to run the submitlink method with a non existing patient 
            try
            {
                controller.SubmitLink(PatientID, DoctorID);
            }
            //if for some reason we get an error, the error will be viewed 
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
            var patientsDoctors = from d in Tcontext.PatientsDoctorss where d.DoctorId == DoctorID select d;
            Patient patient_ = Tcontext.Patients.FirstOrDefault(x => x.PatientId == PatientID);
            
            //check if link is made in the database
            foreach (var item in patientsDoctors)
            {   
                if (patientExists == true)
            {
                if (item.PatientId == patient_.PatientId)
                {
                    Aresult = "Link made for Patientid:" + patient_.PatientId + " and Doctorid:" + DoctorID;
                    Pass = false;
                }
            }
                else
                {
                    Aresult = "Link not made";
                    Pass = true;
                }
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
         internal class LinkAdminDocNullTest1 : Test
    {
        public LinkAdminDocNullTest1(TestController tc)
        {
            testController = tc;
            Id = "A2.Integration.LAT4";
            Description = "Link Admin Doctor is null";
            Steps = "Check if doctor does not exist, try to create link";
            Criteria = "Pass: link is not made, warning to admin that doctor does not exist| Fail: exeption error";
            Inputstr = "DoctorID of a non existent patient, PatientID of existing doctor  ";
            Aresult = "";
            Eresult = "Doctor does not exist";
        }

        public override TestViewModel Run()
        {
            TestViewModel model;

            //arrange
            bool Pass = false;
            DatabaseContext Tcontext = testController.getContext();
            AdminController controller = new AdminController(testController.getContext());
            int PatientID = -1;
            int DoctorID = new int();
            Doctor doctor = Tcontext.Doctors.FirstOrDefault(x => x.DoctorId == DoctorID);
            Patient patient = Tcontext.Patients.FirstOrDefault(p => p.PatientId == PatientID);

            var doctors = from d in Tcontext.Doctors select d;
            bool doctorExists = true;
            if (doctor == null){
                doctorExists = false;
            }


            //loops untill doctor is found that doesnt exist 
            for (int i = -3; doctorExists; i++)
            {
                DoctorID = i;
                doctor = Tcontext.Doctors.FirstOrDefault(p => p.DoctorId == DoctorID);
                if (doctor == null)
                {
                doctorExists = false;
                }
            }

            //try to run the submitlink method with a non existing doctor 
            try
            {
                controller.SubmitLink(PatientID, DoctorID);
            }
            //if for some reason we get an error, the error will be viewed 
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
            var patientsDoctors = from d in Tcontext.PatientsDoctorss where d.PatientId == PatientID select d;
            Doctor doctor_ = Tcontext.Doctors.FirstOrDefault(x => x.DoctorId == DoctorID);
            
            //check if link is made in the database
            foreach (var item in patientsDoctors)
            {   
                if (doctorExists == true)
            {
                if (item.DoctorId == doctor_.DoctorId)
                {
                    Aresult = "Link made for Patientid:" + PatientID + " and Doctorid:" + doctor_.DoctorId;
                    Pass = false;
                }
            }
                else
                {
                    Aresult = "Link not made";
                    Pass = true;
                }
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
    internal class CreatePatientAccountTest1 : Test
    {
        public CreatePatientAccountTest1(TestController tc)
        {
            testController = tc;
            Id = "P1.Integration.CPA1";
            Description = "Create Patient Account Test 1";
            Steps = "Create a patient with all values inserted";
            Criteria = "Pass: patient is created in the database | Fail: exception error";
            Inputstr = "All parameters for a patient";
            Aresult = "";
            Eresult = "Patient added to database";
        }

        public override TestViewModel Run()
        {
            TestViewModel model;

            //arrange
            bool Pass = false;
            PatientController controller = new PatientController(testController.getContext());

            string firstName = "Harry";
            string lastName = "Jones";
            string eMail = "HarryJ@email.com";
            string phoneNumber = "06-24685344";
            string userName = "UserAT1";
            string password = "password12342";

            //act
            try
            {
                controller.SubmitPatientAccount(firstName, lastName, eMail, phoneNumber, userName, password);
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
            DatabaseContext Tcontext = testController.getContext();
            Patient pat = Tcontext.Patients.FirstOrDefault(u => u.UserName == userName);
            if (pat == null)
            {
                Aresult = "Patient not added to database";
                Pass = false;
            }
            else
            {
                Aresult = "Patient added to database";
                Pass = true;
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
    internal class CreatePatientAccountMissingParameterTest1 : Test
    {
        public CreatePatientAccountMissingParameterTest1(TestController tc)
        {
            testController = tc;
            Id = "P1.Integration.CPA2";
            Description = "Create patient account with a missing parameter";
            Steps = "Create a patient account with a parameter missing";
            Criteria = "Pass: patient is not created | Fail: patient is created or exeption error";
            Inputstr = "All parameters except email";
            Aresult = "";
            Eresult = "Patient is not created";
        }

        public override TestViewModel Run()
        {
            TestViewModel model;

            //arrange
            bool Pass = false;
            PatientController controller = new PatientController(testController.getContext());

            string firstName = "Haley";
            string lastName = "Jones";
            string eMail = null;
            string phoneNumber = "06-24685344";
            string userName = "HaleyJ";
            string password = "password";

            //act
            try
            {
                controller.SubmitPatientAccount(firstName, lastName, eMail, phoneNumber, userName, password);
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
            DatabaseContext Tcontext = testController.getContext();
            Patient pat = Tcontext.Patients.FirstOrDefault(u => u.UserName == userName);
            if (pat == null)
            {
                Aresult = "Patient is not created";
                Pass = true;
            }
            else
            {
                Aresult = "Patient is created";
                Pass= false;
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
    internal class CreatePatientAccountPasswordtest1 : Test
    {
        public CreatePatientAccountPasswordtest1(TestController tc)
        {
            testController = tc;
            Id = "P1.Integration.CPA3";
            Description = "Create Patient Account with password less than 8 characters";
            Steps = "Try to create a patient account with a password less than 8 characters long";
            Criteria = "Pass: patient is not created | Fail: patient is created";
            Inputstr = "All parameters entered with a password with less than 8 characters";
            Aresult = "";
            Eresult = "Patient is not created";
        }

        public override TestViewModel Run()
        {
            TestViewModel model;

            //arrange
            bool Pass = false;
            PatientController controller = new PatientController(testController.getContext());

            string firstName = "Kim";
            string lastName = "Jones";
            string eMail = "KimmJJ@email.com";
            string phoneNumber = "06-24685344";
            string userName = "KimJ";
            string password = "passw";

            //act
            try
            {
                controller.SubmitPatientAccount(firstName, lastName, eMail, phoneNumber, userName, password);
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
            DatabaseContext Tcontext = testController.getContext();
            Patient pat = Tcontext.Patients.FirstOrDefault(u => u.UserName == userName);
            if (pat == null)
            {
                Aresult = "Patient is not created";
                Pass = true;
            }
            else
            {
                Aresult = "Patient is created";
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
    internal class CreatePatientAccountEmailTest1 : Test
    {
        public CreatePatientAccountEmailTest1(TestController tc)
        {
            testController = tc;
            Id = "P1.Integration.CPA4";
            Description = "Create Patient Account Existing Email";
            Steps = "Try to create a patient account with the email of an existing patient";
            Criteria = "Pass: Account is not created| Fail: account is created or exeption error";
            Inputstr = "All parameters plus the email of an existing patient";
            Aresult = "";
            Eresult = "Patient is not created";
        }

        public override TestViewModel Run()
        {
            TestViewModel model;

            //arrange
            bool Pass = false;
            DatabaseContext Tcontext = testController.getContext();
            PatientController controller = new PatientController(testController.getContext());
            int PatientID = new int();
            Patient patient = Tcontext.Patients.FirstOrDefault(x => x.PatientId == PatientID);
            var patients = from d in Tcontext.Patients select d;
            bool patientExists = false;

            //loops untill doctor is found that exists 
            for (int i = -5; !patientExists; i++)
            {
                PatientID = i;
                patient = Tcontext.Patients.FirstOrDefault(p => p.PatientId == PatientID);
                if (patient != null)
                {
                patientExists = true;
                }
            }
            string firstName = "Rose";
            string lastName = "Jones";
            string eMail = patient.Email;
            string phoneNumber = "06-24685344";
            string userName = "RoseJ";
            string password = "Password";

            //act
            try
            {
                controller.SubmitPatientAccount(firstName, lastName, eMail, phoneNumber, userName, password);
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
            Patient pat = Tcontext.Patients.FirstOrDefault(u => u.UserName == userName);
            if (pat == null)
            {
                Aresult = "Patient is not created";
                Pass = true;
            }
            else
            {
                Aresult = "Patient is created";
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
      internal class CreatePatientAccountUsernameTest1 : Test
    {
        public CreatePatientAccountUsernameTest1(TestController tc)
        {
            testController = tc;
            Id = "P1.Integration.CPA5";
            Description = "Create Patient Account Existing Username";
            Steps = "Try to create a patient account with the username of an existing patient";
            Criteria = "Pass: Account is not created| Fail: account is created or exeption error";
            Inputstr = "All parameters plus the username of an existing patient";
            Aresult = "";
            Eresult = "Patient is not created";
        }

        public override TestViewModel Run()
        {
            TestViewModel model;

            //arrange
            bool Pass = false;
            DatabaseContext Tcontext = testController.getContext();
            PatientController controller = new PatientController(testController.getContext());
            int PatientID = new int();
            Patient patient = Tcontext.Patients.FirstOrDefault(x => x.PatientId == PatientID);
            var patients = from d in Tcontext.Patients select d;
            bool patientExists = false;

            //loops untill doctor is found that exists 
            for (int i = -5; !patientExists; i++)
            {
                PatientID = i;
                patient = Tcontext.Patients.FirstOrDefault(p => p.PatientId == PatientID);
                if (patient != null)
                {
                patientExists = true;
                }
            }
            string firstName = "Rose";
            string lastName = "Jones";
            string eMail = "RoseJ@email.com";
            string phoneNumber = "06-24685344";
            string userName = patient.UserName;
            string password = "Password";

            //act
            try
            {
                controller.SubmitPatientAccount(firstName, lastName, eMail, phoneNumber, userName, password);
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
            Patient pat = Tcontext.Patients.FirstOrDefault(u => u.Email == eMail);
            if (pat == null)
            {
                Aresult = "Patient is not created";
                Pass = true;
            }
            else
            {
                Aresult = "Patient is created";
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
          internal class AddLocalIdToPatientTest1 : Test
    {
        public AddLocalIdToPatientTest1(TestController tc)
        {
            testController = tc;
            Id = "D7.Integration.ALP1";
            Description = "Doctor Adds Local Id to patient account";
            Steps = "Doctor adds a local id to a patient account";
            Criteria = "Pass: local id is added| Fail: local id is not added or exeption error";
            Inputstr = "A local id as string";
            Aresult = "";
            Eresult = "Local id is added";
        }

        public override TestViewModel Run()
        {
            TestViewModel model;

            //arrange
            bool Pass = false;
            DatabaseContext Tcontext = testController.getContext();
            DoctorController controller = new DoctorController(testController.getContext());
            var patientdoctors = from d in Tcontext.PatientsDoctorss select d;
            PatientsDoctors patdoc = patientdoctors.First(); 
            int patientid = patdoc.PatientId;
            Patient pat = Tcontext.Patients.FirstOrDefault(u => u.PatientId == patientid);
            if (pat.LocalId == null)
            {
                pat.LocalId = new List<string>();
                Tcontext.SaveChanges();
            }
            string LocalID = "LocalIdtest1";
            var localids = pat.LocalId;

            bool localidExists = false;

            if (localids != null)
            {
                foreach (var item in localids)
                {
                    if (item == LocalID)
                    {
                        localidExists = true;
                        Pass = false;
                        Aresult = "Local id already exists, try another one";
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
           

            //act
            try
            {
                controller.AddLocalId( patientid,  LocalID);
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

            foreach (var item in localids)
                {
                    if (item == LocalID)
                    {
                        localidExists = true;
                        
                    }
                }

            if (localidExists == true)
            {
                Aresult = "Local id added";
                Pass = true;
            }
            else
            {
                Aresult = "Local id not added";
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
         internal class AddLocalIdNullToPatientTest1 : Test
    {
        public AddLocalIdNullToPatientTest1(TestController tc)
        {
            testController = tc;
            Id = "D7.Integration.ALP2";
            Description = "Doctor Adds Local Id to patient account with local id being NULL";
            Steps = "Doctor adds a local id equal to NULL to a patient account";
            Criteria = "Pass: local id is not added| Fail: local id is added or exeption error";
            Inputstr = "A local id equal to NULL";
            Aresult = "";
            Eresult = "Local id is not added";
        }

        public override TestViewModel Run()
        {
            TestViewModel model;

            //arrange
            bool Pass = false;
            DatabaseContext Tcontext = testController.getContext();
            DoctorController controller = new DoctorController(testController.getContext());
            var patientdoctors = from d in Tcontext.PatientsDoctorss select d;
            PatientsDoctors patdoc = patientdoctors.First(); 
            int patientid = patdoc.PatientId;
            Patient pat = Tcontext.Patients.FirstOrDefault(u => u.PatientId == patientid);
            string LocalID = null;
            
            if (pat.LocalId == null)
            {
                pat.LocalId = new List<string>();
                Tcontext.SaveChanges();

            }
            var localids = pat.LocalId;
            bool localidExists = false;

            //act
            try
            {
                controller.AddLocalId( patientid,  LocalID);
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

            foreach (var item in localids)
                {
                    if (item == LocalID)
                    {
                        localidExists = true;
                        
                    }
                }

            if (localidExists == true)
            {
                Aresult = "Local id added";
                Pass = false;
            }
            else
            {
                Aresult = "Local id not added";
                Pass = true;
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

             internal class AddLocalIdAlreadyExistsToPatientTest1 : Test
    {
        public AddLocalIdAlreadyExistsToPatientTest1(TestController tc)
        {
            testController = tc;
            Id = "D7.Integration.ALP3";
            Description = "Doctor Adds Local Id to patient account with local id already added";
            Steps = "Doctor adds a local id that is already added to a patient account";
            Criteria = "Pass: local id is not added| Fail: local id is added or exeption error";
            Inputstr = "A local id that is already added";
            Aresult = "";
            Eresult = "Local id is not added";
        }

        public override TestViewModel Run()
        {
            TestViewModel model;

            //arrange
            bool Pass = false;
            DatabaseContext Tcontext = testController.getContext();
            DoctorController controller = new DoctorController(testController.getContext());
            var patientdoctors = from d in Tcontext.PatientsDoctorss select d;
            PatientsDoctors patdoc = patientdoctors.First(); 
            int patientid = patdoc.PatientId;
            Patient pat = Tcontext.Patients.FirstOrDefault(u => u.PatientId == patientid);
            string LocalID = "";
            var localids = pat.LocalId;


            if (pat.LocalId == null || pat.LocalId.Contains(""))
            {
                pat.LocalId = new List<String>();
                pat.LocalId.Add("LocalId1");
                Tcontext.SaveChanges();
                LocalID = "LocalIdTEST1";
            }


            if (LocalID == null)
            {
                Pass = false;
                Aresult = "No local ids exist, can't perform test";
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
            //act
            try
            {
                controller.AddLocalId( patientid,  LocalID);
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

            List<int> count = new List<int>();

            foreach (var item in localids)
                {
                    if (item == LocalID)
                    {
                        count.Add(1);
                    }
                }

            if (count.Count() > 1)
            {
                Aresult = "Local id added again";
                Pass = false;
            }
            else
            {
                Aresult = "Local id not added again";
                Pass = true;
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
