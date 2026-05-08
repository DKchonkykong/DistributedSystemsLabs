using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OOCLab
{
    public class BankAccount
    {
        [Timestamp]
        public byte[] RowVersion { get; set; }
        [Key]
        public int AccountIdentifier { get; set; }
        public decimal Balance { get; set; }
        public BankAccount() { }
    }
}
