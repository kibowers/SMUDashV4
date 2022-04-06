namespace WebApplication3
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Professor_Course")]
    public partial class Professor_Course
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SectionID { get; set; }

        public int? ProfID { get; set; }

        public int? CourseID { get; set; }
    }
}
