using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations;

namespace DistSysAcwServer.Models
{
    public class Log
    {
        [Key]
        public int id { get; set; }
        public string LogString { get; set; }
        public DateTime LogDateTime { get; set; }
        public string ApiKey { get; set; }

        public Log()
        { }

        public Log(string logString, string apiKey)
        {
            LogString = logString;
            LogDateTime = DateTime.Now;
            ApiKey = apiKey;
        }

    }
}
