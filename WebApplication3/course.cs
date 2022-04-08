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
        [Key]
[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "Please assure your inputs have been selected.")]

        public int CourseID { get; set; }

        [StringLength(255)]
        [Required]
        public string CourseCode { get; set; }

        [StringLength(255)]
        [Required(ErrorMessage = "Please assure your inputs have been selected.")]

        public string CourseTitle { get; set; }

        [StringLength(255)]
        public string CourseTerm { get; set; }

        public int? CourseYear { get; set; }


       
    }


}
