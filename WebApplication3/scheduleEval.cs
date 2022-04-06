namespace WebApplication3
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("scheduleEval")]
    public partial class scheduleEval
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ScheduleID { get; set; }

        public int? CourseID { get; set; }


        [Column(TypeName = "date")]
        public DateTime? DueDate { get; set; }
    }
}
