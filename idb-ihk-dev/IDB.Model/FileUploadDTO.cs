using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDB.Model
{

        public class FileUploadDTO
        {
            public int RecordId { get; set; }
            public string FileName { get; set; }
            public string ContentType { get; set; }
            public decimal? FileSize { get; set; }
            public byte[]? FileData { get; set; }
            public string? UploadedBy { get; set; }
            public string PlannedStoragePath { get; set; }
            public DateTime? UploadDate { get; set; }
        }
    
}
