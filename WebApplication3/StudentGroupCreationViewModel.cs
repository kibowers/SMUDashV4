using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication3
{
    public class StudentGroupCreationViewModel
    {

        public Course_Student CourseStudent { get; set; }
        public List<course> Courses { get; set; }


        public scheduleEval ScheduleEval { get; set; }

        public evaluation Evaluation { get; set; }
        public student Student { get; set; }
        public Student_Grouper student_Grouper { get; set; }

    }
}