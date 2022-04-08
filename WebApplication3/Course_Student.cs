namespace WebApplication3
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Course_Student")]
    public partial class Course_Student
    {
        [Key]

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EnrollmentID { get; set; }

        public int StudentID { get; set; }

        public int CourseID { get; set; }

        [NotMapped]
        public bool DashAssigned { get; set; } = false;
        /* new unmapped entity property. created so that I have easy way to filter students based on this. */
    }
}
