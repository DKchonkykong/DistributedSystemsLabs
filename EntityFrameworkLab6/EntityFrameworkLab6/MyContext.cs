using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkLab6
{
    internal class MyContext :DbContext
    {
        public MyContext():base() 
        { 
        }
    public DbSet<Person> People { get; set; }
        public DbSet<Address> Addresses { get; set; }

        //need to change this to be different like the location of the database should work now 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=EFLab;Trusted_Connection=True;TrustServerCertificate=True;");
            base.OnConfiguring(optionsBuilder);
        }


    }
}
