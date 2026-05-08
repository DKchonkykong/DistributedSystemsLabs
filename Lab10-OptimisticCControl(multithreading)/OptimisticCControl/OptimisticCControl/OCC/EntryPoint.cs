using Microsoft.EntityFrameworkCore;
using OOCLab;
using System;
using System.Collections.Generic;
using System.Linq;


using (var ctx = new Context())
{
    Address addr = new Address() { House_Name_or_Number = "1076", Street = "Some Street", City = "Some City", County = "Some County", Country = "UK", Postcode = "Some Postcode", People = new List<Person>() };
    BankAccount accnt = new BankAccount() { Balance = 50.0m };
    Person prsn = new Person() { First_Name = "Jane", Last_Name = "Doe", Date_of_Birth = new DateTime(1987, 10, 1), Age = (int.Parse(DateTime.Now.ToString("yyyyMMdd")) - 19871001)/10000, Address = addr, BankAccount = accnt };

    ctx.Addresses.Add(addr);
    ctx.BankAccounts.Add(accnt);
    ctx.People.Add(prsn);

    ctx.SaveChanges();
}
//this is for bank account balance for user?
//need to fix says OOCLab.Person.BankAccount.get returned null. now works and balance updates accordingly

//added mitigation now apparently previous version didn't update the rows or smth ok

using (var ctx = new Context())
{
    bool success = false;
    while (!success)
    {
        Person prsn = ctx.People.First();
        decimal balance = prsn.BankAccount.Balance;
        decimal balancechange = 0;
        do
        {
            Console.WriteLine("Enter a balance modifier");
        }
        while (!decimal.TryParse(Console.ReadLine(), out balancechange));
        balance += balancechange;
        prsn.BankAccount.Balance = balance;
        try
        {
            ctx.SaveChanges();
            success = true;
        }
        catch (DbUpdateConcurrencyException ex)
        {
            var entry = ex.Entries[0];
            var d = entry.GetDatabaseValues();
            entry.OriginalValues.SetValues(d);
            entry.CurrentValues.SetValues(d);
            Console.WriteLine("Sorry. That didn't work. Try again.");
        }
    }
}