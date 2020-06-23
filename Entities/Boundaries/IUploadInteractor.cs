using NetCore.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCore.Core.Boundaries
{
    public interface IUploadInteractor
    {
        IEnumerable<Document> GetAllDocuments();

        void UploadDocument(Document document);
     
        Document GetDocument(Guid id);

        Task<bool> DeleteDocument(Guid id);
    }
}
