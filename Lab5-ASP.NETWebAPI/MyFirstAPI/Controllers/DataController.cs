using Microsoft.AspNetCore.Mvc;

namespace MyFirstAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DataController : ControllerBase
    {
        private readonly string[] _myData = new[] { "zero", "one", "two", "three", "four", "five" };

        // GET /data  -> returns ["zero","one","two","three","four","five"]
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return _myData;
        }

        // GET /data/3 -> returns "three"
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            if (id < 0 || id >= _myData.Length)
            {
                return NotFound(); // 404 if index is out of range
            }

            return _myData[id];
        }
    }
}
