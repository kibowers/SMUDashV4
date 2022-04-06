using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication3
{
    public class ProfDBModel
    {
        public student Student { get; set; }

        public evaluation Evaluation { get; set; }

        public course Course { get; set;  }

        public scheduleEval ScheduleEval { get; set; }

    }
}