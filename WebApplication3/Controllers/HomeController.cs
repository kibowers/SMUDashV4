﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.WebPages;
using System.IO;
using System.Data.Entity.Migrations;

namespace WebApplication3.Controllers
{
    public class HomeController : Controller
    {


        Model1 db = new Model1();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }



        /** test view is a sample view for me to throw random shit at and see if it works
         */

        public ActionResult TestView()
        {
            return View();

        }


        public ActionResult StudentLogin()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult StudentLogin(student objUser)
        {
            if (ModelState.IsValid)
            {

                var obj = db.students.Where(a => a.StudentEmail.Equals(objUser.StudentEmail) && a.StudentPassword.Equals(objUser.StudentPassword)).FirstOrDefault();
                if (obj != null)
                {
                    Session["StudentEmail"] = obj.StudentEmail.ToString();
                    Session["StudentPassword"] = obj.StudentPassword.ToString();
                    Session["StudentFirstName"] = obj.StudentFirstName.ToString();
                    Session["StudentLastName"] = obj.StudentLastName.ToString();
                    Session["StudentID"] = obj.StudentID.ToString();


                    return RedirectToAction("PeerEval");
                }

            }
            return View(objUser);
        }

        public ActionResult EvalSheet()
        {
            if (Session["StudentEmail"] != null)
            {
                return View();


            }
            else
            {
                return RedirectToAction("StudentLogin");
            }
        }



        public ActionResult ProfLogin()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProfLogin(professor objUser)
        {
            if (ModelState.IsValid)
            {
                var obj = db.professors.Where(a => a.ProfEmail.Equals(objUser.ProfEmail) && a.ProfPassword.Equals(objUser.ProfPassword)).FirstOrDefault();
                if (obj != null)
                {
                    Session["ProfEmail"] = obj.ProfEmail.ToString();
                    Session["ProfID"] = obj.ProfID;
                    var profinfo = obj.ProfFirstName + " " + obj.ProfLastName;
                    Session["ProfName"] = profinfo;
                    return RedirectToAction("ProfHub");
                }

            }
            return View(objUser);
        }

        public ActionResult ProfDashboard()
        {


            var scheduleevalsdb = db.scheduleEvals;
            return View(scheduleevalsdb.ToList());

        }

        public ActionResult SortScheduleAsc()
        {
            return View();
        }

        public ActionResult PeerEval()
        {

            ListClasses();

            return View();

        }


        [ValidateAntiForgeryToken]
        [HttpPost]

