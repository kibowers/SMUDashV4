namespace WebApplication3
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("student")]
    public partial class student
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StudentID { get; set; }

        [StringLength(50)]
        public string StudentFirstName { get; set; }

        [StringLength(50)]
        
        public string StudentLastName { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Please enter student email.")]
        [EmailAddress(ErrorMessage = "Please assure you are entering a valid email address.")]
        public string StudentEmail { get; set; }


        [StringLength(255)]
        [Required(ErrorMessage = "Please enter student password. ")]
        [MaxLength(8, ErrorMessage = "Passwords are only designated to be 8 characters long. Please assess your inputs. ") ]
        public string StudentPassword { get; set; }
    }
}
