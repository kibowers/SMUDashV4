namespace WebApplication3
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Student_Grouper")]
    public partial class Student_Grouper
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int GroupMemberID { get; set; }

        public int? GroupID { get; set; }

        public int? StudentID { get; set; }
    }
}