        public ActionResult PeerEval(evaluation eval)
        {
            if (ModelState.IsValid)
            {
                int CriticalThinkingProblemSolving_Score = Request["CriticalThinkingProblemSolving_Score"].AsInt();
                int InnovativeEntrepreneurialSkills_Score = Request.Form["InnovativeEntrepreneurialSkills_Score"].AsInt();
                int CollaborationLeadership_Score = Request.Form["CollaborationLeadership_Score"].AsInt();
                int Communication_Score = Request.Form["Communication_Score"].AsInt();
                int Intercultural_Score = Request.Form["Intercultural_Score"].AsInt();
                int DevelopmentInAsia_Score = Request.Form["DevelopmentInAsia_Score"].AsInt();
                int EthicsSocialResponsibility_Score = Request.Form["EthicsSocialResponsibility_Score"].AsInt();
                int ResiliencePositivity_Score = Request.Form["ResiliencePositivity_Score"].AsInt();
                int SelfdirectednessMetalearning_Score = Request.Form["SelfdirectednessMetalearning_Score"].AsInt();
                int Knowledge_Score = Request.Form["Knowledge_Score"].AsInt();
                int stuID = Convert.ToInt32(Session["StudentID"]);
                var compTime = DateTime.Now; //note to self: parentheses seem to indicate the method version w parameters, even if empty
                var stuRecID = Request.Form["hiddenInfo"].AsInt();


                Console.WriteLine(CriticalThinkingProblemSolving_Score);


                evaluation submittedEval = new evaluation
                {
                    CriticalThinkingProblemSolving_Score = CriticalThinkingProblemSolving_Score,
                    InnovativeEntrepreneurialSkills_Score = InnovativeEntrepreneurialSkills_Score,
                    CollaborationLeadership_Score = CollaborationLeadership_Score,
                    Communication_Score = Communication_Score,
                    Intercultural_Score = Intercultural_Score,
                    DevelopmentInAsia_Score = DevelopmentInAsia_Score,
                    EthicsSocialResponsibility_Score = EthicsSocialResponsibility_Score,
                    ResiliencePositivity_Score = ResiliencePositivity_Score,
                    SelfdirectednessMetalearning_Score = SelfdirectednessMetalearning_Score,
                    Knowledge_Score = Knowledge_Score,
                    CompletionTime = compTime,
                    ScheduleID = 33,


                    /* the fields below are not controlled via the form */
                    StudentIDWriter = stuID,
                    StudentIDReceiver = stuRecID

                };

                db.evaluations.Add(submittedEval);

                Session["CriticalThinkingProblemSolving_Score"] = submittedEval.CriticalThinkingProblemSolving_Score;
                Session["InnovativeEntrepreneurialSkills_Score"] = submittedEval.InnovativeEntrepreneurialSkills_Score;
                Session["CollaborationLeadership_Score"] = submittedEval.CollaborationLeadership_Score;
                Session["Communication_Score"] = submittedEval.Communication_Score;
                Session["Intercultural_Score"] = submittedEval.Intercultural_Score;
                Session["DevelopmentInAsia_Score"] = submittedEval.DevelopmentInAsia_Score;
                Session["EthicsSocialResponsibility_Score"] = submittedEval.EthicsSocialResponsibility_Score;
                Session["ResiliencePositivity_Score"] = submittedEval.ResiliencePositivity_Score;
                Session["SelfdirectednessMetalearning_Score"] = submittedEval.SelfdirectednessMetalearning_Score;
                Session["Knowledge_Score"] = submittedEval.Knowledge_Score;

                db.SaveChanges();
                return RedirectToAction("evalComplete");
            }
            ListClasses();
            return View();

        }





        public List<course> peerevalstudentList()
        {

            List<course> thing = new List<course>();
            var stuID = Convert.ToInt32(Session["StudentID"]) + 1;


            using (Model1 db = new Model1())
            {
                var listContent = db.Course_Student.Where(a => a.StudentID.Equals(stuID)).ToList();
                if (listContent != null)
                {

                    foreach (var cID in listContent)
                    {

                        thing = db.courses.Where(a => a.CourseID.Equals(cID.CourseID)).ToList();

                        foreach (var item in thing)
                        {
                            Console.WriteLine(item.CourseTitle);
                            System.Diagnostics.Debug.WriteLine(item.CourseTitle);
                        }


                    }




                }
            }

            return thing;


        }

        [HttpGet]
        public ActionResult ListClasses()
        {
            var stuID = Convert.ToInt32(Session["StudentID"]);
            var listContent = db.Course_Student.Where(a => a.StudentID.Equals(stuID)).ToList();

            List<course> sList = new List<course>();

            foreach (var cId in listContent)
            {
                var filler = db.courses.Where(a => a.CourseID.Equals(cId.CourseID)).ToList();

                foreach (var coursename in filler)
                {
                    var coursecontent = coursename;
                    sList.Add(coursecontent);
                }

            }



            sList.Insert(0, new course { CourseID = 0, CourseTitle = "Please select a course." });
            var viewModel = new SelectStudentCourseViewModel { Courses = sList };

            return View(viewModel);


        }



