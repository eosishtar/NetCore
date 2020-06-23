using NetCore.Core.Boundaries;
using NetCore.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Net;
using WebApi.Helpers;
using WebApi.ViewModels;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UploadController : ControllerBase
    {
        private readonly IUploadInteractor _documentInteractor;
        private readonly ILogger _logger;

        public UploadController(IUploadInteractor documentInteractor, ILogger<UploadController> logger)
        {
            this._documentInteractor = documentInteractor;
            this._logger = logger;
        }

        [HttpGet]
        public ActionResult<UploadCreateResultModel> Test()
        {
            return this.HandleBadRequest<UploadCreateResultModel>("", "This is a test");
        }


        [HttpPost]
        public ActionResult<UploadCreateResultModel> UploadImage()
        {
            //TODO: 
            /*

            * Add Serilog 
            * Add MiddleWare for the logging 
             */

            if (Request.Form.Files.Count == 0)
            {
                return this.HandleBadRequest<UploadCreateResultModel>(
                      Request.Path,
                      "There are no images to upload");
            }

            int fileCount = 0;

            try
            {
                foreach (var file in Request.Form.Files)
                {
                    if (file.Length > 0)
                    {
                        fileCount++;

                        Document document = new Document()
                        {
                            Id = Guid.NewGuid(),
                            FileName = file.FileName
                        };

                        MemoryStream ms = new MemoryStream();
                        file.CopyTo(ms);
                        document.FileData = ms.ToArray();

                        ms.Close();
                        ms.Dispose();

                        _documentInteractor.UploadDocument(document);
                    }
                }

                return new UploadCreateResultModel()
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = $"'{fileCount}' file(s) were successfully uploaded."
                };
            }
            catch (Exception ex)
            {
                //TODO: need to do some logging here and middleware
                return this.HandleBadRequest<UploadCreateResultModel>(
                    Request.Path,
                    ex.Message.ToString());
            }
        }
    }
}