using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.ViewModels
{
    public class ServiceRequestResultModel
    {
        public int StatusCode { get; set; }

        public string Message { get; set; }
     
        public string ExceptionMessage { get; set; }
     
        public string Path { get; set; }

        public DateTime DateTimeStamp { get; set; } = DateTime.Now;

    }
}
