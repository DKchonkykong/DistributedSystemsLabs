using Microsoft.AspNetCore.Mvc;

namespace MyFirstAPI.DataAccess
{
    //crud stuff yay
    //it basically does the create read update and delete operations for the string my data
    public class MyDataCRUD
    {

        private string[] MyData { get; set; }
        public void Create()
        {
            MyData = new string[] { "zero", "one", "two", "three", "four", "five" };
        }
        public string[] Read()
        {
            return MyData;
        }
        public string Read(int index)
        {
            return MyData[index];
        }
        public void Update(int index, string data)
        {
            MyData[index] = data;
        }
        public void Delete(int index)
        {
            MyData[index] = "Deleted";
        }
    }


    public class MyDataCRUDGet
    {
        private string[] MyData { get; set; }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return Read();
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return Read(id);
        }
        // added another get id method but it returns something else it gives me the string of error e.g., data 5 
        // going to try do this a bit later
        [HttpGet("{id}")]
        
        public string Get(string id)
        {
            return "Data not found.";
        }
        


        //done this modified the HTTPGet method to be action
        [HttpGet("[action]")]
        public void CreateData()
        {
            Create();
        }

        public void Create()
        {
            MyData = new[] { "zero", "one", "two", "three", "four", "five" };
        }

        public string[] Read() => MyData;
        public string Read(int index) => MyData[index];
        public void Update(int index, string data) => MyData[index] = data;
        public void Delete(int index) => MyData[index] = "Deleted";
    }


}

