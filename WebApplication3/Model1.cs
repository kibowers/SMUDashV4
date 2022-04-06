using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace WebApplication3
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=AwsModel316")
        {
        }

        public virtual DbSet<course> courses { get; set; }
        public virtual DbSet<Course_Student> Course_Student { get; set; }
        public virtual DbSet<evaluation> evaluations { get; set; }
        public virtual DbSet<grouper> groupers { get; set; }
        public virtual DbSet<professor> professors { get; set; }
        public virtual DbSet<Professor_Course> Professor_Course { get; set; }
        public virtual DbSet<scheduleEval> scheduleEvals { get; set; }
        public virtual DbSet<student> students { get; set; }
        public virtual DbSet<Student_Grouper> Student_Grouper { get; set; }

        public SelectList CourseDropdown { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<course>()
                .Property(e => e.CourseCode)
                .IsUnicode(false);

            modelBuilder.Entity<course>()
                .Property(e => e.CourseTitle)
                .IsUnicode(false);

            modelBuilder.Entity<course>()
                .Property(e => e.CourseTerm)
                .IsUnicode(false);

            modelBuilder.Entity<professor>()
                .Property(e => e.ProfFirstName)
                .IsUnicode(false);

            modelBuilder.Entity<professor>()
                .Property(e => e.ProfLastName)
                .IsUnicode(false);

            modelBuilder.Entity<professor>()
                .Property(e => e.ProfEmail)
                .IsUnicode(false);

            modelBuilder.Entity<professor>()
                .Property(e => e.ProfPassword)
                .IsUnicode(false);

            modelBuilder.Entity<student>()
                .Property(e => e.StudentFirstName)
                .IsUnicode(false);

            modelBuilder.Entity<student>()
                .Property(e => e.StudentLastName)
                .IsUnicode(false);

            modelBuilder.Entity<student>()
                .Property(e => e.StudentEmail)
                .IsUnicode(false);

            modelBuilder.Entity<student>()
                .Property(e => e.StudentPassword)
                .IsUnicode(false);
        }

       

    }
}
