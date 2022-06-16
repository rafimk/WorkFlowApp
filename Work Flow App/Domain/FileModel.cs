using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Work_Flow_App.Domain
{
    public abstract class FileModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string FileType { get; set; }
        public string Extension { get; set; }
        public string Description { get; set; }
        public int? UploadedBy { get; set; }
        public DateTime? CreatedOn { get; set; }

    }
}
