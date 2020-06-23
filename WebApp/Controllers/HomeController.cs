using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NetCore.Core.Boundaries;
using NetCore.Core.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUploadInteractor _documentInteractor;

        public HomeController(ILogger<HomeController> logger, IUploadInteractor documentInteractor)
        {
            _logger = logger;
            _documentInteractor = documentInteractor;
        }

        public IActionResult Index()
        {
            var items = _documentInteractor.GetAllDocuments();
            
            var model = new List<DocumentViewModel>();
            foreach (var item in items)
            {
                model.Add(new DocumentViewModel()
                {
                    FileId = item.Id,
                    FileName = item?.Description ?? item.FileName,
                    FileDoc = item.FileData
                });
            }

            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UploadDocument(DocumentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Create", "Home", model);
            }

            try
            {
                foreach (var file in Request.Form.Files)
                {
                    if (file.Length > 0)
                    {
                        var document = new NetCore.Core.Entities.Document()
                        {
                            Id = Guid.NewGuid(),
                            Description = model.FileName,
                            FileName = file.FileName
                        };

                        MemoryStream ms = new MemoryStream();
                        file.CopyTo(ms);
                        document.FileData = ms.ToArray();

                        ms.Close();
                        ms.Dispose();

                        _documentInteractor.UploadDocument(document);

                        ViewBag.Message = "Image(s) stored in database!";
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "An error has occurred. <br /> " + ex.Message.ToString();
            }

            return RedirectToAction("Index", model);
        }

        public IActionResult Details(Guid? id)
        {
            var item = _documentInteractor.GetDocument((Guid)id);

            string imageBase64Data = Convert.ToBase64String(item.FileData);
            string imageDataURL = string.Format("data:image/jpg;base64,{0}", imageBase64Data);

            ViewBag.ImageTitle = item?.Description ?? item.FileName;
            ViewBag.ImageDataUrl = imageDataURL;

            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromBody]Guid id)
        {
            try
            {
                var res = await this._documentInteractor.DeleteDocument(id);
                if (res == true)
                {
                    return Json(new { isok = true, message = "Attachment successfully deleted" });
                }
                else
                {
                    return Json(new { isok = false, message = "An error occured when deleting the attachment" });
                }
            }
            catch (Exception ex)
            {
                return this.RedirectToAction("Error", "Home");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
