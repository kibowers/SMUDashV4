namespace WebApplication3
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("professor")]
    public partial class professor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProfID { get; set; }

        [StringLength(255)]
        public string ProfFirstName { get; set; }

        [StringLength(255)]
        public string ProfLastName { get; set; }

        [StringLength(255)]
        [Required(ErrorMessage = "Please enter an email. ")]
        [EmailAddress(ErrorMessage = "Please assure you are entering a valid email address.")]
        public string ProfEmail { get; set; }

        [StringLength(255)]
        [Required(ErrorMessage = "Please enter a password. ")]
        [MaxLength(8, ErrorMessage = "Passwords are only designated to be 8 characters long. Please assess your inputs. ")]
        public string ProfPassword { get; set; }
    }
}