        [HttpGet]
        public ActionResult GetDropdownList(int input)
        {
            List<Student_Grouper> stinkyFinder = new List<Student_Grouper>();
            List<student> stuFinder = new List<student>();
            List<SelectListItem> fine = new List<SelectListItem>();
            List<grouper> grouplist = new List<grouper>();
            var stuID = Convert.ToInt32(Session["StudentID"]);
            List<int> studentIdcollector = new List<int>();

            //attempt to retrieve the groupers objects with the same course IDs, this will give access
            // to the group IDs which we can use to retrieve info from student grouper 

            var grouplister = db.groupers.Where(a => a.CourseID.Value.Equals(input)).ToList();


            //list for every group the student is in. We have group id's here. 
            var yourGroups = db.Student_Grouper.Where(a => a.StudentID.Value.Equals(stuID)).ToList();


            foreach (var groupObj in yourGroups)
            {
                var groupfinder = db.groupers.Where(a => a.GroupID == groupObj.GroupID).ToList();


                foreach (var groupinfo in groupfinder)
                {
                    if (groupinfo.CourseID == input)
                    {
                        grouplist.Add(groupinfo);
                    }
                }
            }
            foreach (var groupa in grouplist)
            {
                var finder = db.Student_Grouper.Where(a => a.GroupID.Value.Equals(groupa.GroupID)).ToList();
                foreach (var v in finder)
                {
                    studentIdcollector.Add((int)v.StudentID);
                }
            }

            foreach (var stuid in studentIdcollector)
            {
                System.Diagnostics.Debug.WriteLine(stuid);

                var stuSearcher = db.students.Where(a => a.StudentID.Equals(stuid)).FirstOrDefault();

                if (stuSearcher != null)
                {
                    stuFinder.Add(stuSearcher);
                }

            }


            return Json(stuFinder, JsonRequestBehavior.AllowGet);




        }



        public ActionResult evalComplete()
        {
            return View();
        }


        public ActionResult StudentResetPass()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult StudentResetPass(student stu)
        {

            var stuEmail = Request.Form["email1"];
            var stuFirst = Request.Form["firstname1"];

            Console.WriteLine(stuEmail);
            Console.WriteLine(stuFirst);


            if (ModelState.IsValid)
            {
                var stusearcher = db.students.Where(a => a.StudentEmail.Equals(stu.StudentEmail) && a.StudentFirstName.Equals(stu.StudentFirstName)).FirstOrDefault();

                if (stusearcher != null)
                {

                    stusearcher.StudentPassword = stu.StudentPassword;



                    db.students.Attach(stusearcher);
                    var entry = db.Entry(stusearcher);
                    entry.Property(e => e.StudentPassword).IsModified = true;
                    db.SaveChanges();
                    return RedirectToAction("StudentLogin");

                }


            }
            return View();
        }

        public ActionResult StudentRegister()
        {
            return View();
        }

        [HttpPost]
        public ActionResult StudentRegister(student stu)
        {

            if (ModelState.IsValid)
            {

                Debug.WriteLine(stu.StudentFirstName);
                Debug.WriteLine(stu.StudentLastName);


                student mark = new student
                {
                    StudentFirstName = stu.StudentFirstName,
                    StudentLastName = stu.StudentLastName,
                    StudentEmail = stu.StudentEmail,
                    StudentPassword = stu.StudentPassword
                };
                db.students.Add(mark);
                db.SaveChanges();
                return RedirectToAction("StudentLogin");
            }

            return View();
        }


