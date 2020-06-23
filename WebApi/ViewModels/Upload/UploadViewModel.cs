using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.ViewModels
{
    public class UploadViewModel : ServiceRequestResultModel
    {
        public string FileName { get; set; }

        public IFormFile FileDoc { get; set; }
    }
}
