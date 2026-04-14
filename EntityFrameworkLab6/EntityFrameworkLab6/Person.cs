using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkLab6
{
    //added base values
    public class Person
    {
        public int PersonID { get; set; }
        public string First_Name { get; set; }
        public string Middle_Name { get; set; }
        public string Last_Name { get; set; }
        public DateTime Date_of_Birth { get; set; }
        public int Age { get; set; }
        public Address Address { get; set; }
        public Person() { }
    }

    //should be fine new had i tmixe up 
    //public static class PersonSeedData
    //{
    //    public static void AddDefaultPerson()
    //    {
    //        using var ctx = new MyContext();

    //        Address addr = new Address()
    //        {
    //            House_Name_or_Number = "1076",
    //            Street = "One way Street",
    //            City = "Down City",
    //            County = "Wrong way County",
    //            Postcode = "WR 1337",
    //            People = new List<Person>()
    //        };

    //        Person prsn = new Person()
    //        {
    //            First_Name = "John",
    //            Middle_Name = "Smith",
    //            Last_Name = "Smith",
    //            Date_of_Birth = new DateTime(1990, 1, 1),
    //            Age = "33",
    //            Address = addr
    //        };

    //        ctx.Add(prsn);
    //        ctx.SaveChanges();
    //    }
    //}
}
