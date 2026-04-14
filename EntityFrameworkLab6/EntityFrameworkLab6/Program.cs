// See https://aka.ms/new-console-template for more information
using EntityFrameworkLab6;
//will do more later unsure why it doesn't give me an error? apparently it is key related though
//need to add Add-Migration InitialCreate apparently will do later 
//ok added the add mitigation in package manager major issue is that I still get the same error that SQL database doesn't exist apparently supposed to get another error at this point relating to the model and database not being valid unsure how to fix this but currently I am 3/4 done of lab 6 and then need to do the rest of last week lab and this week and should try to get around to doing the assignment and exam for it as well. (page 6/9 of lab 6)
//I think I am done with this lab but im not sure if this is how it is supposed to look like since I can't see the data only the how it looks as a type in sql object explorer also was supposed to roll back to a previous point but didn't work so uhhh 
using (var ctx = new MyContext())
{

    Address addr = new Address()
    {
        House_Name_or_Number = "1076",
        Street = "One way Street",
        City = "Down City",
        County = "Wrong way County",
        Postcode = "WR 13ET",
        Country = "United Kingdom",
        People = new List<Person>()
    };

    Person prsn = new Person()
    {
        First_Name = "John",
        Middle_Name = "Smith",
        Last_Name = "Smith",
        Date_of_Birth = new DateTime(1993, 1, 1),
        Age = 33,
        Address = addr
    };

    ctx.Add(prsn);
    ctx.SaveChanges();
}