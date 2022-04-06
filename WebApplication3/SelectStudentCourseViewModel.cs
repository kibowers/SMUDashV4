using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication3
{
    public class SelectStudentCourseViewModel
    {

        public Course_Student CourseStudent { get; set; }
        public List<course> Courses { get; set; }
        public IEnumerable<course> studentCourses { get; set; }

        public List<student> studentsinGroup { get; set; }
        public scheduleEval ScheduleEval { get; set; }

        public evaluation Evaluation { get; set; }
        public student Student { get; set; }

    }
}