        public ActionResult LogOutStudent()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            return RedirectToAction("StudentLogin");
        }

        public ActionResult LogOutProf()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            return RedirectToAction("ProfLogin");
        }


        public ActionResult CreateSchedule()
        {
            GetCourseDropdown();
            return View();
        }

        [HttpPost]
        public ActionResult CreateSchedule(scheduleEval scheduledEval)
        {

            if (ModelState.IsValid)
            {
                var scheduleTime = Request.Form["scheduleTime"];

                var converttime = Convert.ToDateTime(scheduleTime);

                Debug.WriteLine(converttime);
                var classid = Request.Form["hiddenInfo"].AsInt();
                Debug.WriteLine(classid);

                var theEval = new scheduleEval
                {
                    CourseID = classid,
                    DueDate = converttime
                };



                db.scheduleEvals.Add(theEval);
                db.SaveChanges();






                return RedirectToAction("SubmittedSchedule");
            }
            return View();



        }


        public ActionResult SubmittedSchedule()
        {
            return View(); 
        }


        [HttpGet]
        public ActionResult GetCourseDropdown()
        {

            var profidentifier = (int?)Convert.ToInt32(Session["ProfID"]);
            var coursecompounder = db.Professor_Course.Where(a => a.ProfID == profidentifier).ToList();

            List<course> courselister = new List<course>();
            if (coursecompounder != null)
            {
                foreach (var coursefinder in coursecompounder)
                {
                    var thing = db.courses.Where(a => a.CourseID == coursefinder.CourseID).ToList();
                    foreach (var coursename in thing)
                    {
                        var coursefound = coursename;
                        courselister.Add(coursefound);
                    }
                }
            }
            courselister.Insert(0, new course { CourseID = 0, CourseTitle = "Please select a course." });
            var viewModel = new SelectStudentCourseViewModel { Courses = courselister };

            return View(viewModel);

        }


        public ActionResult ProfResetPass()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProfResetPass(professor prof)
        {

            var stuEmail = Request.Form["email1"];
            var stuFirst = Request.Form["firstname1"];

            Console.WriteLine(stuEmail);
            Console.WriteLine(stuFirst);


            if (ModelState.IsValid)
            {
                var profsearcher = db.professors.Where(a => a.ProfEmail.Equals(prof.ProfEmail) && prof.ProfFirstName.Equals(prof.ProfFirstName)).FirstOrDefault();

                if (profsearcher != null)
                {

                    profsearcher.ProfPassword = prof.ProfPassword;



                    db.professors.Attach(profsearcher);
                    var entry = db.Entry(profsearcher);
                    entry.Property(e => e.ProfPassword).IsModified = true;
                    db.SaveChanges();
                    return RedirectToAction("ProfLogin");

                }


            }
            return View();
        }

        [HttpGet]

        public ActionResult ProfHub()
        {
            return View();
        }



        [HttpGet]
        public ActionResult ImportCourses()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ImportCourses(HttpPostedFileBase file, course courser)
        {


            //duplicate course protection. 
            List<student> stufindah = new List<student>();
            var cc = courser.CourseCode;
            var ct = courser.CourseTitle;
            var cy = (int?)Convert.ToInt32(Request.Form["courseYear"]);
            var cterm = Request.Form["hiddenInfo"];
            if (ModelState.IsValid)
            {

                var theCourse = new course
                {
                    CourseID = default,
                    CourseCode = cc,
                    CourseTitle = ct,
                    CourseYear = cy,
                    CourseTerm = cterm
                };


                



                db.courses.AddOrUpdate(theCourse);
                db.SaveChanges();


                Session["CreatedCourseTitle"] = theCourse.CourseTitle;
                Session["CreatedCourseCode"] = theCourse.CourseCode;
                Session["CreatedCourseYear"] = theCourse.CourseYear;
                Session["CreatedCourseTerm"] = theCourse.CourseTerm;

                var thingid = theCourse.CourseID;
                Debug.WriteLine(thingid);

                var profidStorage = Convert.ToInt32(Session["ProfID"]);

                var theProfDec = new Professor_Course
                {
                    SectionID = default, 
                    ProfID = profidStorage,
                    CourseID = thingid

                };

                db.Professor_Course.AddOrUpdate(theProfDec);
                db.SaveChanges();

                //storing the courseid of the created course in a local variable for future use 





                /**module attempting to scrape  information for course */
                /** basically two separate parts. The one above is formally creating the course object.
                 * The below one takes the supplied csv and creates course_student objects based on the assumption
                 * that the students have accounts */



                /* check to see if the course w/ identical exists before POSTing entry */

                if (file.ContentLength > 0 && file != null)
                {
                    try
                    {


                        string name = Path.GetFileName(file.FileName);
                        string path = Path.Combine(Server.MapPath("~/Uploads"), name);
                        file.SaveAs(path);


                        ViewBag.Message = "File Uploaded Successfully!";

                        var lineNumber = 0;
                        using (StreamReader reader = new StreamReader(@path))
                        {
                            while (!reader.EndOfStream)
                            {
                                var line1 = reader.ReadLine();

                                if (lineNumber != 0)
                                {
                                    var vals = line1.Split(',');


                                    var thing1 = vals[0];
                                    var thing2 = vals[1];

                                    //search the db table to find students with identical first and last names
                                    var stinky = db.students.Where(a => a.StudentFirstName.Equals(thing2) && a.StudentLastName.Equals(thing1)).FirstOrDefault();

                                    //add student object to local list 
                                    stufindah.Add(stinky);
                                }
                                lineNumber++;


                            }

                            if (reader.EndOfStream)
                            {
                                foreach (var foundstu in stufindah)
                                {
                                    var newStu = new Course_Student
                                    {
                                        EnrollmentID = default,
                                        CourseID = thingid,
                                        StudentID = foundstu.StudentID

                                    };

                                    db.Entry(newStu).State = System.Data.Entity.EntityState.Modified;



                                    db.Course_Student.AddOrUpdate(newStu);







                                }
                                db.SaveChanges();

                                Session["CreatedCourseStudents"] = stufindah;


                            }


                        }
                    }
                    catch(Exception ex)
                    {
                        ViewBag.Message = "Double check your inputs.";
                    }

                    return RedirectToAction("courseCreated");
                }









            }
            return View();
        }
    
    
    
    
    
    
        [HttpGet]
        public ActionResult CourseCreated()
        {
            return View();

        }





        [HttpGet]
        public ActionResult GetCourseDropdownGroups()
        {

            var profidentifier = (int?)Convert.ToInt32(Session["ProfID"]);
            var coursecompounder = db.Professor_Course.Where(a => a.ProfID == profidentifier).ToList();

            List<course> courselister = new List<course>();
            if (coursecompounder != null)
            {
                foreach (var coursefinder in coursecompounder)
                {
                    var thing = db.courses.Where(a => a.CourseID == coursefinder.CourseID).ToList();
                    foreach (var coursename in thing)
                    {
                        var coursefound = coursename;
                        courselister.Add(coursefound);
                    }
                }
            }
            courselister.Insert(0, new course { CourseID = 0, CourseTitle = "Please select a course." });
            var viewModel = new StudentGroupCreationViewModel { Courses = courselister };

            return View(viewModel);

        }

        [HttpGet]
        public ActionResult CreateStudentGroups()
        {
            GetCourseDropdownGroups();


            //var scheduleevalsdb = db.scheduleEvals;
            //return View(scheduleevalsdb.ToList());




            return View();
        }




        [HttpGet]
        public ActionResult GetTableInfo(int input)
        {
            /* List<Student_Grouper> stinkyFinder = new List<Student_Grouper>();
            List<student> stuFinder = new List<student>();
            List<SelectListItem> fine = new List<SelectListItem>();
            List<grouper> grouplist = new List<grouper>();
            var stuID = Convert.ToInt32(Session["StudentID"]);
            List<int> studentIdcollector = new List<int>();
            */
            //attempt to retrieve the groupers objects with the same course IDs, this will give access
            // to the group IDs which we can use to retrieve info from student grouper 
            /*
            var grouplister = db.groupers.Where(a => a.CourseID.Value.Equals(input)).ToList();


            //list for every group the student is in. We have group id's here. 
            var yourGroups = db.Student_Grouper.Where(a => a.StudentID.Value.Equals(stuID)).ToList();


            foreach (var groupObj in yourGroups)
            {
                var groupfinder = db.groupers.Where(a => a.GroupID == groupObj.GroupID).ToList();


                foreach (var groupinfo in groupfinder)
                {
                    if (groupinfo.CourseID == input)
                    {
                        grouplist.Add(groupinfo);
                    }
                }
            }
            foreach (var groupa in grouplist)
            {
                var finder = db.Student_Grouper.Where(a => a.GroupID.Value.Equals(groupa.GroupID)).ToList();
                foreach (var v in finder)
                {
                    studentIdcollector.Add((int)v.StudentID);
                }
            }

            foreach (var stuid in studentIdcollector)
            {
                System.Diagnostics.Debug.WriteLine(stuid);

                var stuSearcher = db.students.Where(a => a.StudentID.Equals(stuid)).FirstOrDefault();

                if (stuSearcher != null)
                {
                    stuFinder.Add(stuSearcher);
                }

            }


            return Json(stuFinder, JsonRequestBehavior.AllowGet);

            */


            //going to course_student to retrieve the list of students who are taking the selected course
            var courseLister = db.Course_Student.Where(a => a.CourseID.Equals(input)).ToList();


            List<student> stufinder = new List<student>();
           
            foreach(var coursestu in courseLister)
            {
                var findah = db.students.Where(a => a.StudentID.Equals(coursestu.StudentID)).FirstOrDefault();

                if (findah != null)
                {
                    stufinder.Add(findah);
                }
            }

            //now we have the list of student objects to return, but how will we return these???

            return Json(stufinder.ToList(), JsonRequestBehavior.AllowGet);

        }



    }



}


    

        /**
        public Microsoft.AspNetCore.Mvc.IActionResult Create()
        {
            
                IEnumerable<string> hList = from char s in db.Course_Student select s.ToString();

                List<course> hUsers = new List<course>();
                hUsers = (from c in db.courses select c).ToList();
                hUsers.Insert(0, new course { CourseID =0 , CourseTitle = "t" });
                ViewBag.historyUsers = hUsers;

            

                List<SelectListItem> options = new List<SelectListItem>
                {
                    new SelectListItem { Value = "True", Text = "Yes" },
                    new SelectListItem { Value = "False", Text = "No" }
                };
                ViewBag.options = options;


            foreach(var item in hUsers)
            {
                System.Diagnostics.Debug.WriteLine(item.CourseTitle);

            }

            var thing = new SelectList(ViewBag.historyUsers, "CourseID", "CourseTitle");
            ViewBag.idea = thing;

            return (Microsoft.AspNetCore.Mvc.IActionResult)View();

                
            
        }


        public SelectList PYSiteSL { get; set; }

        public void PopulateSiteDropDownList(Model1 db, object selectedSiteList = null)
        {
            var siteQuery = from s in db.courses
                            select s.CourseTitle;
            var PYSiteSL = new SelectList(siteQuery, selectedSiteList);
        }


        public ActionResult OnGet()
        {
            PopulateSiteDropDownList(db, PYSiteSL);

            return View();
        }








        /** public Microsoft.AspNetCore.Mvc.IActionResult CreateCourseList()
        {
            Model1 db = new Model1();
            var stuID = Convert.ToInt32(Session["StudentID"]) + 1;

            IEnumerable<string> theList = (IEnumerable<string>)(from s in db.Course_Student
                                                                where s.StudentID == stuID
                                                                select s);

            List<Course_Student> studentCourses = new List<Course_Student>();
            studentCourses = (from d in db.Course_Student select d).ToList();
            studentCourses.Insert(0, new Course_Student { CourseID = 0, StudentID = stuID, EnrollmentID = 8 });
            ViewBag.courseList = studentCourses;


            List<SelectListItem> options = new List<SelectListItem>
            {
                new SelectListItem { Value = "True", Text = "Yes" },
                new SelectListItem { Value = "False", Text = "No" }
            };

            ViewBag.options = options;
            return (Microsoft.AspNetCore.Mvc.IActionResult)View();
        }
        */



        //we need a dropdown to allow the student to select from the list of class they have first, then filter on that class idea at a later point



        //we need a dropdown to allow the student to select from the list of class they have first, then filter on that class idea at a later point
        /**
         * public ActionResult CourseDropdown()
        {

            Model1 db = new Model1();



            var selector = db.Course_Student.Where(a => a.StudentID.Equals(Session["StudentID"])).ToList();
            var stumod = new SelectStudentCourseViewModel();

            foreach (var availableClass in selector)
            {

                stumod.course.CourseID = availableClass.CourseID;
                stumod.StudentID = availableClass.StudentID;

                var classesStudentsTaking = db.courses.Where(a => a.CourseID.Equals(stumod.CourseID)).ToList();

                foreach (var classAttending in classesStudentsTaking)
                {
                    stumod.CourseTitle = classAttending.CourseTitle;

                    stumod.studentCourses = db.courses.Select(a => new SelectListItem
                    {
                        Value = a.CourseID.ToString(),
                        Text = a.CourseTitle

                    });
                }


            }

            return View(stumod);
        }


        
         *   public List<course> CourseDropdown()
        {

            Model1 db = new Model1();
            List<course> result = new List<course>();



            var selector = db.Course_Student.Where(a => a.StudentID.Equals(Session["StudentID"])).ToList();


            foreach (var availableClass in selector)
            {
                Course_Student studentClass = new Course_Student();

                var classesStudentsTaking = db.courses.Where(a => a.CourseID.Equals(studentClass.CourseID)).ToList();

                foreach (var classAttending in classesStudentsTaking)
                {
                    course courseTaken = new course();
                    courseTaken.CourseTitle = classAttending.CourseTitle;
                    result.Add(courseTaken);
                }
                

            }

            return result; 
        }
        */







    




 