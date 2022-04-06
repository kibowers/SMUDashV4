namespace WebApplication3
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    [Table("course")]
    public partial class course
    {
[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CourseID { get; set; }

        [StringLength(255)]
        [Required]
        public string CourseCode { get; set; }

        [StringLength(255)]
        [Required]
        public string CourseTitle { get; set; }

        [StringLength(255)]
        public string CourseTerm { get; set; }

        public int? CourseYear { get; set; }


       
    }


}
