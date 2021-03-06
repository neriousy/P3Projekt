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
        public DbSet<Students> Studenci { get; set; }
        public DbSet<Attendance> Obecnosc { get; set; }
        public DbSet<Classes> Klasy { get; set; }
        public DbSet<Grades> Grades { get; set; }
        public DbSet<Lessons> Lekcje { get; set; }
        public DbSet<Parents_students> Parents_students { get; set; }
        public DbSet<Subjects> Przedmioty { get; set; }
        public DbSet<Warnings> Uwagi { get; set; }
        public DbSet<Parents> Rodzice { get; set; }
        public DbSet<Teachers> Nauczyciele { get; set; }

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
            modelBuilder.Entity<Parents_students>().HasKey(o => new { o.student_id, o.parent_id });


            modelBuilder.Entity<Grades>().Property<Guid>("student_id");
            modelBuilder.Entity<Grades>().HasOne(s => s.Student).WithMany(g => g.Grades).HasForeignKey("student_id");


            modelBuilder.Entity<Parents_students>().HasOne(pu => pu.Student).WithMany(s => s.Parents_students).HasForeignKey(pu => pu.student_id);
            modelBuilder.Entity<Parents_students>().HasOne(pu => pu.Parent).WithMany(s => s.Parents_students).HasForeignKey(pu => pu.parent_id);

            modelBuilder.Entity<Students>().Navigation(s => s.Grades).AutoInclude();

            modelBuilder.Entity<Parents_students>().Navigation(p => p.Parent).AutoInclude();
            modelBuilder.Entity<Parents_students>().Navigation(s => s.Student).AutoInclude();

            //modelBuilder.Entity<Students>().Navigation(s => s.Parents_students).AutoInclude();
            //modelBuilder.Entity<Parents>().Navigation(p => p.Parents_students).AutoInclude();

        }
    }
}
