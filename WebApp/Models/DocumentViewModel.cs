using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class DocumentViewModel 
    {
        [Key]
        public Guid FileId { get; set; }

        [Required(ErrorMessage = "File name is required.")]
        [Column(TypeName = "nvarchar(50)")]
        [DisplayName("File Name")]
        public string FileName { get; set; }

        [DisplayName("Upload File")]
        public byte[] FileDoc { get; set; }
    }
}
