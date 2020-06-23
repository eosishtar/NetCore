using NetCore.Core.Boundaries;
using NetCore.Core.Entities;
using NetCore.Logic.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCore.Logic.Interactors
{
    public class DocumentInteractor : IUploadInteractor
    {
        private readonly NCContext _db;

        private string[] allowedFileTypes = { ".png", ".jpg", ".jpeg", ".bmp" };

        public DocumentInteractor(NCContext db)
        {
            this._db = db;
        }

        public Task<bool> DeleteDocument(Guid id)
        {
            var item = _db.Document.Where(x => x.Id == id).FirstOrDefault();

            if (item != null)
            {
                _db.Document.Remove(item);
                _db.SaveChangesAsync();

                return System.Threading.Tasks.Task.FromResult(true);
            }

            return System.Threading.Tasks.Task.FromResult(false);
        }

        public IEnumerable<Document> GetAllDocuments()
        {
            return _db.Document;
        }

        public Document GetDocument(Guid id)
        {
            return _db.Document.Where(x => x.Id == id).FirstOrDefault();
        }

        public void UploadDocument(Document document)
        {
            //check the type of files uploaded here?
            if (!allowedFileTypes.Contains(Path.GetExtension(document.FileName)))
            {
               throw new Exception(
                    $"Only images are allowed. File type '{Path.GetExtension(document.FileName)}' not allowed");
            }

            _db.Document.Add(document);
            _db.SaveChanges();
        }

    }
}
