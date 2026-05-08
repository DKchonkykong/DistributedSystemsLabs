using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OOCLab
{
    public class Person
    {
        [Key]
        public int PersonID { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public DateTime Date_of_Birth { get; set; }
        public virtual Address Address { get; set; }
        public virtual BankAccount BankAccount { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public int Age { get; set; }
        public Person() { }
    }
}
