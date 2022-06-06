using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Dziennik_Szkolny
{
    public class MyContext : DbContext
    {
        //public DbSet<Car> Cars { get; set; }
        public DbSet<Users> Uzytkownicy { get; set; }
        public DbSet<Attendance> Obecnosc { get; set; }
        public DbSet<Classes> Klasy { get; set; }
        public DbSet<Grades> Obecny { get; set; }
        public DbSet<Lessons> Lekcje { get; set; }
        public DbSet<Parent_user> Rodzice { get; set; }
        public DbSet<Subjects> Przedmioty { get; set; }
        public DbSet<Warnings> Uwagi { get; set; }

        public MyContext() : base(nameOrConnectionString: "Dziennik") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            modelBuilder.Entity<Parent_user>().HasKey(o => new { o.user_id, o.parent_id });
            base.OnModelCreating(modelBuilder);
        }
    }
}
