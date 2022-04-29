using System;
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
                    HttpContext.Session.Timeout = 30;

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
                    HttpContext.Session.Timeout = 30;

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
                var scID = Convert.ToInt32(Session["ScheduleID"]);


                Console.WriteLine(CriticalThinkingProblemSolving_Score);


                /** evaluation submittedEval = new evaluation
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


                     /* the fields below are not controlled via the form 
                     StudentIDWriter = stuID,
                     StudentIDReceiver = stuRecID

                 };

                 */

                var foundEval = db.evaluations.Where(a => a.StudentIDWriter == stuID && a.ScheduleID == scID && a.StudentIDReceiver == stuRecID).FirstOrDefault();

                foundEval.CriticalThinkingProblemSolving_Score = CriticalThinkingProblemSolving_Score;
                foundEval.InnovativeEntrepreneurialSkills_Score = InnovativeEntrepreneurialSkills_Score;
                foundEval.CollaborationLeadership_Score = CollaborationLeadership_Score;
                foundEval.Communication_Score = Communication_Score;
                foundEval.Intercultural_Score = Intercultural_Score;
                foundEval.DevelopmentInAsia_Score = DevelopmentInAsia_Score;
                foundEval.EthicsSocialResponsibility_Score = EthicsSocialResponsibility_Score;
                foundEval.ResiliencePositivity_Score = ResiliencePositivity_Score;
                foundEval.SelfdirectednessMetalearning_Score = SelfdirectednessMetalearning_Score;
                foundEval.Knowledge_Score = Knowledge_Score;
                foundEval.CompletionTime = compTime;
                foundEval.ScheduleID = scID;

                db.evaluations.AddOrUpdate(foundEval);



                //db.evaluations.Add(submittedEval);

                /*Session["CriticalThinkingProblemSolving_Score"] = submittedEval.CriticalThinkingProblemSolving_Score;
                Session["InnovativeEntrepreneurialSkills_Score"] = submittedEval.InnovativeEntrepreneurialSkills_Score;
                Session["CollaborationLeadership_Score"] = submittedEval.CollaborationLeadership_Score;
                Session["Communication_Score"] = submittedEval.Communication_Score;
                Session["Intercultural_Score"] = submittedEval.Intercultural_Score;
                Session["DevelopmentInAsia_Score"] = submittedEval.DevelopmentInAsia_Score;
                Session["EthicsSocialResponsibility_Score"] = submittedEval.EthicsSocialResponsibility_Score;
                Session["ResiliencePositivity_Score"] = submittedEval.ResiliencePositivity_Score;
                Session["SelfdirectednessMetalearning_Score"] = submittedEval.SelfdirectednessMetalearning_Score;
                Session["Knowledge_Score"] = submittedEval.Knowledge_Score;
                */
                Session["CriticalThinkingProblemSolving_Score"] = foundEval.CriticalThinkingProblemSolving_Score;
                Session["InnovativeEntrepreneurialSkills_Score"] = foundEval.InnovativeEntrepreneurialSkills_Score;
                Session["CollaborationLeadership_Score"] = foundEval.CollaborationLeadership_Score;
                Session["Communication_Score"] = foundEval.Communication_Score;
                Session["Intercultural_Score"] = foundEval.Intercultural_Score;
                Session["DevelopmentInAsia_Score"] = foundEval.DevelopmentInAsia_Score;
                Session["EthicsSocialResponsibility_Score"] = foundEval.EthicsSocialResponsibility_Score;
                Session["ResiliencePositivity_Score"] = foundEval.ResiliencePositivity_Score;
                Session["SelfdirectednessMetalearning_Score"] = foundEval.SelfdirectednessMetalearning_Score;
                Session["Knowledge_Score"] = foundEval.Knowledge_Score;

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
            var scheduleidfinder = db.scheduleEvals.Where(a => a.CourseID.Value.Equals(input)).FirstOrDefault();


            if(scheduleidfinder != null)
            {
                Session["ScheduleID"] = scheduleidfinder.ScheduleID;
            }
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


                List<grouper> theGroups = new List<grouper>();

                var finder1 = db.groupers.Where(a => a.CourseID == theEval.CourseID).ToList();


                foreach (var holder1 in finder1)
                {
                    theGroups.Add(holder1);
                }


                //so now we have the groups. 
                //from here, conceptually, should loop through groups in clusters somehow, each student grading another?





                List<Student_Grouper> refList = new List<Student_Grouper>();

                foreach(var foundGroup in theGroups)
                {
                    var finder2 = db.Student_Grouper.Where(a => a.GroupID == foundGroup.GroupID).ToList();
                    //list of students within each group


                    foreach (var foundstu in finder2)
                    {




                        //create evaluation objects where they grade themselves 
                        var createdEval = new evaluation
                        {
                            ScheduleID = theEval.ScheduleID,
                            StudentIDWriter = foundstu.StudentID,
                            StudentIDReceiver = finder2[0].StudentID
                        };

                        var createdEval2 = new evaluation
                        {
                            ScheduleID = theEval.ScheduleID,
                            StudentIDWriter = foundstu.StudentID,
                            StudentIDReceiver = finder2[1].StudentID
                        };

                        var createdEval3 = new evaluation
                        {

                            ScheduleID = theEval.ScheduleID,
                            StudentIDWriter = foundstu.StudentID,
                            StudentIDReceiver = finder2[2].StudentID

                        };


                        db.evaluations.Add(createdEval);
                        db.evaluations.Add(createdEval2);
                        db.evaluations.Add(createdEval3);

                        db.SaveChanges();

                    }
                }


                /** go back and add more backend eval logic time permitting */


                /** so:
                 * for the course id within this object, we need to go to the grouper table and get the group id's assigned to that course id
                 * using those group id's we need to go to student groupers and retrieve the student id's
                 * then find some way to group together student id's. Maybe smart logic
                 * Then insert the scheduleeval object scheduleid, the writer, and receiever into evaluations table
                 * 
                 * 
                 * 
                 * */








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


            List<course> courseremover = new List<course>();
            foreach (var foundcourse in courselister)
            {
                var existingSchedules = db.scheduleEvals.Where(a => a.CourseID == foundcourse.CourseID).FirstOrDefault();

                if(existingSchedules != null)
                {
                    courseremover.Add(foundcourse);
                }
            }

            foreach(var coursetoremove in courseremover)
            {
                courselister.Remove(coursetoremove);
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
            List<Course_Student> infocounter = new List<Course_Student>();
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
                                        StudentID = foundstu.StudentID,
                                        DashAssigned = false
                 
                                    };

                                    db.Entry(newStu).State = System.Data.Entity.EntityState.Modified;


                                    infocounter.Add(newStu);
                                    db.Course_Student.AddOrUpdate(newStu);







                                }
                                db.SaveChanges();


                            }


                        }
                    }
                    catch(Exception ex)
                    {
                        ViewBag.Message = "Double check your inputs.";
                    }




                    List<student> names = new List<student>();
                    foreach(var stuinfo in infocounter)
                    {
                        var finder1 = db.students.Where(a => a.StudentID.Equals(stuinfo.StudentID)).FirstOrDefault();

                        if(finder1 != null)
                        {
                            names.Add(finder1);
                        }
                    }


                    HttpContext.Session["CreatedCourseStudents"] = names;
                    Session["NumOfStudents"] = infocounter.Count.ToString();


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

            //session variable storing the course name upon selection. need this for confirmation screen. 
            var courseStorer = db.courses.Where(a => a.CourseID.Equals(input)).FirstOrDefault();

            Session["CourseName"] = courseStorer.CourseTitle;
            Session["CourseTerm"] = courseStorer.CourseTerm;
            Session["CourseYear"] = courseStorer.CourseYear;
            Session["CourseCode"] = courseStorer.CourseCode;
            Session["CourseID"] = courseStorer.CourseID;



            //going to course_student to retrieve the list of students who are taking the selected course
            var courseLister = db.Course_Student.Where(a => a.CourseID == input).ToList();
            var groupsearcha = db.groupers.Where(a => a.CourseID == input).ToList(); //list of groups for this course ID




            List<Course_Student> holder = new List<Course_Student>();

            foreach (var assignFinder in courseLister)
            {
                //var notAssigned = db.Course_Student.Where(a => a.DashAssigned != true).FirstOrDefault();
                if(assignFinder.DashAssigned != true)
                {

                    holder.Add(assignFinder);
                }

            }

            List<student> stufinder = new List<student>();
            foreach (var stuobj in holder) 
             {
                var stufound = db.students.Where(a => a.StudentID.Equals(stuobj.StudentID)).FirstOrDefault();

                if(stufound != null)
                {

                    stufinder.Add(stufound); 
                }

             }


            List<Student_Grouper> stugrouper = new List<Student_Grouper>(); //list of student grouper objects associated with the list of student id's
            foreach (var stuObj2 in stufinder)
            {
                var stugroup = db.Student_Grouper.Where(a => a.StudentID == stuObj2.StudentID).ToList();

                foreach(var item1 in stugroup)
                {
                    stugrouper.Add(item1);
                }
            }

            List<grouper> groupgetter = new List<grouper>();
            foreach(var groupinfo in stugrouper)
            {
                var groupinfogetter = db.groupers.Where(a => a.GroupID == groupinfo.GroupID && a.CourseID == input).ToList();

                foreach (var item1 in groupinfogetter)
                {

                    Debug.WriteLine(item1);


                    groupgetter.Add(item1); //list of groups in the course! We have ID's
                }
            }


            List<Student_Grouper> reverser = new List<Student_Grouper>();
            foreach(var groupremover in groupgetter)
            {
                var groupreverser = db.Student_Grouper.Where(a => a.GroupID == groupremover.GroupID).ToList();

                foreach (var item1 in groupreverser)
                {
                    reverser.Add(item1);
                }
            
            }



            List<student> sturemov = new List<student>();
            foreach(var sturemove in reverser)
            {
                var removalstu = db.students.Where(a => a.StudentID == sturemove.StudentID).ToList();

                foreach (var item1 in removalstu)
                {
                    sturemov.Add(item1);
                }
            }


            foreach(var sturemoved in sturemov)
            {
                stufinder.Remove(sturemoved);
            }




            return Json(stufinder.ToList(), JsonRequestBehavior.AllowGet);

        }


        [HttpGet]
        public ActionResult GetTableNames(int input)
        {

            List<student> stufinder = new List<student>();

            var stufound = db.students.Where(a => a.StudentID.Equals(input)).FirstOrDefault();
            //tried using Entity property. Having issues. May have to use traditional SQL. 


            if (stufound != null)
            {
                stufinder.Add(stufound);
            }

            return Json(stufinder, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public ActionResult CreateStudentGroups(StudentGroupCreationViewModel modelThing)
        {
            //initially, check to see that the form submitted is valid
            if (ModelState.IsValid)
            {



                //retrieve the values of the posted three selected students
                var select1 = Request.Form["1"].AsInt();
                var select2 = Request.Form["2"].AsInt();
                var select3 = Request.Form["3"].AsInt(); 
                var selectedClass = (int?)Convert.ToInt32(Request.Form["hiddenInfo"]); //this holds the information for the selected course
                int holder = new int();



                // search. Let's find the course_student objects based on the studentID and the selected class
                var obj1 = db.Course_Student.Where(a => a.StudentID == (select1) && a.CourseID == (selectedClass)).FirstOrDefault();
                var obj2 = db.Course_Student.Where(a => a.StudentID == (select2) && a.CourseID == (selectedClass)).FirstOrDefault();
                var obj3 = db.Course_Student.Where(a => a.StudentID == (select3) && a.CourseID == (selectedClass)).FirstOrDefault();

                /*  //set dash assigned property to true!!! Now they shouldn't appear on the dropdown
                  obj1.DashAssigned = true;
                  obj2.DashAssigned = true;
                  obj3.DashAssigned = true;



                  //unsure if this will work. Trying to add/update entity objects once dashassigned is changed
                  db.Course_Student.AddOrUpdate(obj1);
                  db.Course_Student.AddOrUpdate(obj2);
                  db.Course_Student.AddOrUpdate(obj3);
                  db.SaveChanges();

                  */


                Debug.WriteLine(obj1.DashAssigned.ToString());
                Debug.WriteLine(obj2.DashAssigned.ToString());
                Debug.WriteLine(obj3.DashAssigned.ToString());



                //clustering together student info to get session variables for confirmation screen. 
                var student1finder = db.students.Where(a => a.StudentID == (select1)).FirstOrDefault();
                var student2finder = db.students.Where(a => a.StudentID == (select2)).FirstOrDefault();
                var student3finder = db.students.Where(a => a.StudentID == (select3)).FirstOrDefault();

                //cobbling session info
                Session["Student1Info"] = student1finder.StudentFirstName + " " + student1finder.StudentLastName;
                Session["Student2Info"] = student2finder.StudentFirstName + " " + student2finder.StudentLastName;
                Session["Student3Info"] = student3finder.StudentFirstName + " " + student3finder.StudentLastName;







                //so now we are searching the group table to find if  a group object already exists for this course id
                var groupnumFinder = db.groupers.Where(a => a.CourseID == (selectedClass)).FirstOrDefault();

                if (groupnumFinder == null)
                {
                    holder = 1;
                }
                else
                {
                    holder = 2;
                }



                //create a grouper object
                var groupCreate = new grouper
                {
                    GroupID = default,
                    CourseID = selectedClass,
                    GroupNumInCourse = holder //use logic to create modular holder object
                };


                //add object to model
                db.groupers.AddOrUpdate(groupCreate);



                //save changes to database - unsure if this will work without mysql instantiated connection (need to test on desktop)
                db.SaveChanges();

                var groupidfinder = groupCreate.GroupID;
                var groupnum = groupCreate.GroupNumInCourse;

                //cobbling together further session info
                Session["GroupID"] = groupidfinder;
                Session["GroupNumInCourse"] = groupnum;




                /** create three new Student_Grouper objects based on information provided. 
                 */


                //first student created
                var StuGroupCreate = new Student_Grouper
                {
                    GroupID = groupidfinder,
                    GroupMemberID = default,
                    StudentID = select1,
                };
                db.Student_Grouper.AddOrUpdate(StuGroupCreate);//add first student

                //second student created
                var StuGroupCreate2 = new Student_Grouper
                {
                    GroupID = groupidfinder,
                    GroupMemberID = default,
                    StudentID = select2,
                };
                db.Student_Grouper.AddOrUpdate(StuGroupCreate2); //add second student 

                //third student created 
                var StuGroupCreate3 = new Student_Grouper
                {
                    GroupID = groupidfinder,
                    GroupMemberID = default,
                    StudentID = select3,

                };
                db.Student_Grouper.AddOrUpdate(StuGroupCreate3); //add third student







                db.SaveChanges();




                //return user to completion screen
                return RedirectToAction("GroupCreated");


                //find way to retroactively remove students based on those three id's?
            }



            //we have the three selected students in the group. 
            /*
             * We can flip idea on it's head 
             * Create table object within grouper table: this will autoiterate the groupID 
             * Grab courseID from selected dropdown result
             * Insert Group number in course: if no existing entry, insert 1, if otherwise, 2
             * 
             * 
             * Grouper object now created. We can now scrape info from grouper
             * to create student_grouper objects...BASED on the three Id's retrieved from the form!
             * Group member iD autoincrements..no need to worry about it 
             * Student ID can be inserted from the three table values
             * Group ID can be grabbed from the previously inserted grouper object.
             * 
             *
             */

            return View();

        }



        public ActionResult GroupCreated()
        {
            //Controller method displaying a view that displays once a group is created. 


            return View();
        }


        public ActionResult TableauPages()
        {
            return View();

        }

        [HttpGet]
        public ActionResult GetExistingCourseDropdown()
        {

            var coursecompounder = db.courses.Where(a => a.CourseID > 0).ToList();

            List<course> courselister = new List<course>();
                    foreach (var coursename in coursecompounder)
                    {
                        var coursefound = coursename;
                        courselister.Add(coursefound);
                    }
                
            
            courselister.Insert(0, new course { CourseID = 0, CourseTitle = "Please select a course." });
            var viewModel = new SelectStudentCourseViewModel { Courses = courselister };

            return View(viewModel);

        }




        public ActionResult ImportCoursesExisting()
        {
            GetExistingCourseDropdown();
            return View();
        }

        public ActionResult ExistingCourseCreated()
        {
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ImportCoursesExisting(HttpPostedFileBase file)
        {


            //duplicate course protection. 
            List<student> stufindah = new List<student>();
            List<Course_Student> infocounter = new List<Course_Student>();

            var courseid = Request.Form["hiddenInfo1"].AsInt();
            var ct = db.courses.Where(a => a.CourseID.Equals(courseid)).FirstOrDefault().CourseTitle; //get existing title
            var cc = db.courses.Where(a => a.CourseID.Equals(courseid)).FirstOrDefault().CourseCode; //using selected courseID, get the course code
            var cy = (int?)Convert.ToInt32(Request.Form["courseYear"]); //request the course year from the form
            var cterm = Request.Form["hiddenInfo2"]; //get the course term here
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
                                        StudentID = foundstu.StudentID,
                                        DashAssigned = false

                                    };

                                    db.Entry(newStu).State = System.Data.Entity.EntityState.Modified;


                                    infocounter.Add(newStu);
                                    db.Course_Student.AddOrUpdate(newStu);







                                }
                                db.SaveChanges();


                            }


                        }
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = "Double check your inputs.";
                    }


                    List<student> names = new List<student>();
                    foreach (var stuinfo in infocounter)
                    {
                        var finder1 = db.students.Where(a => a.StudentID.Equals(stuinfo.StudentID)).FirstOrDefault();

                        if (finder1 != null)
                        {
                            names.Add(finder1);
                        }
                    }


                    HttpContext.Session["CreatedCourseStudents"] = names;
                    Session["NumOfStudents"] = infocounter.Count.ToString();


                    return RedirectToAction("courseCreated");
                }









            }
            return View();
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







    




 