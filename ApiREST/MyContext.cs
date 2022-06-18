using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace ApiREST
{
    public class MyContext : DbContext
    {
        public DbSet<Students> Students { get; set; }
        public DbSet<Attendance> Attendance { get; set; }
        public DbSet<Classes> Classes { get; set; }
        public DbSet<Grades> Grades { get; set; }
        public DbSet<Lessons> Lessons { get; set; }
        public DbSet<Parents_students> Parents_students { get; set; }
        public DbSet<Subjects> Subjects { get; set; }
        public DbSet<Warnings> Warnings { get; set; }
        public DbSet<Parents> Parents { get; set; }
        public DbSet<Teachers> Teachers { get; set; }

        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {
            Database.Migrate();
        }

        public MyContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.HasDefaultSchema("public");
            modelBuilder.Entity<Parents_students>().HasKey(o => new { o.Student_id, o.Parent_id });


            //modelBuilder.Entity<Grades>().Property<Guid>("Student_id");
            //modelBuilder.Entity<Grades>().HasOne(s => s.Student).WithMany(g => g.Grades).HasForeignKey("Student_id");

            modelBuilder.Entity<Subjects>().Property<Guid>("Subject_id");
            modelBuilder.Entity<Subjects>().HasOne(s => s.Lesson_plan).WithMany(l => l.Subjects).HasForeignKey("Subject_id");

            modelBuilder.Entity<Parents_students>().HasOne(pu => pu.Student).WithMany(s => s.Parents_students).HasForeignKey(pu => pu.Student_id);
            modelBuilder.Entity<Parents_students>().HasOne(pu => pu.Parent).WithMany(s => s.Parents_students).HasForeignKey(pu => pu.Parent_id);

            modelBuilder.Entity<Students>().Navigation(s => s.Grades).AutoInclude();
            modelBuilder.Entity<Grades>().Navigation(g => g.Subjects).AutoInclude();
            modelBuilder.Entity<Grades>().Navigation(g => g.Teachers).AutoInclude();
            modelBuilder.Entity<Students>().Navigation(s => s.Classes).AutoInclude();
            modelBuilder.Entity<Lesson_plan>().Navigation(l => l.Subjects).AutoInclude();
            modelBuilder.Entity<Classes>().Navigation(c => c.Lesson_Plan).AutoInclude();
            
            //modelBuilder.Entity<Students>().Navigation(s => s.Parents_students).AutoInclude();
            //modelBuilder.Entity<Parents>().Navigation(p => p.Parents_students).AutoInclude();

            modelBuilder.Entity<Parents_students>().Navigation(p => p.Parent).AutoInclude();
            modelBuilder.Entity<Parents_students>().Navigation(s => s.Student).AutoInclude();

        }
    }
}
