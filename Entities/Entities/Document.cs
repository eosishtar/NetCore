using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCore.Core.Entities
{
    [Table("Document", Schema = "brm")]
    public partial class Document
    {
        [Key]
        public Guid Id { get; set; }
        [StringLength(50)]
        public string Description { get; set; }
        [StringLength(50)]
        public string FileName { get; set; }
        public byte[] FileData { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
