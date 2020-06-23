using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.ViewModels
{
    public class UploadCreateResultModel : ServiceRequestResultModel
    {
        public List<UploadViewModel> files { get; set; }
    }
}
