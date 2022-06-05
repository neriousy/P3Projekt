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

        public MyContext() : base(nameOrConnectionString: "Dziennik") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            base.OnModelCreating(modelBuilder);
        }
    }
}
