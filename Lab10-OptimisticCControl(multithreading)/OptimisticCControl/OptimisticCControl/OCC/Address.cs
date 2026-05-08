using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OOCLab
{
    public class Address
    {
        [Key]
        public int AddressIdentifier { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public string House_Name_or_Number { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string Country { get; set; }
        public string Postcode { get; set; }
        public virtual ICollection<Person> People { get; set; }
        public Address() { }
    }
}
